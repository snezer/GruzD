export interface AccessToken {
  access_token: string;
  expires_in: number;
  refresh_token: string;
  scope: string;
  token_type: string;
}

export const tokenStorage = {
  set,
  get,
  clear,
};

const key = 'userToken';

function set(userData: AccessToken) {
  if (userData.access_token) {
    localStorage.setItem(key, JSON.stringify(userData));
  }
}

function get(): AccessToken | null {
  const item = localStorage.getItem(key);
  if (!item) {
    return null;
  }

  const user = JSON.parse(item) as AccessToken;

  if (user && user.access_token) {
    return user;
  }

  return null;
}

function clear() {
  localStorage.removeItem(key);
}
