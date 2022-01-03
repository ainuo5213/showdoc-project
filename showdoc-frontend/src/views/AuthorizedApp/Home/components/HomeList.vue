<template>
  <div class="homelist-container">
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
    return {
      folderEnum: ProjectItemEnums.Folder,
    };
  },
});
</script>

<style lang="less" scoped>
.homelist-container {
  margin-top: 20px;
  height: 100%;
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
