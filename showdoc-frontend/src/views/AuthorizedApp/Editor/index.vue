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
          <el-cascader v-model="folder" :options="folders"></el-cascader>
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
      <editor v-model:value="value" />
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, toRefs } from "vue-demi";
import Editor from "@/components/Editor/index.vue";
import { useRoute, useRouter } from "vue-router";
import {
  IDocumentData,
  IUpdateDocumentForm,
} from "@/types/document";
import { createFolderOrDocument, updateDocument } from "@/api/document";
import { ElMessage } from "element-plus";
import { IDataResult } from "@/types/data";
const folders = [
  {
    value: 0,
    label: "Guide",
  },
  {
    value: 0,
    label: "Component",
    children: [
      {
        value: 1,
        label: "1",
      },
    ],
  },
];

export default defineComponent({
  components: {
    Editor,
  },
  setup() {
    const router = useRouter();
    const route = useRoute();
    const state = reactive({
      folder: [],
      title: "",
      value: "",
      saving: false,
    });
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
    return {
      folders,
      goBack,
      ...toRefs(state),
      saveDocument,
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
