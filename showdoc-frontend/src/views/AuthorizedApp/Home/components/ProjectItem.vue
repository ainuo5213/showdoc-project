<template>
  <div
    class="item-container"
    @dblclick="ondbClick"
    @contextmenu.stop.prevent="onProjectContextMenu"
    @click.stop.prevent="onclick"
    ref="projectRef"
    tabindex="0"
    @dragstart="onDrag"
    @dragenter="$event.preventDefault()"
    @dragover="$event.preventDefault()"
    draggable="draggable"
  >
    <el-card
      :class="{
        'entity-selected': isSelected,
      }"
      shadow="hover"
      :body-style="{
        border: 'none',
        padding: '0px',
        height: '100%',
        display: 'flex',
        'flex-direction': 'column',
      }"
    >
      <div class="item">
        <img :src="require('@/assets/img/project.png')" />
      </div>
      <div class="title-container elipsis">
        <span v-if="!isRenaming" class="title">{{ data.name }}</span>
        <el-input
          v-else
          v-model="name"
          placeholder="请输入项目名"
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
  clearClipboard,
  default as contextmenuData,
  setSelectEntity,
} from "@/hooks/contextmenu";
import {
  useEntitySelection,
  useRename,
  useShortCut,
} from "@/hooks/useFunction";
import { useRouter } from "vue-router";
import { setDragData } from "@/hooks/drag"

export default defineComponent({
  name: "Project",
  props: {
    data: {
      type: Object as PropType<IProjectItem>,
      required: true,
    },
  },
  setup(props) {
    const router = useRouter();

    const projectRef = ref();
    const { isSelected } = useEntitySelection(props.data);
    useShortCut(projectRef);
    const onProjectContextMenu = (e: MouseEvent) => {
      openContextMenuWithEntity(props.data, e, true, ProjectItemEnums.Project);
    };

    const ondbClick = () => {
      closeContextMenu();
      clearClipboard();
      router.push({
        name: "project",
        query: { projectID: props.data.objectID },
      });
    };
    // 设置选中状态
    const onclick = (e: MouseEvent) => {
      if (contextmenuData.showContextMenu) {
        closeContextMenu();
      }
      setSelectEntity(props.data);
    };

    function onDrag() {
      setDragData(props.data);
    }

    const { name, onRename, isRenaming } = useRename(props.data);

    return {
      onProjectContextMenu,
      contextmenuData,
      isSelected,
      ondbClick,
      isRenaming,
      onRename,
      onclick,
      projectRef,
      name,
      onDrag,
    };
  },
});
</script>

<style lang="less" scoped>
@import url("./common.less");
</style>
