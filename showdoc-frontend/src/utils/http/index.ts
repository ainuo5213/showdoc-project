import axios from "axios";
import { isDev } from "../isFunction";

let url;
if (isDev()) {
    // dev is localhost
    url = "http://localhost:9000/";
}
else {
    // production
}

const instance = axios.create({
    baseURL: url,
    timeout: 10000,
});

instance.interceptors.request.use((resolve, reject) => {
    // getToken
});