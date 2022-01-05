<template>
  <search @search="onSearch"></search>
  <div class="nav" v-if="folders.length">
    <el-link :underline="false" v-if="folders.length" @click="clickFolder(0)"
      >root<span class="sep">/</span></el-link
    >
    <el-link
      :underline="false"
      v-for="folder in folders"
      :key="folder.folderID"
      @click="clickFolder(folder.folderID)"
      :class="{
        active: folder.folderID == folders[folders.length - 1].folderID,
      }"
      >{{ folder.name }}<span class="sep">/</span></el-link
    >
  </div>
  <home-list :data="data"></home-list>
  <context-menu v-if="contextmenuData.showContextMenu.value"></context-menu>
</template>

<script lang="ts">
import ContextMenu from "@/components/contextmenu/index.vue";
import { computed, defineComponent, reactive, toRefs, watch } from "vue-demi";
import HomeList from "./components/HomeList.vue";
import Search from "./components/Search.vue";
import { IProjectItem } from "@/types/project";
import { default as contextmenuData } from "@/hooks/contextmenu";
import { useRequest } from "@/hooks/useFunction";
import { useRoute, useRouter } from "vue-router";
import { default as folderData, removeChildFolders } from "@/hooks/folder";

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
    let sourceData: IProjectItem[];
    const state = reactive({
      data: [] as IProjectItem[],
    });

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
        sourceData = res.data;
        state.data = sourceData;
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

    // search
    const onSearch = (searchValue: string) => {
      if (searchValue.length == 0) {
        state.data = sourceData;
      } else {
        state.data = sourceData.filter((r) => r.name.indexOf(searchValue) >= 0);
      }
    };

    // 改变网站名字
    watch(
      folderData.folders,
      (value, newValue) => {
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

    // 面包屑导航
    function clickFolder(folderID: number) {
      console.log(folderID);
      // 将之后的移除
      removeChildFolders(folderID);
      // 跳转
      router.replace({ name: "home", query: { folderID: folderID } });
    }

    return {
      onSearch,
      ...toRefs(state),
      contextmenuData,
      loading,
      folders: folderData.folders,
      clickFolder,
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
