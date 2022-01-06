<template>
  <div
    tabindex="0"
    ref="homeListRef"
    class="homelist-container"
    @contextmenu.stop.prevent="onContextMenu"
  >
    <transition-group name="homelist" tag="ul" :appear="true" mode="out-in">
      <li
        v-for="item in data"
        :key="`${item.name}_${item.objectID}_${item.type}`"
      >
        <template v-if="item.type === folderEnum">
          <folder-item :data="item" :key="`${item.name}_${item.objectID}_${item.type}`"></folder-item>
        </template>
        <template v-else>
          <project-item :data="item" :key="`${item.name}_${item.objectID}_${item.type}`"></project-item>
        </template>
      </li>
    </transition-group>
  </div>
</template>

<script lang="ts">
import { defineComponent, onBeforeUnmount, onMounted, PropType, ref } from "vue-demi";
import FolderItem from "./FolderItem.vue";
import ProjectItem from "./ProjectItem.vue";
import { IProjectItem, ProjectItemEnums, EntityMode } from "@/types/project";
import {
  openContextMenuWithEntity,
  default as contextmenuData,
  clearSelectedEntity,
  clearClipboard,
} from "@/hooks/contextmenu";
import { useRoute } from "vue-router";
import { moveFolderOrProject } from "@/api/project";
import { ElMessage } from "element-plus";
import { default as entities, appendData } from "@/hooks/project";

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
    const homeListRef = ref();
    const route = useRoute();
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
    onMounted(() => {
      homeListRef.value.addEventListener("keyup", onKeyup);
    });
    onBeforeUnmount(() => {
      homeListRef.value.removeEventListener("keyup", onKeyup);
    })
    async function onKeyup(e: KeyboardEvent) {
      if (e.ctrlKey && e.code == "KeyV") {
        if (
          contextmenuData.clipBoard.value.clipBoardEntity != null &&
          contextmenuData.clipBoard.value.mode != null &&
          contextmenuData.clipBoard.value.mode != EntityMode.None
        ) {
          let res = await moveFolderOrProject({
            objectID: contextmenuData.clipBoard.value.clipBoardEntity.objectID,
            folderID: +(route.query.folderID || 0),
            type: contextmenuData.clipBoard.value.clipBoardEntity.type,
          });
          if (res.errno == 0 && res.data) {
            ElMessage({
              type: "success",
              message: "移动实体成功",
            });
            const index = entities.data.value.findIndex(
              (r) =>
                r.objectID ==
                  contextmenuData.clipBoard.value.clipBoardEntity!.objectID &&
                r.type == contextmenuData.clipBoard.value.clipBoardEntity!.type
            );
            if (index < 0) {
              appendData(contextmenuData.clipBoard.value.clipBoardEntity);
            }
            clearSelectedEntity();
            clearClipboard();
          } else {
            ElMessage({
              type: "error",
              message: res.errmsg,
            });
          }
        }
      }
    }
    return {
      folderEnum: ProjectItemEnums.Folder,
      onContextMenu,
      homeListRef,
    };
  },
});
</script>

<style lang="less" scoped>
.homelist-container {
  margin-top: 20px;
  height: 100%;
  overflow-y: auto;
  overflow-x: hidden;
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

.homelist-enter-active,
.homelist-leave-active {
  transition: all 0.5s ease;
}
.homelist-enter-from,
.homelist-leave-to {
  transform: translateY(30px);
  opacity: 0;
}

.homelist-leave-to {
  position: absolute;
}

.homelist-move {
  transition: transform 0.5s ease;
}
</style>
