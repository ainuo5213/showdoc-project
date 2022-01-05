import { IToken } from "@/types/userInfo/login";
import { getToken, setToken as setStorageToken } from "@/utils/storage";
import { reactive, readonly, toRefs, watch } from "vue";
import dayjs from "dayjs";

const tokenInfo = getToken();
export const source = reactive(tokenInfo);
const state = toRefs(readonly(source));

export default state;
export const setToken = (data: IToken) => {
  source.token = data.token;
  source.expires = data.expires;
  source.userID = data.userID;
};

export const clearToken = () => {
  source.token = "";
  source.expires = dayjs().toISOString();
  source.userID = 0;
};

console.log(state);
if (dayjs(tokenInfo.expires).isBefore(dayjs())) {
  clearToken();
}

watch(source, (value) => {
  setStorageToken(value);
});

// 每一个小时检查token是否过期，如果过期就清理token，token过期之后就发起请求就会报错，跳转到登录页
setInterval(() => {
  if (dayjs(tokenInfo.expires).isBefore(dayjs())) {
    clearToken();
  }
}, 1000 * 60 * 60);
