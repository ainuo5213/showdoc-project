<template>
  <search @search="onSearch"></search>
  <home-list :data="data"></home-list>
</template>

<script lang="ts">
import { computed, defineComponent, reactive, toRefs } from "vue-demi";
import HomeList from "./components/HomeList.vue";
import Search from "./components/Search.vue";
import { IProjectItem, ProjectItemEnums } from "@/types/project";
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
  },{
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
  },{
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
  },{
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
];
export default defineComponent({
  components: {
    HomeList,
    Search,
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
    };
  },
});
</script>

<style></style>
