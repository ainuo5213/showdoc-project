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
        <li v-show="showEntityOperation" @click.stop.prevent="onView">查看</li>
        <li
          v-show="showSpaceOperation && showRetunOperation"
          @click.stop.prevent="onReturn"
        >
          返回
        </li>
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
        <!-- 复制有点复杂之后再做 -->
        <!-- <li v-show="showEntityOperation" @click.stop.prevent="onCopy">复制</li> -->
        <li v-show="showCutOperation" @click.stop.prevent="onCut">剪切</li>
        <li
          v-show="showSpaceOperation && hasClipboardEntity"
          @click.stop.prevent="onPaste"
        >
          粘贴
        </li>
      </ul>
    </div>
  </teleport>

  <el-dialog
    v-model="state.showNewEntityDialog"
    :title="'新建' + newEntityDialogTitle"
    append-to-body
    @close="cancelCreate"
    width="20%"
    custom-class="new-entity-dialog"
  >
    <el-form
      :rules="state.rules"
      :model="state.form"
      ref="formRef"
      destroy-on-close
      :close-on-click-modal="false"
      status-icon
    >
      <el-form-item prop="name">
        <el-input maxlength="18" v-model="state.form.name"> </el-input>
      </el-form-item>
    </el-form>
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="cancelCreate">取消</el-button>
        <el-button type="primary" @click="createProjectOrFolder"
          >创建</el-button
        >
      </span>
    </template>
  </el-dialog>
</template>

<script lang="ts">
import { computed, defineComponent, ref, reactive } from "vue-demi";
import { ElMessageBox, ElMessage } from "element-plus";
import {
  default as contextmenuData,
  copyToClipBard,
  closeContextMenu,
  clearClipboard,
} from "@/hooks/contextmenu";
import { EntityMode, ProjectItemEnums } from "@/types/project";
import { useRoute, useRouter } from "vue-router";
import {
  pushFolder,
  default as folders,
  removeChildFolders,
  clearFolders,
} from "@/hooks/folder";
import {
  createFolderOrProject as createFolderOrProjectRequest,
  deleteFolderOrProject,
  moveFolderOrProject,
} from "@/api/project";
import {
  appendData,
  removeData,
  setRenamingData,
  default as entities,
} from "@/hooks/project";

export default defineComponent({
  name: "ContextMenu",
  setup() {
    const router = useRouter();
    const route = useRoute();
    const formRef = ref();
    const state = reactive({
      form: {
        name: "",
      },
      rules: {
        name: [
          { required: true, message: `请输入名字` },
          { min: 1, max: 18, message: `名字需在1到18个字符` },
        ],
      },
      show: true,
      showNewEntityDialog: false,
      entityType: ProjectItemEnums.None,
    });

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
    const newEntityDialogTitle = computed(() => {
      return state.entityType == ProjectItemEnums.Project ? "项目" : "文件夹";
    });
    const showCutOperation = computed(() => {
      return contextmenuData.entity?.value.objectID > 0;
    });
    const hasClipboardEntity = computed(() => {
      return contextmenuData.clipBoard.value.clipBoardEntity != null;
    });
    const onDelete = () => {
      if (
        contextmenuData.entity.value.type != ProjectItemEnums.Space &&
        contextmenuData.entity.value.type != ProjectItemEnums.None
      ) {
        ElMessageBox.confirm("删除的数据无法恢复，请问您要删除吗？", {
          confirmButtonText: "直接删除",
          cancelButtonText: "考虑一下",
          type: "error",
        })
          .then(async (data) => {
            let res = await deleteFolderOrProject({
              type: contextmenuData.entity.value.type,
              objectID: contextmenuData.entity.value.objectID,
            });
            if (res.errno == 0 && res.data) {
              ElMessage({
                type: "success",
                message: "删除成功",
              });
              removeData(
                contextmenuData.entity.value.objectID,
                contextmenuData.entity.value.type
              );
              clearClipboard(contextmenuData.entity.value.objectID);
            } else {
              ElMessage({
                type: "error",
                message: "删除失败",
              });
            }
          })
          .finally(() => {
            closeContextMenu();
          });
      }
    };
    const onNewProject = () => {
      if (contextmenuData.contxtMenuType.value == ProjectItemEnums.Space) {
        state.showNewEntityDialog = true;
        state.entityType = ProjectItemEnums.Project;
      }
    };
    const onNewFolder = () => {
      if (contextmenuData.contxtMenuType.value == ProjectItemEnums.Space) {
        state.showNewEntityDialog = true;
        state.entityType = ProjectItemEnums.Folder;
      }
    };
    const onRename = (e: MouseEvent) => {
      if (contextmenuData.entity.value != null) {
        setRenamingData(contextmenuData.entity.value);
      }
      closeContextMenu();
    };
    const onCut = () => {
      copyToClipBard(EntityMode.Cut);
      closeContextMenu();
    };
    const onCopy = () => {
      copyToClipBard(EntityMode.Copy);
      closeContextMenu();
    };
    const onView = () => {
      if (contextmenuData.entity.value.type == ProjectItemEnums.Folder) {
        // router跳转
        router.replace({
          name: "home",
          query: { folderID: contextmenuData.entity.value.objectID },
        });
        // folder设置
        pushFolder({
          folderID: contextmenuData.entity.value.objectID,
          parentID: contextmenuData.entity.value.parentID,
          name: contextmenuData.entity.value.name,
        });
      } else {
        // router跳转
        router.replace({
          name: "project",
          params: { projectID: contextmenuData.entity.value.objectID },
        });
        clearFolders();
      }
      closeContextMenu();
    };
    const onReturn = () => {
      // router跳转：判断是否是点击空白区域，且folder历史存在
      if (
        folders.folders.value.length &&
        contextmenuData.contxtMenuType.value == ProjectItemEnums.Space
      ) {
        const folder = folders.folders.value[folders.folders.value.length - 1];
        router.replace({
          name: "home",
          query: { folderID: folder.parentID },
        });
        removeChildFolders(folder.parentID);
      }
      closeContextMenu();
    };
    const onPaste = async () => {
      if (
        contextmenuData.clipBoard.value != null &&
        contextmenuData.clipBoard.value.mode != undefined &&
        contextmenuData.clipBoard.value.mode != EntityMode.None &&
        contextmenuData.clipBoard.value.clipBoardEntity != null
      ) {
        const folderID = +(route.query.folderID || 0);
        if (contextmenuData.clipBoard.value.mode == EntityMode.Cut) {
          // 剪切，即移动
          let res = await moveFolderOrProject({
            objectID: contextmenuData.clipBoard.value.clipBoardEntity.objectID,
            folderID: folderID,
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
          } else {
            ElMessage({
              type: "error",
              message: res.errmsg,
            });
          }
        } else {
          // 复制
          console.log("待完善复制");
        }
      }
      clearClipboard();
      closeContextMenu();
    };
    const cancelCreate = () => {
      state.showNewEntityDialog = false;
      state.entityType = ProjectItemEnums.None;
      closeContextMenu();
    };
    const createProjectOrFolder = async () => {
      if (state.form.name.length == 0) {
        return;
      }
      try {
        let res = await createFolderOrProjectRequest({
          name: state.form.name,
          type: state.entityType,
          folderID: +(route.query.folderID || 0),
        });
        if (res.errno == 0 && res.data != null) {
          ElMessage({
            type: "success",
            message: `创建${newEntityDialogTitle.value}成功`,
          });
          appendData(res.data);
        } else {
          ElMessage({
            type: "error",
            message: `创建${newEntityDialogTitle.value}失败`,
          });
        }
      } catch {
        // do nothing
        ElMessage({
          type: "error",
          message: "出了一点问题",
        });
      } finally {
        cancelCreate();
      }
    };
    const showRetunOperation = computed(() => {
      return folders.folders.value.length > 0;
    });
    return {
      contextmenuData,
      position: positioRef,
      onDelete,
      onRename,
      onNewFolder,
      onNewProject,
      onCopy,
      onPaste,
      onCut,
      onView,
      onReturn,
      showSpaceOperation,
      showEntityOperation,
      hasClipboardEntity,
      showCutOperation,
      showRetunOperation,
      createProjectOrFolder,
      cancelCreate,
      formRef,
      newEntityDialogTitle,
      state,
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

::v-deep(.new-entity-dialog) {
  width: 360px !important;
}
</style>
