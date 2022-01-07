import axios from "axios";
import { isDev } from "../isFunction";
import { default as token } from "@/hooks/token";

let url;
if (isDev()) {
  url = "http://localhost:9000/";
} else {
  // production
  url = "http://www.example.com/";
}

const instance = axios.create({
  baseURL: url,
  timeout: 10000,
});
instance.interceptors.request.use((config) => {
  if (token.token.value.length > 0) {
    config.headers = {
      Authorization: `Bearer ${token.token.value}`,
      "Content-Type": "application/json; charset=utf-8",
    };
  }
  return config;
});

instance.interceptors.response.use(
  (response) => {
    // 401：Unauthorize
    if (response.status === 401) {
      history.pushState(null, "/login");
      return Promise.reject("未登录授权");
    }
    return response.data;
  },
  (error) => {
    return Promise.reject("请求失败");
  }
);

export default instance