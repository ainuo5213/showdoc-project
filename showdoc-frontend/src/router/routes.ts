export const routes = [
    {
        path: "/login",
        component: () => import("@/views/Unauthorized/Login/index.vue"),
    },
    {
        path: "/register",
        component: () => import("@/views/Unauthorized/Register/index.vue"),
        meta: {
            requireAuth: true
        }
    }
]