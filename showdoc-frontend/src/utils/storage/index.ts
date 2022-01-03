const KEY = "info";
import { IToken } from "@/types/userInfo/login";
export function getToken(): IToken {
  const info = localStorage.getItem(KEY);
  let tokenInfo = {
    token: "",
    userID: 0,
    expires: "",
  };
  if (info) {
    try {
      tokenInfo = JSON.parse(info);
    } catch (error) {
      // nothing
    }
  }
  return tokenInfo;
}

export function setToken(token: IToken) {
  localStorage.setItem(KEY, JSON.stringify(token));
}
