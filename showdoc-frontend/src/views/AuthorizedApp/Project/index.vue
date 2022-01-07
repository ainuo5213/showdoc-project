<template>
  <div class="document-container">
    <el-row class="row-container">
      <el-col :span="4" class="col-left">
        <search @search="onSearch"></search>
        <tree :data="data"></tree>
      </el-col>
      <el-col :span="12" class="col-mid">
        <detail></detail>
      </el-col>
      <el-col :span="3" class="col-right">
        <side-area></side-area>
      </el-col>
    </el-row>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent } from "vue-demi";
import Search from "@/components/Search/index.vue";
import { DocumentTypeEnums, IDocumentTreeData } from "@/types/document";
import Tree from "./components/Tree.vue";
import Detail from "./components/Detail.vue";
import SideArea from "./components/SideArea.vue";
import { default as documentState } from "@/hooks/detail";
const sourceData: IDocumentTreeData[] = [
  {
    projectID: 0,
    name: "1",
    type: DocumentTypeEnums.Document,
    parentID: 0,
    sortTime: "",
    objectID: 1,
  },
  {
    projectID: 0,
    name: "2",
    type: DocumentTypeEnums.Folder,
    parentID: 0,
    sortTime: "",
    objectID: 2,
  },
  {
    projectID: 0,
    name: "3",
    type: DocumentTypeEnums.Document,
    parentID: 2,
    sortTime: "",
    objectID: 3,
  },
];

export default defineComponent({
  components: {
    Search,
    Tree,
    Detail,
    SideArea,
  },
  setup() {
    const onSearch = (value: string) => {
      console.log(value);
    };
    const treeData = computed(() => {
      const newData = [...sourceData];
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
    console.log(treeData);
    return {
      onSearch,
      data: treeData,
      documentState,
    };
  },
});
</script>

<style lang="less" scoped>
.document-container {
  height: 100%;
  box-sizing: border-box;
  padding-top: 80px;

  .row-container {
    height: 100%;
    width: 80%;
    margin: 0 auto;
  }
}

.search-container {
  width: 100%;
}

.col-left {
  max-width: 360px;
  margin-right: 20px;
}

.col-mid {
  margin-right: 20px;
}
</style>
