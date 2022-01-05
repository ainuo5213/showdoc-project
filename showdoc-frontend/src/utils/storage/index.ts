const KEY = "info";
const FOLDER = "folders";
import { IFolderHistory } from "@/types/folder";
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

export function setFolder(folders: IFolderHistory[]) {
  localStorage.setItem(FOLDER, JSON.stringify(folders));
}

export function getFolders(): IFolderHistory[] {
  const info = localStorage.getItem(FOLDER);
  let folders: IFolderHistory[] = [];
  if (info?.length) {
    try {
      folders = JSON.parse(info);
    } catch (error) {
      // nothing
    }
  }
  return folders;
}
