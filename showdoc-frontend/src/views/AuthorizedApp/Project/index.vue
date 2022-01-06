<template>
  <div class="document-container">
    <el-row>
      <el-col :span="6" class="col-left">
        <search @search="onSearch"></search>
        <tree :data="data"></tree>
      </el-col>
      <el-col :span="18"></el-col>
    </el-row>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent } from "vue-demi";
import Search from "@/components/Search/index.vue";
import { DocumentTypeEnums, IDocumentTreeData } from "@/types/document";
import Tree from "./components/Tree.vue";
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
    };
  },
});
</script>

<style lang="less" scoped>
.document-container {
  height: 100%;

  .el-row {
    height: 100%;
  }
}

.search-container {
  width: 100%;
}

.col-left {
  max-width: 360px;
}
</style>
