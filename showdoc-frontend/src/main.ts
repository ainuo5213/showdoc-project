import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import "@/assets/font/iconfont.css";
import ElmentPlus from "element-plus";
import "element-plus/dist/index.css";
import "@/assets/normalize.css";
import "@/assets/common.css";

createApp(App).use(router).use(ElmentPlus).mount("#app");
