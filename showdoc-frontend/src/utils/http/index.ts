import axios from "axios";
import { isDev } from "../isFunction";
import { useToken } from "@/hooks/token";

let url;
if (isDev()) {
  // dev is localhost
  url = "http://localhost:9000/";
} else {
  // production
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

instance.interceptors.response.use((response) => {
  // 401：Unauthorize
    // TODO: 处理异常情况
  if (response.status === 401) {
    history.pushState(null, "/login");
  }
  return response.data;
});
