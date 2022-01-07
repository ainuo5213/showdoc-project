<template>
  <search @search="onSearch"></search>
  <div class="nav">
    <el-link
      :class="{
        active: 0 == activeFodlerID,
      }"
      @drop.stop.prevent="onDrop(0)"
      :underline="false"
      draggable="draggable"
      @click="clickFolder(0)"
      >root<span class="sep">/</span></el-link
    >
    <el-link
      :underline="false"
      v-for="folder in folders"
      @drop.stop.prevent="onDrop(folder.folderID)"
      draggable="draggable"
      :key="folder.folderID"
      @click="clickFolder(folder.folderID)"
      :class="{
        active: folder.folderID == activeFodlerID,
      }"
      >{{ folder.name }}<span class="sep">/</span></el-link
    >
  </div>
  <home-list :data="filteredData"></home-list>
  <context-menu v-if="contextmenuData.showContextMenu.value"></context-menu>
</template>

<script lang="ts">
import ContextMenu from "@/components/Contextmenu/index.vue";
import { computed, defineComponent, watch, ref } from "vue-demi";
import HomeList from "./components/HomeList.vue";
import Search from "@/components/Search/index.vue";
import { IProjectItem } from "@/types/project";
import { default as contextmenuData } from "@/hooks/contextmenu";
import { useRequest } from "@/hooks/useFunction";
import { useRoute, useRouter, onBeforeRouteUpdate } from "vue-router";
import { default as folderData, removeChildFolders } from "@/hooks/folder";
import { default as state, removeData, setData } from "@/hooks/project";
import { default as dragState } from "@/hooks/drag";
import { moveFolderOrProject } from "@/api/project";
import { ElMessage } from "element-plus";

export default defineComponent({
  components: {
    HomeList,
    Search,
    ContextMenu,
  },
  setup() {
    const { loading, request } = useRequest<IProjectItem[]>();
    const route = useRoute();
    const router = useRouter();
    const searchValue = ref("");
    const activeFodlerID = computed(() => +(route.query.folderID || 0));

    const folderID = computed(() => {
      return route.query["folderID"] || 0;
    });

    // fetch data
    async function fetchData() {
      let res = await request({
        url: "/api/project/list",
        method: "get",
        params: {
          folderID: folderID.value,
        },
      });
      if (res.errno == 0) {
        setData(res.data);
      }
    }

    fetchData();
    watch(
      folderID,
      () => {
        fetchData();
      },
      {
        immediate: false,
      }
    );

    const filteredData = computed(() => {
      if (searchValue.value.length == 0) {
        return state.data.value;
      } else {
        return state.data.value.filter(
          (r) => r.name.indexOf(searchValue.value) >= 0
        );
      }
    });

    // search
    const onSearch = (value: string) => {
      searchValue.value = value;
    };

    // 改变网站名字
    watch(
      folderData.folders,
      (value) => {
        const last = value[value.length - 1];
        if (last && last.name) {
          console.log("我的项目" + "~" + last.name);
          document.title = "我的项目" + "~" + last.name;
        } else {
          document.title = "我的项目";
        }
      },
      {
        immediate: true,
        deep: true,
      }
    );

    onBeforeRouteUpdate((to, next) => {
      const folderID = +(to.query.folderID || 0);
      removeChildFolders(folderID);
    });

    // 面包屑导航
    function clickFolder(folderID: number) {
      // 将之后的移除
      removeChildFolders(folderID);
      // 跳转
      router.replace({ name: "home", query: { folderID: folderID } });
    }

    async function onDrop(folderID: number) {
      console.log(1111);
      const data = dragState.dragData.value;
      let res = await moveFolderOrProject({
        folderID: folderID,
        type: data.type,
        objectID: data.objectID,
      });
      if (res.errno == 0 && res.data) {
        ElMessage({
          type: "success",
          message: "移动实体成功",
        });
        removeData(data.objectID, data.type);
      } else {
        ElMessage({
          type: "error",
          message: res.errmsg,
        });
      }
    }

    return {
      onSearch,
      contextmenuData,
      loading,
      folders: folderData.folders,
      filteredData,
      clickFolder,
      activeFodlerID,
      onDrop,
    };
  },
});
</script>

<style lang="less" scoped>
@import url("~@/assets/less/variable.less");

.active {
  color: @primary;
}

.nav {
  height: 40px;
  line-height: 40px;
  overflow: hidden;
  margin: 10px 0;
  .el-link {
    margin-right: 10px;
    .sep {
      color: @text;
      margin-left: 10px;
    }
  }
}
</style>
