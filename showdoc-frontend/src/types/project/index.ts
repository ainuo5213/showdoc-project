export enum ProjectItemEnums {
  None = -1,
  Folder = 0,
  Project = 1,
  Space = 2,
}
export enum EntityMode {
  None = -1,
  Copy = 0,
  Cut = 1,
}

export interface IProjectItem {
  userID: number;
  createTime: string;
  name: string;
  objectID: number;
  parentID: number;
  sortTime: string;
  type: ProjectItemEnums;
}

export interface IClipBoard {
  mode?: EntityMode;
  clipBoardEntity?: IProjectItem;
}

export interface IContextMenuData {
  showContextMenu: boolean;
  contextmenuEvent?: MouseEvent;
  contxtMenuType: ProjectItemEnums;
  entity: IProjectItem;
  clipBoard: IClipBoard;
  selectEntity: IProjectItem;
}

export interface ICreateEntityForm {
  folderID: number;
  type: ProjectItemEnums;
  name: string;
}

export interface IDeleteEntityForm {
  objectID: number;
  type: ProjectItemEnums;
}

export interface IMoveEntityForm {
  folderID: number;
  type: ProjectItemEnums;
  objectID: number;
}

export interface IRenamEntityForm {
  name: string;
  type: ProjectItemEnums;
  objectID: number;
}
