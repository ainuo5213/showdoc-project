<template>
  <div class="sidearea-container">
    <ul>
      <li>
        <icon :size="15" name="xitongfanhui" @click="goBack"></icon>
      </li>
      <li>
        <icon :size="15" name="plus" @click="goEdit(0)"></icon>
      </li>
      <li>
        <icon :size="15" name="wenjianjia" @click="goFolder"></icon>
      </li>
      <li>
        <icon :size="15" name="editor"></icon>
      </li>
      <li>
        <icon :size="15" name="history1"></icon>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import Icon from "@/components/Icon/index.vue";
import { defineComponent } from "vue-demi";
import { useRoute, useRouter } from "vue-router";
import { default as folderState } from "@/hooks/folder";
export default defineComponent({
  components: {
    Icon,
  },
  setup() {
    const router = useRouter();
    const route = useRoute();
    const goBack = () => {
      router.replace({
        name: "home",
        query: {
          folderID: +(
            folderState.folders.value[folderState.folders.value.length - 1]?.folderID || 0
          ),
        },
      });
    };
    const goEdit = (documentID: number) => {
      router.push({
        name: "edit",
        params: {
          projectID: route.params.projectID,
          documentID: documentID
        },
      });
    }
    return {
      goBack,
      goEdit
    };
  },
});
</script>

<style lang="less" scoped>
.sidearea-container {
  color: #333;
  width: 240px;
  ul {
    display: flex;
    flex-wrap: wrap;
    li {
      margin-bottom: 20px;
      width: 80px;

      .icon {
        cursor: pointer;
      }
    }
  }
}
</style>
