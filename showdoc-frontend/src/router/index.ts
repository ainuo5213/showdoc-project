import { createRouter, createWebHashHistory } from "vue-router";
import { routes } from "./routes";
import NProgress from "nprogress";
import "nprogress/nprogress.css";
import userInfo from "@/hooks/userInfo";
import { MetaData } from "@/types/router";

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
    if (userInfo.userID.value <= 0) {
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
