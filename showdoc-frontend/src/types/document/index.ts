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
  isEditing: boolean;
}
