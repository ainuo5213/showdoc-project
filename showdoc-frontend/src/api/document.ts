import { IDataResult } from "@/types/data";
import {
  DocumentTypeEnums,
  IDocumentData,
  IUpdateDocumentForm,
  IDocumentTreeData
} from "@/types/document";
import axios from "@/utils/http";

export async function createFolderOrDocument(
  form: IUpdateDocumentForm
): Promise<IDataResult<IDocumentData>> {
  return await axios.post("/api/document/create", {
    ...form,
    type: DocumentTypeEnums.Document,
  });
}

export async function updateDocument(
  form: IUpdateDocumentForm
): Promise<IDataResult<IDocumentData>> {
  return await axios.post("/api/document/update", form);
}

export async function getMenus(
  projectID: number
): Promise<IDataResult<IDocumentTreeData[]>> {
  return await axios.get("/api/document/menu", {
    params: {
      projectID,
    },
  });
}

export async function getDocumentContent(
  documentID: number
): Promise<IDataResult<IDocumentData>> {
  return await axios.get("/api/document/content", {
    params: {
      documentID,
    },
  });
}