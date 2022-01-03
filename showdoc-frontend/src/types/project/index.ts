export enum ProjectItemEnums {
  Folder = 0,
  Project = 1,
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
