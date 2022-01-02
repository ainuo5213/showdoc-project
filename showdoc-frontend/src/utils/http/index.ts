import axios from "axios";
import { isDev } from "../isFunction";
import { useToken } from "@/hooks/token";

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
  const { token } = useToken();
  if (token) {
    config.headers = {
      Authorization: `Bearer ${token}`,
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