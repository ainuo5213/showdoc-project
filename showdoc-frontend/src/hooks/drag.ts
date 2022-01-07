import { IProjectItem, ProjectItemEnums } from "@/types/project";
import dayjs from "dayjs";
import { reactive, readonly, toRefs, watch } from "vue";

export const source: {
  dragData: IProjectItem;
} = reactive({
  dragData: {
    userID: 0,
    objectID: 0,
    name: "",
    createTime: dayjs().format("YYYY-MM-DD HH-mm-ss"),
    sortTime: dayjs().format("YYYY-MM-DD HH-mm-ss"),
    parentID: 0,
    type: ProjectItemEnums.None,
  },
});
const state = toRefs(readonly(source));

export default state;
export const setDragData = (data: IProjectItem): void => {
  source.dragData = data;
};

export function removeDragData() {
  source.dragData = {
    userID: 0,
    objectID: 0,
    name: "",
    createTime: dayjs().format("YYYY-MM-DD HH-mm-ss"),
    sortTime: dayjs().format("YYYY-MM-DD HH-mm-ss"),
    parentID: 0,
    type: ProjectItemEnums.None,
  };
}
