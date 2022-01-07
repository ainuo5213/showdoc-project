<template>
  <div class="editor-container">
    <div class="header">
      <div class="left">
        <div class="title-box">
          <label>标题：</label>
          <el-input v-model="title"></el-input>
        </div>
        <div class="folder-box">
          <label>目录：</label>
          <el-cascader
            placeholder="请选择目录（可不选）"
            v-model="folder"
            :options="folders"
            :props="maps"
          >
          </el-cascader>
        </div>
      </div>
      <div class="right">
        <el-button type="primary" @click="saveDocument" :loading="saving"
          >保存</el-button
        >
        <el-button @click="goBack">返回</el-button>
      </div>
    </div>
    <div class="util">
      <el-button>API接口模板</el-button>
      <el-button>数据字典模板</el-button>
      <el-button>历史版本</el-button>
      <el-dropdown class="dropdown-container" split-button>
        格式工具
        <template #dropdown>
          <el-dropdown-menu>
            <el-dropdown-item>JSON转表格</el-dropdown-item>
            <el-dropdown-item>JSON格式化</el-dropdown-item>
          </el-dropdown-menu>
        </template>
      </el-dropdown>
    </div>
    <div class="editor">
      <editor v-if="!loading" v-model:value="value" />
      <div v-else>加载中</div>
    </div>
  </div>
</template>

<script lang="ts">
import {
  defineComponent,
  onBeforeUnmount,
  onMounted,
  reactive,
  toRefs,
} from "vue-demi";
import Editor from "@/components/Editor/index.vue";
import { useRoute, useRouter } from "vue-router";
import {
  IDocumentData,
  IFolderItemData,
  IUpdateDocumentForm,
} from "@/types/document";
import {
  createFolderOrDocument,
  getDocumentContent,
  getProjectFolders,
  updateDocument,
} from "@/api/document";
import { ElMessage } from "element-plus";
import { IDataResult } from "@/types/data";

export default defineComponent({
  components: {
    Editor,
  },
  setup() {
    const router = useRouter();
    const route = useRoute();
    const state: {
      folder: number[];
      folders: IFolderItemData[];
      title: string;
      value: string;
      saving: boolean;
      loading: boolean;
    } = reactive({
      folder: [],
      folders: [],
      title: "",
      value: "",
      saving: false,
      loading: false,
    });
    async function getFolders() {
      const documentID = +(route.params.documentID || 0);
      if (documentID > 0) {
        const res = await getProjectFolders(documentID);
        if (res.errno === 0) {
          return res.data;
        }
        return Promise.reject(res.errmsg);
      }
      return Promise.reject("文档ID错误");
    }
    async function getDetail() {
      const documentID = +(route.params.documentID || 0);
      if (documentID > 0) {
        const res = await getDocumentContent(documentID);
        if (res.errno === 0) {
          return res.data;
        }
        return Promise.reject(res.errmsg);
      }
      return Promise.reject("文档ID错误");
    }

    function onKeyUp(e: KeyboardEvent) {
      if (e.ctrlKey && e.code == "KeyS") {
        e.preventDefault();
        saveDocument();
      }
    }

    onMounted(() => {
      document.addEventListener("keydown", onKeyUp);
    });

    onBeforeUnmount(() => {
      document.removeEventListener("keydown", onKeyUp);
    });

    async function fetchData() {
      try {
        state.loading = true;
        const folders = await getFolders();
        const detail = await getDetail();
        state.folders = folders;
        state.title = detail.title;
        state.value = detail.content;
        const tree = getChildren(detail.folderID);
        state.folder = tree;
      } catch (error) {
        ElMessage({
          type: "error",
          message: "加载错误",
        });
      } finally {
        state.loading = false;
      }
    }

    fetchData();

    function getChildren(folderID: number) {
      let tree: number[] = [];
      let folder = state.folders.find((r) => r.folderID == folderID);
      if (folder == null) {
        return [0];
      }

      tree.unshift(folder.folderID);

      let index = -1;
      while (
        (index = state.folders.findIndex(
          (r) => r.folderID == folder!.parentID
        )) > 0
      ) {
        folder = state.folders[index];
        tree.push(folder.folderID);
      }

      return tree;
    }

    function goBack() {
      router.back();
    }

    async function saveDocument() {
      const documentID = +(route.params.documentID || 0);
      const data: IUpdateDocumentForm = {
        folderID: +(state.folder[state.folder.length - 1] || 0),
        title: state.title,
        projectID: +(route.params.projectID || 0),
        content: state.value,
        documentID: documentID,
      };
      let res: IDataResult<IDocumentData>;
      if (documentID) {
        res = await updateDocument(data);
      } else {
        res = await createFolderOrDocument(data);
      }
      if (res.errno == 0) {
        ElMessage({
          type: "success",
          message: "保存成功",
        });
        if (!+route.params.documentID) {
          router.replace({
            name: "edit",
            params: {
              ...route.params,
              documentID: res.data.documentID,
            },
          });
        }
      } else {
        ElMessage({
          type: "error",
          message: "保存失败",
        });
      }
    }

    const maps = {
      value: "folderID",
      label: "name",
    };
    return {
      goBack,
      ...toRefs(state),
      saveDocument,
      maps: maps,
    };
  },
});
</script>

<style lang="less" scoped>
.editor-container {
  width: 90%;
  margin: 20px auto 0;
}

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  box-sizing: border-box;
  padding-bottom: 18px;

  .left {
    display: flex;
    justify-content: center;

    .title-box,
    .folder-box {
      display: flex;
      align-items: center;
      margin-right: 20px;

      .el-input {
        width: 200px;
      }
    }
  }
}

.util {
  margin-top: 15px;
  margin-bottom: 15px;

  .dropdown-container {
    margin-left: 100px;
  }
}

.editor {
  width: 100%;
  height: 100%;
}
</style>
