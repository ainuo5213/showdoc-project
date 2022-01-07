<template>
  <div class="document-container">
    <el-row class="row-container">
      <el-col :span="4" class="col-left">
        <search @search="onSearch"></search>
        <tree></tree>
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
import { defineComponent } from "vue-demi";
import Search from "@/components/Search/index.vue";
import Tree from "./components/Tree.vue";
import Detail from "./components/Detail.vue";
import SideArea from "./components/SideArea.vue";
import { default as documentState } from "@/hooks/detail";

export default defineComponent({
  components: {
    Search,
    Tree,
    SideArea,
    Detail,
  },
  setup() {
    const onSearch = (value: string) => {
      console.log(value);
    };

    return {
      onSearch,
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
