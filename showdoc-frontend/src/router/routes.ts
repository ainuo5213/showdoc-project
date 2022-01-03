export const routes = [
  {
    path: "/login",
    name: "login",
    component: () => import("@/views/UnauthorizedApp/Login/index.vue"),
    meta: {
      title: "登录",
    },
  },
  {
    path: "/register",
    name: "register",
    component: () => import("@/views/UnauthorizedApp/Register/index.vue"),
    meta: {
      title: "注册",
    },
  },
  {
    path: "/",
    redirect: "/home",
    name: "home",
    component: () => import("@/components/Layout/index.vue"),
    children: [
      {
        path: "/home",
        name: "index",
        component: () => import("@/views/AuthorizedApp/Home/index.vue"),
        meta: {
          requireAuth: true,
          title: "我的项目",
        },
      },
    ],
  },
  {
    path: "/invitation",
    redirect: "/invitation/index",
    name: "invitation",
    component: () => import("@/components/Layout/index.vue"),
    children: [
      {
        path: "index",
        name: "invitation",
        component: () => import("@/views/AuthorizedApp/Invitation/index.vue"),
        meta: {
          requireAuth: true,
          title: "我的邀请",
        },
      },
    ],
  },
  {
    path: "/project",
    name: "project",
    component: () => import("@/views/AuthorizedApp/Project/index.vue"),
    meta: {
      requireAuth: true,
      title: "项目详情",
    },
  },
  {
    path: "/history",
    name: "history",
    component: () => import("@/views/AuthorizedApp/History/index.vue"),
    meta: {
      requireAuth: true,
      title: "文档历史",
    },
  },
];
