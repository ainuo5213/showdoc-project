<template>
  <div class="search-container">
    <el-input
      v-model="text"
      class="search"
      placeholder="搜索项目"
      @keyup.enter="$emit('search', text)"
    />
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue-demi";

export default defineComponent({
  name: "Search",
  emits: ["search"],
  setup() {
    const text = ref("");
    return {
      text,
    };
  },
});
</script>

<style scoped lang="less">
.search-container {
  width: 180px;
}
</style>
