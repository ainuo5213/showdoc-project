import { getDocumentContent } from "@/api/document";
import { IDetailData, IDocumentTreeData } from "@/types/document";
import { reactive, readonly, toRefs, computed, watch } from "vue";
const source: IDetailData = reactive({
  documentID: 0,
  searchValue: "",
  menus: [],
  document: {
    content: "",
    createTime: "",
    creator: "",
    documentID: 0,
    title: "",
    projectName: "",
    parentID: 0,
    folderID: 0,
    folderName: "",
  },
});

const state = toRefs(readonly(source));

watch(state.documentID, async (value) => {
  if (value != 0) {
    const res = await getDocumentContent(source.documentID);
    if (res.errno == 0) {
      source.document = res.data;
    }
  } else {
    source.document = {
      content: "",
      createTime: "",
      creator: "",
      documentID: 0,
      title: "",
      projectName: "",
      parentID: 0,
      folderID: 0,
      folderName: "",
    };
  }
});

export const treeData = computed(() => {
  const newData = [...source.menus];
  const treed = [];
  while (newData.length > 0) {
    const data = newData.shift();
    if (!data) {
      continue;
    }
    const children = newData.filter((r) => r.parentID === data.objectID);
    children.forEach((r) => {
      newData.splice(newData.findIndex((p) => p.objectID === r.objectID));
    });
    data.children = children;
    treed.push(data);
  }

  return treed;
});

export default state;

export const setCurrentDocument = (documentID: number) => {
  source.documentID = documentID;
};

export const setSearchValue = (searchValue: string) => {
  source.searchValue = searchValue;
};

export const setMenus = (menus: IDocumentTreeData[]) => {
  source.menus = menus;
};
