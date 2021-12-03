import { LOCAL_STORAGE_KEY } from '@/config';

const getStorageObj = async () => {
  try {
    const data = localStorage.getItem(LOCAL_STORAGE_KEY);
    if (data) {
      return JSON.parse(data) || {};
    }
  } catch (e) {
    //Ignore
  }
};

export const getFromLocalStorage = async (key: string) => {
  let ls = { key };
  if (localStorage) {
    const lsObj = await getStorageObj();
    ls = { ...ls, ...lsObj };
  }

  return ls[key];
};

export const saveToLocalStorage = async (key: string, value: any) => {
  if (localStorage) {
    const lsObj = await getStorageObj();
    localStorage.setItem(
      LOCAL_STORAGE_KEY,
      JSON.stringify({
        ...lsObj,
        [key]: value,
      })
    );
  }
};

export const reset = async () => {
  localStorage.removeItem(LOCAL_STORAGE_KEY);
};
