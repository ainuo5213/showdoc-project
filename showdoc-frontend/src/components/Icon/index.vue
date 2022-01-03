<template>
  <div
    class="icon"
    :style="{
      color,
      'border-radius': radius + 'px'
    }"
  >
    <i
      class="iconfont"
      :class="iconName"
      :style="{
        'font-size': size + 'px',
      }"
    ></i>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, PropType } from "vue";

export default defineComponent({
  props: {
    name: {
      type: String as PropType<string>,
      required: true,
    },
    size: {
      type: Number as PropType<number>,
      required: false,
      default: 20,
    },
    color: {
      type: String as PropType<string>,
      required: false,
      default: "#000",
    },
    radius: {
      type: Number as PropType<number>,
      required: false,
      default: 20
    },
  },
  setup(props) {
    const iconName = computed(() => `icon-${props.name}`);
    return {
      iconName,
    };
  },
});
</script>

<style lang="less" scoped>
.icon {
  display: inline-block;
  overflow: hidden;
  .iconfont {
    width: 100%;
    height: 100%;
  }
}
</style>
