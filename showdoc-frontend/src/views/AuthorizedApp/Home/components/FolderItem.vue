<template>
  <div class="folder-container">
    <el-card
      shadow="hover"
      :body-style="{
        border: 'none',
        padding: '0px',
        height: '100%',
        display: 'flex',
        'flex-direction': 'column',
      }"
    >
      <div class="folder">
        <img :src="require('@/assets/img/folder.png')" />
      </div>
      <div class="title-container">
        <span class="title">{{ data.name }}</span>
      </div>
    </el-card>
  </div>
</template>

<script lang="ts">
import { IProjectItem } from "@/types/project";
import { defineComponent, PropType } from "vue-demi";

export default defineComponent({
  name: "Folder",
  props: {
    data: {
      type: Object as PropType<IProjectItem>,
      required: true,
    },
  },
  setup(props) {
    return {};
  },
});
</script>

<style lang="less" scoped>
.folder-container {
  width: 180px;
  height: 120px;
  padding-bottom: 10px;
  .el-card {
    height: 100%;
    border: none;
    cursor: pointer;
  }
  .folder {
    height: 75%;
    text-align: center;
    img {
      width: 50%;
    }
  }
  .title-container {
    padding: 5px;
    text-align: center;
    .title {
      font-size: 12px;
    }
  }
}
</style>
