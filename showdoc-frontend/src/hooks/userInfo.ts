import { IUserInfo } from "@/types/userInfo/login";
import { reactive, readonly, toRefs, watch } from "vue";

const data = reactive({
  userID: 2,
  username: "",
  email: "",
  cellphone: "",
  headImg: "",
  time: +new Date(),
});
console.log(data);
const state = toRefs(readonly(data));
watch(data, (value) => {
  console.log(value);
});
export default state;
export const setUserInfo = (userInfo: IUserInfo) => {
  data.userID = userInfo.userID;
  data.username = userInfo.username;
  data.email = userInfo.email;
  data.cellphone = userInfo.cellphone;
  data.headImg = userInfo.headImg;
};
