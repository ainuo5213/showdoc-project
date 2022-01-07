import { IProjectItem, ProjectItemEnums } from "@/types/project";
import dayjs from "dayjs";
import { reactive, readonly, toRefs } from "vue";

export const source: {
  data: IProjectItem[];
  namedEntity: IProjectItem;
} = reactive({
  data: [],
  namedEntity: {
    name: "",
    objectID: 0,
    type: ProjectItemEnums.None,
    sortTime: dayjs().format("YYYY-MM-DD HH-mm-ss"),
    createTime: dayjs().format("YYYY-MM-DD HH-mm-ss"),
    parentID: 0,
    userID: 0,
  },
});
const state = toRefs(readonly(source));

export default state;

export const setData = (data: IProjectItem[]) => {
  source.data = data;
};

export const appendData = (data: IProjectItem) => {
  source.data.unshift(data);
};

export const removeData = (objectID: number, type: ProjectItemEnums) => {
  const index = source.data.findIndex(
    (r) => r.objectID == objectID && r.type == type
  );
  if (index >= 0) {
    source.data.splice(index, 1);
  }
};

export const setRenamingData = (data: IProjectItem) => {
  source.namedEntity.createTime = data.createTime;
  source.namedEntity.name = data.name;
  source.namedEntity.objectID = data.objectID;
  source.namedEntity.parentID = data.parentID;
  source.namedEntity.sortTime = data.sortTime;
  source.namedEntity.type = data.type;
  source.namedEntity.userID = data.userID;
};

export const cancelRenaming = () => {
  setRenamingData({
    name: "",
    objectID: 0,
    type: ProjectItemEnums.None,
    sortTime: dayjs().format("YYYY-MM-DD HH-mm-ss"),
    createTime: dayjs().format("YYYY-MM-DD HH-mm-ss"),
    parentID: 0,
    userID: 0,
  });
};

export const renameData = (name: string) => {
  if (source.namedEntity.objectID > 0) {
    const entity = source.data.find(
      (r) =>
        r.objectID == source.namedEntity.objectID &&
        r.type == source.namedEntity.type
    );
    if (entity != null) {
      entity.name = name;
      cancelRenaming();
    }
  }
};
