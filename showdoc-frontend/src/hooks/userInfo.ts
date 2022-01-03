import { IUserInfo } from "@/types/userInfo/login";
import { reactive, readonly, toRefs, watch } from "vue";

export const data = reactive({
  userID: 2,
  username: "ainuo5213",
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
