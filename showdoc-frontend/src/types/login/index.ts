export interface ILoginForm {
  cellphone: string;
  password: string;
}

export interface ILoginResult {
  expires: string;
  headImg: string;
  token: string;
  userID: number;
  username: string;
}
