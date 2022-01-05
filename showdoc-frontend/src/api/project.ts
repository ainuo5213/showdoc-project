import { IDataResult } from "@/types/data";
import { ICreateEntityForm, IDeleteEntityForm, IProjectItem } from "@/types/project";
import axios from "@/utils/http";

export async function createFolderOrProject(form: ICreateEntityForm): Promise<IDataResult<IProjectItem>> {
  return await axios.post("/api/project/create", form);
}


export async function deleteFolderOrProject(form: IDeleteEntityForm): Promise<IDataResult<boolean>> {
    return await axios.post("/api/project/delete", form);
  }
  