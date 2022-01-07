export enum ErrorNo {
  Failed = -1,
  Success = 0,
}

export interface IDataResult<T> {
  errno: ErrorNo;
  errmsg: string;
  data: T;
}
