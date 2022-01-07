export enum DocumentTypeEnums {
  None = -1,
  Document = 0,
  Folder = 1,
}

export interface IDocumentTreeData {
  projectID: number;
  name: string;
  objectID: number;
  parentID: number;
  type: DocumentTypeEnums;
  sortTime: string;
  children?: IDocumentTreeData[];
}

export interface IDetailData {
  documentID: number;
  searchValue: string;
  menus: IDocumentTreeData[];
  document: IDocumentData;
}

export interface IDocumentData {
  documentID: number;
  title: string;
  content: string;
  createTime: string;
  creator: string;
  projectName: string;
  parentID: number;
  folderID: number;
  folderName: string;
}


export interface IUpdateDocumentForm {
  projectID: number;
  documentID: number;
  title: string;
  content: string;
  folderID: number;
}

export interface IFolderItemData {
  parentID: number;
  name: string;
  folderID: number;
  children?: IFolderItemData[];
}