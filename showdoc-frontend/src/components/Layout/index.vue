<template>
  <main class="main">
    <header class="header">
      <div class="avatar-menu">
        <span class="greet"
          >Hi: {{ username }}<span>{{ greetHello() }}</span></span
        >
        <el-dropdown trigger="click">
          <span class="el-dropdown-link">
            <el-avatar
              fit="cover"
              shape="square"
              :size="40"
              :src="headImg || require('@/assets/img/avatar.jpg')"
            ></el-avatar>
          </span>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item>
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
      // TODO: logout api
      router.push({ name: "login" });
    };
    return {
      ...userInfo,
      greetHello,
      onLogout
    };
  },
});
</script>

<style lang="less" scoped>
.main {
  width: 100%;
  height: 100%;
  box-sizing: border-box;
  padding: 20px;
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
</style>
