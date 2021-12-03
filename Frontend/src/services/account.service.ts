import axios, { AxiosResponse } from 'axios';
import qs from 'qs';
import { tokenStorage, AccessToken } from '@/helpers/token.storage';
import { ApplicationUser } from '@/models';

class AccountService {
  private API_URL = '/connect/';

  get user(): ApplicationUser | null {
    const item = localStorage.getItem('user');
    return item ? (JSON.parse(item) as ApplicationUser) : null;
  }

  currentUserName(): string {
    if (this.user) {
      return this.user.FullName !== null && this.user.FullName !== ''
        ? this.user.FullName
        : this.user.UserName;
    } else {
      return '';
    }
  }

  set user(info: ApplicationUser | null) {
    localStorage.setItem('user', JSON.stringify(info));
  }

  async login(username: string, password: string): Promise<AccessToken> {
    const tokenData = {
      grant_type: 'password',
      username,
      password,
      client_id: 'vue.client',
      scope: 'api1 offline_access',
    };

    const response = await axios.post<any, AxiosResponse<AccessToken>>(
      this.API_URL + 'token',
      qs.stringify(tokenData),
      {
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded',
        },
      }
    );

    tokenStorage.set(response.data);
    return response.data;
  }

  logout() {
    tokenStorage.clear();
  }

  async refresh(): Promise<AccessToken> {
    const accessToken = tokenStorage.get();
    if (!accessToken) {
      return Promise.reject('Token not found');
    }

    const tokenData = {
      grant_type: 'refresh_token',
      client_id: 'vue.client',
      scope: 'api1 offline_access',
      refresh_token: accessToken.refresh_token,
    };

    const response = await axios.post<any, AxiosResponse<AccessToken>>(
      this.API_URL + 'token',
      qs.stringify(tokenData),
      {
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded',
        },
      }
    );

    tokenStorage.set(response.data);
    return response.data;
  }
}

export const accountService = new AccountService();
