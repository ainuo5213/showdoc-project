import { IDataResult } from "@/types/data";
import { DocumentTypeEnums, IDocumentData, IUpdateDocumentForm } from "@/types/document";
import axios from "@/utils/http";

export async function createFolderOrDocument(
  form: IUpdateDocumentForm
): Promise<IDataResult<IDocumentData>> {
  return await axios.post("/api/document/create", { ...form, type: DocumentTypeEnums.Document });
}

export async function updateDocument(
  form: IUpdateDocumentForm
): Promise<IDataResult<IDocumentData>> {
  return await axios.post("/api/document/update", form);
}
