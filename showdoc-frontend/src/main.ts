import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import "@/assets/font/iconfont.css";
import ElmentPlus from "element-plus";
import "@/assets/normalize.css";
import "@/assets/common.css";
import dayjs from "dayjs";
(window as any).dayjs = dayjs;

window.oncontextmenu = function (e: MouseEvent) {
  e.preventDefault();
};

createApp(App).use(router).use(ElmentPlus).mount("#app");
