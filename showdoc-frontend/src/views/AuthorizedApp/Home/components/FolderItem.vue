<template>
  <div
    class="item-container"
    @dblclick="ondbClick"
    @contextmenu.stop.prevent="onProjectContextMenu"
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
          v-model="state.name"
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
import { computed, defineComponent, PropType, reactive } from "vue-demi";
import {
  openContextMenuWithEntity,
  closeContextMenu,
} from "@/hooks/contextmenu";
import { useEntitySelection } from "@/hooks/useFunction";
import { useRouter } from "vue-router";
import { pushFolder } from "@/hooks/folder";
import {
  default as projectState,
  renameData,
  cancelRenaming,
} from "@/hooks/project";
import { renamFolderOrProject } from "@/api/project";
import { ElMessage } from "element-plus";

export default defineComponent({
  name: "Folder",
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
      name: props.data.name,
    });
    const onProjectContextMenu = (e: MouseEvent) => {
      openContextMenuWithEntity(props.data, e, true, ProjectItemEnums.Project);
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
