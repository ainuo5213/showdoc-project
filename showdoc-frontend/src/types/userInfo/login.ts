export interface ILoginInfo {
  token: string;
  expires: string;
  userID: number;
  username: number;
  headImg: string;
}

export interface IToken {
  token: string;
  expires: string;
  userID: number;
}

export interface IUserInfo {
  userID: number;
  email: string;
  cellphone: string;
  username: string;
  headImg: string;
}
