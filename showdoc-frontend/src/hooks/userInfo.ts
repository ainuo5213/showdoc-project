import { IUserInfo } from "@/types/userInfo/login";
import { reactive, readonly, toRefs, watch } from "vue";

export const data = reactive({
  userID: 0,
  username: "",
  email: "",
  cellphone: "",
  headImg: "",
});
const state = toRefs(readonly(data));

export default state;
export const setUserInfo = (userInfo: IUserInfo) => {
  data.userID = userInfo.userID;
  data.username = userInfo.username;
  data.email = userInfo.email;
  data.cellphone = userInfo.cellphone;
  data.headImg = userInfo.headImg;
};

export const clearUserInfo = () => {
  data.userID = 0;
  data.username = "";
  data.email = "";
  data.cellphone = "";
  data.headImg = "";
};
