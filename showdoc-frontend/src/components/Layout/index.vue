<template>
  <main class="main">
    <header class="header">
      <div class="avatar-menu">
        <span class="greet"
          >Hi: {{ username }}<span>{{ greetHello() }}</span></span
        >
        <el-dropdown>
          <span
            class="el-dropdown-link"
            :style="{
              cursor: $route.path === '/home' ? 'default' : 'pointer',
            }"
            @click="$router.push({ name: 'home' })"
          >
            <el-avatar
              fit="cover"
              shape="square"
              :size="40"
              :src="headImg || require('@/assets/img/avatar.jpg')"
            ></el-avatar>
          </span>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item @click="$router.push({ name: 'userInfo' })">
                <icon :size="20" name="gerenxinxi-"></icon>
                <span>我的信息</span>
              </el-dropdown-item>
              <el-dropdown-item @click="$router.push({ name: 'invitation' })">
                <icon name="yaoqingma"></icon>
                <span>我的邀请</span>
              </el-dropdown-item>
              <el-dropdown-item @click="onLogout">
                <icon name="logout"></icon>
                <span>退出登录</span>
              </el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
      </div>
    </header>
    <!-- 子路由出口 -->
    <router-view></router-view>
  </main>
</template>

<script lang="ts">
import { defineComponent } from "vue-demi";
import { default as userInfo, setUserInfo } from "@/hooks/userInfo";
import { clearToken } from "@/hooks/token";
import {} from "@/hooks/token";
import { greetHello } from "@/utils/time";
import Icon from "@/components/Icon/index.vue";
import { useRouter } from "vue-router";

export default defineComponent({
  components: {
    Icon,
  },
  setup() {
    const router = useRouter();
    const onLogout = () => {
      setUserInfo({
        userID: 0,
        username: "",
        email: "",
        cellphone: "",
        headImg: "",
      });
      clearToken();
      router.push({ name: "login" });
    };
    return {
      ...userInfo,
      greetHello,
      onLogout,
    };
  },
});
</script>

<style lang="less" scoped>
.main {
  width: 70%;
  height: 100%;
  margin: 0 auto;
  box-sizing: border-box;
  padding: 20px;
  background: #fff;
  display: flex;
  flex-direction: column;
  .header {
    width: 100%;
    height: 40px;
    position: relative;
    .avatar-menu {
      position: absolute;
      right: 0;
      display: flex;
      align-items: center;
      .greet {
        font-size: 13px;
        margin-right: 20px;
      }
    }
  }
}
::v-deep(.el-dropdown-menu__item) {
  width: 90px;
  display: flex;
  align-items: center;
  justify-content: start;
}
</style>
