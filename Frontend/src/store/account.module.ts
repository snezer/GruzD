import { accountService } from '@/services';

const user = localStorage.userToken;
const initialState = user
  ? { status: { loggedIn: true }, user }
  : { status: { loggedIn: false }, user: null };

const account = {
  namespaced: true,
  state: initialState,
  actions: {
    login(
      { commit }: { commit: (ev: string, param?: any) => void },
      user: any
    ) {
      return accountService.login(user.username, user.password).then(
        (user) => {
          commit('loginSuccess', user);
          return Promise.resolve(user);
        },
        (error) => {
          commit('loginFailure');
          return Promise.reject(error);
        }
      );
    },
    logout({ commit }: { commit: (ev: string, param?: any) => void }) {
      accountService.logout();
      commit('logout');
    },
  },
  mutations: {
    loginRequest(state: any, user: any) {
      state.status = { loggingIn: true };
      state.user = user;
    },
    loginSuccess(state: any, user: any) {
      state.status = { loggedIn: true };
      state.user = user;
    },
    loginFailure(state: any) {
      state.status = {};
      state.user = null;
    },
    logout(state: any) {
      state.status = {};
      state.user = null;
    },
  },
};

export default account;
