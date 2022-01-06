<template>
  <el-tree class="tree-container scrollbar" :data="data" :props="construct">
    <template #default="{ node, data }">
      <span class="custom-tree-node">
        <span>{{ node.label }}</span>
        <img
          v-if="data.type === folder"
          class="icon"
          :class="{
            expanded: node.expanded,
          }"
          :src="require('@/assets/img/arrawRight.png')"
        />
      </span>
    </template>
  </el-tree>
</template>

<script lang="ts">
import { IDocumentTreeData, DocumentTypeEnums } from "@/types/document";
import { computed, defineComponent, PropType } from "vue-demi";

export default defineComponent({
  name: "Tree",
  props: {
    data: {
      type: Array as PropType<IDocumentTreeData[]>,
      required: true,
    },
  },
  setup() {
    return {
      folder: DocumentTypeEnums.Folder,
      construct: {
        children: "children",
        label: "name",
      },
    };
  },
});
</script>

<style lang="less" scoped>
.tree-container {
  width: 100%;
  height: 100%;
  overflow: auto;
  box-sizing: border-box;
  background: #fff;
  margin-top: 20px;

  ::v-deep(.el-tree-node) {
    @height: 40px;
    height: @height;
    .el-tree-node__content {
      height: @height;
      line-height: @height;

      &:hover {
        background: darken(#f5f7fa, 5%);
        transition: 0.2s;
      }
    }

    .el-tree-node__expand-icon {
      display: none;
    }

    .custom-tree-node {
      width: 100%;
      box-sizing: border-box;
      padding: 0 20px;
      display: flex;
      justify-content: space-between;
      align-items: center;
    }

    .icon {
      width: 16px;
      height: 16px;
      transform: rotate(0deg);
      transition: all 0.5s;

      &.expanded {
        transform: rotate(90deg);
      }
    }
  }
}
</style>
