<template>
  <search @search="onSearch"></search>
  <home-list :data="data"></home-list>
  <context-menu v-if="contextmenuData.showContextMenu.value"></context-menu>
</template>

<script lang="ts">
import ContextMenu from "@/components/contextmenu/index.vue";
import { defineComponent, reactive, toRefs } from "vue-demi";
import HomeList from "./components/HomeList.vue";
import Search from "./components/Search.vue";
import { IProjectItem, ProjectItemEnums } from "@/types/project";
import { default as contextmenuData } from "@/hooks/contextmenu";

const source: IProjectItem[] = [
  {
    userID: 1,
    createTime: "",
    name: "project1",
    objectID: 1,
    type: ProjectItemEnums.Project,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "folder1",
    objectID: 1,
    type: ProjectItemEnums.Folder,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "project1",
    objectID: 2,
    type: ProjectItemEnums.Project,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "folder1",
    objectID: 2,
    type: ProjectItemEnums.Folder,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "project1",
    objectID: 3,
    type: ProjectItemEnums.Project,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "folder1",
    objectID: 3,
    type: ProjectItemEnums.Folder,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "project1",
    objectID: 4,
    type: ProjectItemEnums.Project,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "folder1",
    objectID: 4,
    type: ProjectItemEnums.Folder,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "project1",
    objectID: 5,
    type: ProjectItemEnums.Project,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "folder1",
    objectID: 5,
    type: ProjectItemEnums.Folder,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "project1",
    objectID: 6,
    type: ProjectItemEnums.Project,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "folder1",
    objectID: 7,
    type: ProjectItemEnums.Folder,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "project1",
    objectID: 8,
    type: ProjectItemEnums.Project,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "folder1",
    objectID: 9,
    type: ProjectItemEnums.Folder,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "project1",
    objectID: 10,
    type: ProjectItemEnums.Project,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "folder1",
    objectID: 11,
    type: ProjectItemEnums.Folder,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "project1",
    objectID: 12,
    type: ProjectItemEnums.Project,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "folder1",
    objectID: 13,
    type: ProjectItemEnums.Folder,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "project1",
    objectID: 14,
    type: ProjectItemEnums.Project,
    sortTime: "",
    parentID: 0,
  },
  {
    userID: 1,
    createTime: "",
    name: "folder1",
    objectID: 15,
    type: ProjectItemEnums.Folder,
    sortTime: "",
    parentID: 0,
  },
];
export default defineComponent({
  components: {
    HomeList,
    Search,
    ContextMenu,
  },
  setup() {
    const sourceData = source;
    const state = reactive({
      data: source,
    });

    const onSearch = (searchValue: string) => {
      if (searchValue.length == 0) {
        state.data = sourceData;
      } else {
        state.data = sourceData.filter((r) => r.name.indexOf(searchValue) >= 0);
      }
    };
    return {
      onSearch,
      ...toRefs(state),
      contextmenuData,
    };
  },
});
</script>

<style></style>
