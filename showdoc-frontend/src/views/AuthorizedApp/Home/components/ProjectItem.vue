<template>
  <div class="item-container" @dblclick="ondbClick" @contextmenu.stop.prevent="onProjectContextMenu">
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
        <span class="title" v-show="!state.renaming">{{ data.name }}</span>
        <el-input
          v-show="state.renaming"
          v-model="state.name"
          placeholder="请输入项目名"
        />
      </div>
    </el-card>
  </div>
</template>

<script lang="ts">
import { IProjectItem, ProjectItemEnums } from "@/types/project";
import { defineComponent, PropType, reactive } from "vue-demi";
import {
  openContextMenuWithEntity,
  closeContextMenu,
  clearClipboard,
  default as contextmenuData,
} from "@/hooks/contextmenu";
import { useEntitySelection } from "@/hooks/useFunction";
import { useRouter } from "vue-router";

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
    const { isSelected } = useEntitySelection(props.data);
    const state = reactive({
      renaming: false,
      name: props.data.name,
    });
    const onProjectContextMenu = (e: MouseEvent) => {
      openContextMenuWithEntity(props.data, e, true, ProjectItemEnums.Project);
    };

    const ondbClick = () => {
      closeContextMenu();
      clearClipboard();
      router.push({ name: "project", query: { projectID: props.data.objectID } });
    };

    return {
      onProjectContextMenu,
      state,
      contextmenuData,
      isSelected,
      ondbClick,
    };
  },
});
</script>

<style lang="less" scoped>
@import url("./common.less");
</style>
