import { IFolderHistory } from "@/types/folder";
import { getFolders, setFolder } from "@/utils/storage";
import { reactive, readonly, toRefs, watch } from "vue";

const folders = getFolders();

export const source: {
  folders: IFolderHistory[];
} = reactive({
  folders: folders,
});
const state = toRefs(readonly(source));

watch(source.folders, (value) => {
  setFolder(value);
});

export default state;
export const pushFolder = (folder: IFolderHistory): void => {
  source.folders.push(folder);
};

function findChildFolders(
  folders: IFolderHistory[],
  folderID: number
): IFolderHistory[] {
  const childFolders: IFolderHistory[] = [];
  let parentID: number = folderID;
  let cnt = folders.length - 1;
  while (cnt >= 0) {
    const childFolder = folders.find((r) => r.parentID == parentID);
    if (childFolder != null) {
      childFolders.push(childFolder);
      parentID = childFolder.folderID;
    } else {
      break;
    }
    cnt--;
  }

  return childFolders;
}

export const removeChildFolders = (folderID: number): void => {
  // 将其子元素删除
  const childFolders = findChildFolders(source.folders, folderID);
  for (let i = 0; i < childFolders.length; i++) {
    const element = childFolders[i];
    const index = source.folders.findIndex(
      (r) => r.folderID == element.folderID
    );
    source.folders.splice(index, 1);
  }
};

export const clearFolders = (): void => {
  // 将其子元素删除
  source.folders.length = 0;
};