<template>
  <teleport to="body">
    <div
      @click.prevent.stop
      class="contextmenu-container"
      :style="{
        left: position.x + 'px',
        top: position.y + 'px',
      }"
    >
      <ul>
        <li v-show="showEntityOperation" @click.stop.prevent="onRename">
          重命名
        </li>
        <li v-show="showEntityOperation" @click.stop.prevent="onDelete">
          删除
        </li>
        <li v-show="showSpaceOperation" @click.stop.prevent="onNewFolder">
          新建文件夹
        </li>
        <li v-show="showSpaceOperation" @click.stop.prevent="onNewProject">
          新建项目
        </li>
        <li v-show="showEntityOperation" @click.stop.prevent="onCopy">复制</li>
        <li v-show="showCutOperation" @click.stop.prevent="onCut">剪切</li>
        <li
          v-show="showSpaceOperation && hasClipboardEntity"
          @click.stop.prevent="onPaste"
        >
          粘贴
        </li>
        <li v-show="showSpaceOperation" @click.stop.prevent="onRefresh">
          刷新
        </li>
      </ul>
    </div>
  </teleport>
</template>

<script lang="ts">
import { computed, defineComponent } from "vue-demi";
import { ElMessageBox, ElMessage } from "element-plus";
import {
  default as contextmenuData,
  copyToClipBard,
  closeContextMenu,
  clearClipboard,
} from "@/hooks/contextmenu";
import { EntityMode } from "@/types/project";

export default defineComponent({
  name: "ContextMenu",
  setup() {
    const positioRef = computed(() => {
      if (contextmenuData.contextmenuEvent?.value) {
        return {
          x: contextmenuData.contextmenuEvent.value!.clientX,
          y: contextmenuData.contextmenuEvent.value!.clientY,
        };
      }

      return {
        x: 0,
        y: 0,
      };
    });
    const showSpaceOperation = computed(() => {
      return contextmenuData.entity?.value.objectID <= 0;
    });
    const showEntityOperation = computed(() => {
      return contextmenuData.entity?.value.objectID > 0;
    });
    const showCutOperation = computed(() => {
      return contextmenuData.entity?.value.objectID > 0;
    });
    const hasClipboardEntity = computed(() => {
      return contextmenuData.clipBoard.value.clipBoardEntity != null;
    });
    const onDelete = (e: MouseEvent) => {
      ElMessageBox.confirm("删除的数据无法恢复，请问您要删除吗？", {
        confirmButtonText: "直接删除",
        cancelButtonText: "考虑一下",
        type: "error",
      })
        .then((data) => {
          ElMessage({
            type: "success",
            message: "删除成功",
          });
          // TODO: delete api
        })
        .finally(() => {
          closeContextMenu();
        });
    };
    const onNewProject = (e: MouseEvent) => {
      console.log(e);
    };
    const onNewFolder = (e: MouseEvent) => {
      console.log(e);
    };
    const onRename = (e: MouseEvent) => {
      console.log(e);
    };
    const onRefresh = (e: MouseEvent) => {
      console.log(e);
    };
    const onCut = (e: MouseEvent) => {
      copyToClipBard(EntityMode.Cut);
      closeContextMenu();
    };
    const onCopy = (e: MouseEvent) => {
      copyToClipBard(EntityMode.Copy);
      closeContextMenu();
    };
    const onPaste = (e: MouseEvent) => {
      if (contextmenuData.clipBoard.value != null) {
        // TODO: paste api
      }
      clearClipboard();
      closeContextMenu();
    };
    return {
      contextmenuData,
      position: positioRef,
      onDelete,
      onRename,
      onNewFolder,
      onNewProject,
      onRefresh,
      onCopy,
      onPaste,
      onCut,
      showSpaceOperation,
      showEntityOperation,
      hasClipboardEntity,
      showCutOperation,
    };
  },
});
</script>

<style lang="less" scoped>
@import "~@/assets/less/variable.less";
.contextmenu-container {
  min-width: 120px;
  max-width: 180px;
  max-height: 120px;
  position: fixed;
  border: 1px solid rgba(0, 0, 0, 0.1);
  background: #fff;
  box-shadow: 1px 1px 2px rgba(0, 0, 0, 0.1);
  border-radius: 5px;
  padding: 10px 0;
  ul {
    li {
      height: 24px;
      line-height: 24px;
      font-size: 14px;
      box-sizing: border-box;
      padding: 0 10px;
      cursor: default;
      user-select: none;
      &:hover {
        color: @primary;
        background: #ecf5ff;
      }
    }
  }
}
</style>
