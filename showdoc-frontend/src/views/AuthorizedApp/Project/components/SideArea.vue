<template>
  <div class="sidearea-container">
    <ul>
      <li>
        <icon :size="15" name="xitongfanhui"></icon>
      </li>
      <li>
        <icon :size="15" name="plus"></icon>
      </li>
      <li>
        <icon :size="15" name="wenjianjia"></icon>
      </li>
      <li>
        <icon :size="15" name="editor"></icon>
      </li>
    </ul>
  </div>
</template>

<script>
import Icon from "@/components/Icon/index.vue";
import { defineComponent } from "vue-demi";
export default defineComponent({
  components: {
    Icon,
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
