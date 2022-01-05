<template>
  <el-card class="card-center" shadow="always">
    <el-form :model="form" status-icon :rules="rules" ref="formRef">
      <h2 class="header">登录</h2>
      <el-form-item prop="cellphone">
        <el-input
          placeholder="手机号"
          v-model="form.cellphone"
          maxlength="11"
          oninput="value = value.replace(/^\.+|[^\d.]/g, '')"
        ></el-input>
      </el-form-item>
      <el-form-item prop="password">
        <el-input
          type="password"
          placeholder="密码"
          v-model="form.password"
          maxlength="18"
        ></el-input>
      </el-form-item>
      <el-form-item>
        <el-button
          :disabled="disabled"
          class="btn"
          type="primary"
          @click="submitForm"
          :loading="loading"
          >登录</el-button
        >
      </el-form-item>
      <el-form-item class="footer">
        <router-link to="/register">注册新账号</router-link>
        &nbsp;&nbsp;&nbsp;
        <router-link to="/resetPassword">忘记密码</router-link>
      </el-form-item>
    </el-form>
  </el-card>
</template>

<script lang="ts">
import { computed, defineComponent, reactive, ref } from "vue-demi";
import { useRequest } from "@/hooks/useFunction";
import { setToken } from "@/hooks/token";
import { ILoginResult } from "@/types/login";
import { IDataResult } from "@/types/data";
import { useRouter } from "vue-router";
import { ElMessage } from "element-plus";

export default defineComponent({
  name: "Login",
  setup() {
    const formRef = ref();
    const router = useRouter();
    const { loading, request } = useRequest<ILoginResult>();
    const state = reactive({
      form: {
        cellphone: "",
        password: "",
      },
      rules: {
        password: [{ required: true, message: "请输入密码", trigger: "blur" }],
        cellphone: [
          { required: true, message: "请输入手机号", trigger: "blur" },
        ],
      },
    });
    const disabled = computed(() => {
      return (
        state.form.cellphone.length == 0 || state.form.password.length == 0
      );
    });
    const submitForm = async () => {
      let res: IDataResult<ILoginResult> = await request({
        url: "/api/auth/login",
        method: "POST",
        data: state.form,
      });
      if (res.errno == 0) {
        setToken({
          userID: res.data.userID,
          token: res.data.token,
          expires: res.data.expires,
        });
        ElMessage({
          type: "success",
          message: "登陆成功",
        });
        router.push({ name: "home" });
      } else {
        ElMessage({
          type: "error",
          message: res.errmsg,
        });
      }
    };
    return {
      form: state.form,
      rules: state.rules,
      formRef,
      submitForm,
      disabled,
      loading,
    };
  },
});
</script>

<style lang="less" scoped>
@import url("./common.less");
</style>
