<template>
  <div
    class="item-container"
    @dblclick="ondbClick"
    @contextmenu.stop.prevent="onProjectContextMenu"
    @click.stop.prevent="onclick"
    ref="folderRef"
    tabindex="0"
    draggable="draggable"
    @dragenter="$event.preventDefault()"
    @dragover="$event.preventDefault()"
    @dragleave="$event.preventDefault()"
    @dragstart="onDrag"
    @drop.stop.prevent="onDrop"
  >
    <el-card
      shadow="hover"
      :class="{
        'entity-selected': isSelected,
      }"
      :body-style="{
        border: 'none',
        padding: '0px',
        height: '100%',
        display: 'flex',
        'flex-direction': 'column',
      }"
    >
      <div class="item">
        <img class="folder" :src="require('@/assets/img/folder.png')" />
      </div>
      <div class="title-container">
        <span v-if="!isRenaming" class="title">{{ data.name }}</span>
        <el-input
          v-else
          v-model="name"
          placeholder="请输入文件夹名"
          @blur="onRename"
          @keyup.enter="$event.target.blur()"
          maxlength="18"
          autofocus
        />
      </div>
    </el-card>
  </div>
</template>

<script lang="ts">
import { IProjectItem, ProjectItemEnums } from "@/types/project";
import { defineComponent, PropType, ref } from "vue-demi";
import {
  openContextMenuWithEntity,
  closeContextMenu,
  setSelectEntity,
  default as contextmenuData,
} from "@/hooks/contextmenu";
import {
  useEntitySelection,
  useRename,
  useShortCut,
} from "@/hooks/useFunction";
import { useRouter } from "vue-router";
import { pushFolder } from "@/hooks/folder";
import { default as dragState, setDragData } from "@/hooks/drag";
import { moveFolderOrProject } from "@/api/project";
import { ElMessage } from "element-plus";
import { removeData } from "@/hooks/project";

export default defineComponent({
  name: "Folder",
  props: {
    data: {
      type: Object as PropType<IProjectItem>,
      required: true,
    },
  },
  setup(props) {
    const folderRef = ref();
    const router = useRouter();
    const { isSelected } = useEntitySelection(props.data);
    useShortCut(folderRef);
    const onProjectContextMenu = (e: MouseEvent) => {
      openContextMenuWithEntity(props.data, e, true, ProjectItemEnums.Folder);
    };

    // 设置选中状态
    const onclick = (e: MouseEvent) => {
      if (contextmenuData.showContextMenu) {
        closeContextMenu();
      }
      setSelectEntity(props.data);
    };
    const ondbClick = () => {
      closeContextMenu();
      // set folders
      pushFolder({
        folderID: props.data.objectID,
        name: props.data.name,
        parentID: props.data.parentID,
      });
      router.push({ name: "home", query: { folderID: props.data.objectID } });
    };

    async function onDrop(e: MouseEvent) {
      const data = dragState.dragData.value;
      let res = await moveFolderOrProject({
        folderID: props.data.objectID,
        type: data.type,
        objectID: data.objectID,
      });
      if (res.errno == 0 && res.data) {
        ElMessage({
          type: "success",
          message: "移动实体成功",
        });
        removeData(data.objectID, data.type);
      } else {
        ElMessage({
          type: "error",
          message: res.errmsg,
        });
      }
    }

    function onDrag() {
      console.log(props);
      setDragData(props.data);
    }

    const { name, onRename, isRenaming } = useRename(props.data);

    return {
      onProjectContextMenu,
      isSelected,
      ondbClick,
      isRenaming,
      onRename,
      onclick,
      folderRef,
      name,
      onDrop,
      onDrag,
    };
  },
});
</script>

<style lang="less" scoped>
@import url("./common.less");
</style>
