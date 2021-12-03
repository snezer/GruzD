import Vue from 'vue';
import Axios, { AxiosStatic } from 'axios';
import { accountService } from '@/services';
import { tokenStorage } from '@/helpers';

declare module 'vue/types/vue' {
  interface Vue {
    $http: AxiosStatic;
  }
}

export function initAxiosSupport() {
  Vue.prototype.$http = Axios;

  Axios.interceptors.request.use(
    (config) => {
      const accessToken = tokenStorage.get();
      if (accessToken) {
        config.headers.Authorization = `Bearer ${accessToken.access_token}`;
      }
      console.log(process.env.VUE_APP_BACKEND_URL);
      //config.baseURL = process.env.VUE_APP_BACKEND_URL;

      config.baseURL = 'https://f506-89-232-75-108.ngrok.io/';

      return config;
    },
    (err) => Promise.reject(err)
  );

  Axios.interceptors.response.use(
    (response) => {
      if (response.status >= 200 && response.status <= 210) {
        return Promise.resolve(response);
      } else {
        return Promise.reject(response);
      }
    },
    (error) => {
      if (error.response && error.response.status) {
        switch (error.response.status) {
          case 401:
            console.log('Session expired');
            console.log(error);

            return accountService
              .refresh()
              .then(() => {
                const config = error.config;

                return new Promise((resolve, reject) => {
                  Axios.request(config)
                    .then((response) => {
                      resolve(response);
                    })
                    .catch((error) => {
                      reject(error);
                    });
                });
              })
              .catch((error) => {
                Promise.reject(error);
              });
          case 403:
            // router.replace({
            //   path: "/login",
            //   query: { redirect: router.currentRoute.fullPath }
            // });
            break;
          case 404:
            console.log('Page not exist');
            break;
          case 502:
            console.log('Bad gateway');
            setTimeout(() => {
              // router.replace({
              //   path: "/login",
              //   query: {
              //     redirect: router.currentRoute.fullPath
              //   }
              // });
            }, 1000);
        }
        return Promise.reject(error.response);
      }
    }
  );
}
