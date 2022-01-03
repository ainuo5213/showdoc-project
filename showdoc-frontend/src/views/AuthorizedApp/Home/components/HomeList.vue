<template>
  <div class="homelist-container" @contextmenu.stop.prevent="onContextMenu">
    <ul>
      <li
        v-for="item in data"
        :key="`${item.name}_${item.objectID}_${item.type}`"
      >
        <template v-if="item.type === folderEnum">
          <folder-item :data="item"></folder-item>
        </template>
        <template v-else>
          <project-item :data="item"></project-item>
        </template>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue-demi";
import FolderItem from "./FolderItem.vue";
import ProjectItem from "./ProjectItem.vue";
import { IProjectItem, ProjectItemEnums } from "@/types/project";
import { openContextMenuWithEntity } from "@/hooks/contextmenu";

export default defineComponent({
  components: {
    FolderItem,
    ProjectItem,
  },
  props: {
    data: {
      type: Array as PropType<IProjectItem[]>,
      required: true,
    },
  },
  setup(props) {
    const onContextMenu = (e: MouseEvent) => {
      openContextMenuWithEntity(
        {
          userID: 0,
          sortTime: "",
          createTime: "",
          name: "",
          objectID: 0,
          parentID: 0,
          type: ProjectItemEnums.None,
        },
        e,
        true,
        ProjectItemEnums.Space
      );
    };
    return {
      folderEnum: ProjectItemEnums.Folder,
      onContextMenu,
    };
  },
});
</script>

<style lang="less" scoped>
.homelist-container {
  margin-top: 20px;
  height: 100%;
  overflow-y: auto;
  & > * {
    user-select: none;
  }

  &::-webkit-scrollbar-track {
    border-radius: 10px;
  }
  &::-webkit-scrollbar-thumb {
    background-color: #0003;
    border-radius: 10px;
    transition: all 0.2s ease-in-out;
  }

  &::-webkit-scrollbar {
    width: 6px;
  }
}
ul {
  li {
    width: 180px;
    height: 120px;
    float: left;
    margin-bottom: 30px;
  }
}
</style>
