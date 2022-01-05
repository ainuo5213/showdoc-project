<template>
  <el-card class="card-center" shadow="always">
    <el-form :model="form" status-icon :rules="rules" ref="formRef">
      <h2 class="header">忘记密码</h2>
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
      <el-form-item prop="confirmPassword">
        <el-input
          type="password"
          placeholder="确认密码"
          v-model="form.confirmPassword"
          maxlength="18"
        ></el-input>
      </el-form-item>
      <el-form-item prop="verifyCode">
        <div class="verify-group">
          <el-input v-model="form.verifyCode"></el-input>
          <el-button
            type="primary"
            :disabled="!verifyCodeDisabled || verifyBtnLoading"
            @click.prevent="sendVerifyCode"
            :loading="verifyBtnLoading"
          >
            <span v-show="!verifyBtnLoading">发送验证码</span>
            <span v-show="verifyBtnLoading">{{ delay }}s</span>
          </el-button>
        </div>
      </el-form-item>
      <el-form-item>
        <el-button
          class="btn"
          type="primary"
          @click="submitForm"
          :disabled="submitFormDisabled"
          :loading="submitFormLoading"
          >修改密码</el-button
        >
      </el-form-item>
      <el-form-item class="footer">
        <router-link to="/login">想起密码了？去登录</router-link>
      </el-form-item>
    </el-form>
  </el-card>
</template>

<script lang="ts">
import { useRequest } from "@/hooks/useFunction";
import { defineComponent, reactive, ref, computed } from "vue-demi";
import { ElMessage } from "element-plus";
import { useRouter } from "vue-router";

export default defineComponent({
  name: "ResetPassword",
  setup() {
    const formRef = ref();
    const {
      loading: verifyBtnLoading,
      delay,
      request: sendVerifyCodeRequest,
    } = useRequest<boolean>(60);

    const { loading: submitFormLoading, request: submitFormRequest } =
      useRequest<boolean>();
    const router = useRouter();

    const state = reactive({
      form: {
        cellphone: "",
        password: "",
        confirmPassword: "",
        verifyCode: "",
      },
      rules: {
        cellphone: [
          { required: true, message: "请输入手机号", trigger: "blur" },
        ],
        password: [{ validator: validatePass, trigger: "blur" }],
        confirmPassword: [{ validator: validateConfirmPass, trigger: "blur" }],
        verifyCode: [
          { required: true, message: "请输入验证码", trigger: "blur" },
        ],
      },
    });
    function validatePass(rule: any, value: any, callback: any) {
      if (value === "") {
        callback(new Error("请输入密码"));
      } else {
        if (state.form.confirmPassword !== "") {
          formRef.value.validateField("checkPass", () => null);
        }
        callback();
      }
    }
    function validateConfirmPass(rule: any, value: any, callback: any) {
      if (value === "") {
        callback(new Error("请输入确认密码"));
      } else if (value !== state.form.password) {
        callback(new Error("密码和确认密码不一致"));
      } else {
        callback();
      }
    }

    const verifyCodeDisabled = computed(() =>
      /^[1][3,4,5,7,8][0-9]{9}$/.test(state.form.cellphone)
    );

    const submitFormDisabled = computed(() => {
      return (
        state.form.cellphone.length == 0 ||
        state.form.password.length == 0 ||
        state.form.confirmPassword.length == 0 ||
        state.form.verifyCode.length == 0
      );
    });

    async function sendVerifyCode() {
      if (state.form.cellphone.length == 0) {
        return;
      }
      const res = await sendVerifyCodeRequest({
        url: "/api/auth/message",
        method: "post",
        data: {
          cellphone: state.form.cellphone,
          type: 1,
        },
      });
      if (res.errno == 0 && res.data) {
        ElMessage({
          type: "success",
          message: "发送验证码成功",
        });
      } else {
        ElMessage({
          type: "error",
          message: res.errmsg,
        });
      }
    }
    async function submitForm() {
      let res = await submitFormRequest({
        url: "/api/auth/passforget",
        method: "post",
        data: {
          password: state.form.password,
          cellphone: state.form.cellphone,
          verifyCode: state.form.verifyCode,
        },
      });
      if (res.errno == 0 && res.data) {
        ElMessage({
          type: "success",
          message: "修改密码成功",
        });
        router.push({ name: "login" });
      } else {
        ElMessage({
          type: "error",
          message: res.errmsg,
        });
      }
    }
    return {
      form: state.form,
      rules: state.rules,
      formRef,
      submitForm,
      sendVerifyCode,
      verifyCodeDisabled,
      verifyBtnLoading,
      delay,
      submitFormDisabled,
      submitFormLoading,
    };
  },
});
</script>

<style lang="less" scoped>
@import url("../Login/common.less");
.card-center {
  width: 380px;
}
.verify-group {
  display: flex;
  .el-button {
    margin-left: 10px;
    width: 112px;
  }
}
</style>
