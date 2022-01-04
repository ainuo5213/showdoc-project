import { IDataResult } from "@/types/data";
import axios from "@/utils/http";

export async function userInfoApi<IUserInfo>(): Promise<IDataResult<IUserInfo>> {
  const res: any = await axios.get("/api/user/info");
  return res as IDataResult<IUserInfo>;
}
