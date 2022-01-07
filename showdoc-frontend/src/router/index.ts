import { createRouter, createWebHashHistory } from "vue-router";
import { routes } from "./routes";
import NProgress from "nprogress";
import "nprogress/nprogress.css";
import userInfo, { setUserInfo } from "@/hooks/userInfo";
import { IUserInfo } from "@/types/userInfo/login";
import { MetaData } from "@/types/router";
import { default as token } from "@/hooks/token";
import { userInfoApi } from "@/api/user";

NProgress.configure({
  showSpinner: false,
});

const router = createRouter({
  history: createWebHashHistory(),
  routes,
});

router.beforeEach(async (to, from, next) => {
  NProgress.start();
  const meta = to.meta as MetaData;
  if (meta.title) {
    document.title = meta.title;
  } else {
    document.title = to.name?.toString() || "";
  }
  if (meta.requireAuth) {
    // 如果token有值，则发起请求拿个人信息
    if (
      token.token.value.length > 0 &&
      token.userID.value > 0 &&
      userInfo.userID.value == 0
    ) {
      const res = await userInfoApi<IUserInfo>();
      setUserInfo(res.data);
    }
    if (userInfo.userID.value <= 0 || token.token.value.length == 0) {
      next("/login");
    } else {
      next();
    }
  } else {
    next();
  }
});

router.afterEach(() => {
  NProgress.done();
});

export default router;
