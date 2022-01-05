import { IProjectItem } from "@/types/project";
import { reactive, readonly, toRefs } from "vue";

export const source: {
  data: IProjectItem[];
} = reactive({
  data: [],
});
const state = toRefs(readonly(source));

export default state;

export const setData = (data: IProjectItem[]) => {
  source.data = data;
};

export const appendData = (data: IProjectItem) => {
  source.data.unshift(data);
};

export const removeData = (objectID: number) => {
  const index = source.data.findIndex((r) => r.objectID == objectID);
  if (index >= 0) {
    source.data.splice(index, 1);
  }
};
