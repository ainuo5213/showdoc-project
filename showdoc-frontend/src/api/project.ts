import { IDataResult } from "@/types/data";
import {
  ICreateEntityForm,
  IDeleteEntityForm,
  IProjectItem,
  IMoveEntityForm,
  IRenamEntityForm
} from "@/types/project";
import axios from "@/utils/http";

export async function createFolderOrProject(
  form: ICreateEntityForm
): Promise<IDataResult<IProjectItem>> {
  return await axios.post("/api/project/create", form);
}

export async function deleteFolderOrProject(
  form: IDeleteEntityForm
): Promise<IDataResult<boolean>> {
  return await axios.post("/api/project/delete", form);
}

export async function moveFolderOrProject(
  form: IMoveEntityForm
): Promise<IDataResult<boolean>> {
  return await axios.post("/api/project/move", form);
}

export async function renamFolderOrProject(
  form: IRenamEntityForm
): Promise<IDataResult<boolean>> {
  return await axios.post("/api/project/rename", form);
}
