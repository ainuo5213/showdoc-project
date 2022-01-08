<template>
  <el-tree
    class="tree-container scrollbar"
    :data="treeData || []"
    :props="construct"
    @node-click="onNodeClick"
  >
    <template #default="{ node, data }">
      <span class="custom-tree-node">
        <div class="prefix">
          <icon
            :size="16"
            :name="data.type == 0 ? 'wendang' : 'wenjianjia_o'"
          ></icon>
          <span class="text elipsis">{{ node.label }}</span>
        </div>
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
import { defineComponent, PropType } from "vue-demi";
import Icon from "@/components/Icon/index.vue";
import { getMenus } from "@/api/document";
import { useRoute, useRouter } from "vue-router";
import { treeData, setCurrentDocument, setMenus } from "@/hooks/detail";

export default defineComponent({
  name: "Tree",
  components: {
    Icon,
  },
  props: {
    data: {
      type: Array as PropType<IDocumentTreeData[]>,
      required: true,
    },
  },
  setup() {
    const route = useRoute();
    async function getProjectMenu() {
      let res = await getMenus(+route.params.projectID);
      if (res.errno == 0) {
        setMenus(res.data);
        if (
          treeData.value[0] &&
          treeData.value[0].type == DocumentTypeEnums.Document
        ) {
          setCurrentDocument(treeData.value[0].objectID);
        } else {
          setCurrentDocument(0);
        }
      }
    }
    getProjectMenu();

    const onNodeClick = (data: IDocumentTreeData, node?: any) => {
      if (data.type == DocumentTypeEnums.Document) {
        setCurrentDocument(data.objectID);
      }
    };
    return {
      folder: DocumentTypeEnums.Folder,
      construct: {
        children: "children",
        label: "name",
      },
      onNodeClick,
      treeData,
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

      .prefix {
        width: 80%;
        display: flex;
        align-items: center;

        .text {
          max-width: 160px;
        }
      }
    }

    .icon {
      width: 16px;
      transform: rotate(0deg);
      transition: all 0.5s;
      margin-right: 20px;

      &.expanded {
        transform: rotate(90deg);
      }
    }
  }
}
</style>
