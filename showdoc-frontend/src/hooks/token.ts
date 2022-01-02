import { IToken } from "@/types/userInfo/login";
import { getToken, setToken as setStorageToken } from "@/utils/storage";
import { reactive, readonly, toRefs, watchEffect } from "vue";
import dayjs from "dayjs";

export function useToken() {
  const tokenInfo = getToken();
  const source = reactive({
    token: tokenInfo.token || "",
    expires: tokenInfo.expires || dayjs().format("YYYY-MM-DD"),
    userID: tokenInfo.userID || 0,
  });
  const state = toRefs(readonly(source));
  const setToken = (data: IToken) => {
    source.token = data.token;
    source.expires = data.expires;
    source.userID = data.userID;
  };
  watchEffect(() => {
    setStorageToken(source);
  });

  return {
    setToken,
    ...state,
  };
}
