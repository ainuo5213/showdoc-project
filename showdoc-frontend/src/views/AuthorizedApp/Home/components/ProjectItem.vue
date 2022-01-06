<template>
  <div
    class="item-container"
    @dblclick="ondbClick"
    @contextmenu.stop.prevent="onProjectContextMenu"
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
          v-model="state.name"
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
import { defineComponent, PropType, reactive, computed } from "vue-demi";
import {
  openContextMenuWithEntity,
  closeContextMenu,
  clearClipboard,
  default as contextmenuData,
} from "@/hooks/contextmenu";
import { renamFolderOrProject } from "@/api/project";
import { useEntitySelection } from "@/hooks/useFunction";
import { useRouter } from "vue-router";
import {
  default as projectState,
  renameData,
  cancelRenaming,
} from "@/hooks/project";
import { ElMessage } from "element-plus";

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
      router.push({
        name: "project",
        query: { projectID: props.data.objectID },
      });
    };

    const isRenaming = computed(() => {
      return (
        projectState.namedEntity.value.objectID != 0 &&
        projectState.namedEntity.value.objectID == props.data.objectID &&
        projectState.namedEntity.value.type == props.data.type
      );
    });
    const onRename = async () => {
      if (state.name.length == 0) {
        ElMessage({
          type: "error",
          message: "项目名长度必须大于1",
        });
        return;
      }
      if (state.name == projectState.namedEntity.value.name) {
        cancelRenaming();
        return;
      }
      let res = await renamFolderOrProject({
        objectID: projectState.namedEntity.value.objectID,
        name: state.name,
        type: projectState.namedEntity.value.type,
      });
      if (res.errno == 0 && res.data) {
        ElMessage({
          type: "success",
          message: "重命名成功",
        });
      } else {
        ElMessage({
          type: "error",
          message: res.errmsg || "重命名失败",
        });
      }
      renameData(state.name);
    };

    return {
      onProjectContextMenu,
      state,
      contextmenuData,
      isSelected,
      ondbClick,
      isRenaming,
      onRename,
    };
  },
});
</script>

<style lang="less" scoped>
@import url("./common.less");
</style>
