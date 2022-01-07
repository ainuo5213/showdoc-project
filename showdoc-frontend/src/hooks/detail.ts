import {
    IDetailData,
  } from "@/types/document";
  import { reactive, readonly, toRefs } from "vue";
  const source: IDetailData = {
    isEditing: false
  };
  
  const data = reactive(source);
  const state = toRefs(readonly(data));
  export default state;
  
  