import { createRouter, createWebHashHistory } from "vue-router";
import { routes } from "./routes";
import NProgress from "nprogress";
import "nprogress/nprogress.css";
import userInfo from "@/hooks/userInfo";

const router = createRouter({
  history: createWebHashHistory(),
  routes,
});

router.beforeEach(async (to, from, next) => {
  NProgress.start();
  if (to.meta.requireAuth) {
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
