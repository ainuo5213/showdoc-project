import {
  IProjectItem,
  ProjectItemEnums,
  IContextMenuData,
  EntityMode,
} from "@/types/project";
import { reactive, readonly, toRefs } from "vue";
const originData: IContextMenuData = {
  showContextMenu: false,
  entity: {
    userID: 0,
    sortTime: "",
    createTime: "",
    name: "",
    objectID: 0,
    parentID: 0,
    type: ProjectItemEnums.None,
  },
  selectEntity: {
    userID: 0,
    sortTime: "",
    createTime: "",
    name: "",
    objectID: 0,
    parentID: 0,
    type: ProjectItemEnums.None,
  },
  clipBoard: {
    mode: EntityMode.None,
    clipBoardEntity: undefined,
  },
  contextmenuEvent: undefined,
  contxtMenuType: ProjectItemEnums.None,
};

export const data: IContextMenuData = reactive(originData);
const state = toRefs(readonly(data));
export default state;

// 打开右键（含选中）
export const setSelectEntity = (contextmenu: IProjectItem) => {
  data.selectEntity = contextmenu;
};

// 打开右键（含选中）
export const openContextMenuWithEntity = (
  contextmenu: IProjectItem,
  event?: MouseEvent,
  showContextMenu?: boolean,
  contxtMenuType?: ProjectItemEnums
) => {
  data.entity = contextmenu;
  data.selectEntity = contextmenu;
  data.contextmenuEvent = event;
  data.showContextMenu = !!showContextMenu;
  data.contxtMenuType =
    contxtMenuType == undefined ? ProjectItemEnums.None : contxtMenuType;
};

// 打开右键（不含选中）
export const openContextMenuWithoutEntity = (
  event?: MouseEvent,
  showContextMenu?: boolean,
  contxtMenuType?: ProjectItemEnums
) => {
  data.contextmenuEvent = event;
  data.showContextMenu = !!showContextMenu;
  data.contxtMenuType =
    contxtMenuType == undefined ? ProjectItemEnums.None : contxtMenuType;
};

// 关闭右键菜单
export const closeContextMenu = () => {
  data.showContextMenu = false;
  clearSelection();
};

// 右键选中的实体加入剪切板
export const copyToClipBard = (mode: EntityMode) => {
  data.clipBoard.clipBoardEntity = data.entity;
  data.clipBoard.mode = mode;
};

// 左键选中的实体加入剪切板
export const copyToClipBardMouseLeft = (mode: EntityMode) => {
  data.clipBoard.clipBoardEntity = data.selectEntity;
  data.clipBoard.mode = mode;
};

// 清理剪切板的数据
export const clearClipboard = (objectID?: number) => {
  if (
    objectID == undefined ||
    (objectID > 0 && data.clipBoard.clipBoardEntity?.objectID == objectID)
  ) {
    data.clipBoard.clipBoardEntity = undefined;
    data.clipBoard.mode = EntityMode.None;
  }
};

// 清除右键选中的实体的数据
export const clearSelection = () => {
  data.entity = {
    userID: 0,
    sortTime: "",
    createTime: "",
    name: "",
    objectID: 0,
    parentID: 0,
    type: ProjectItemEnums.None,
  };
};

// 清除左键选中的实体的数据
export const clearSelectedEntity = () => {
  data.selectEntity = {
    userID: 0,
    sortTime: "",
    createTime: "",
    name: "",
    objectID: 0,
    parentID: 0,
    type: ProjectItemEnums.None,
  };
};
