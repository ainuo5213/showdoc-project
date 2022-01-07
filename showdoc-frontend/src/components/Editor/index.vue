<template>
  <div id="editor">
    <textarea
      ref="textareaRef"
      :value="value"
      @input="$emit('update:value', $event.target.value)"
      id="textarea"
      style="display: none"
    ></textarea>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, PropType, ref } from "vue-demi";
export default defineComponent({
  name: "Editor",
  props: {
    value: {
      type: String as PropType<string>,
      required: true,
    },
  },
  emits: ["update:value"],
  setup(props, { emit }) {
    const editor = ref();
    const textareaRef = ref();
    onMounted(() => {
      editor.value = (window as any).editormd("editor", {
        width: "100%",
        height: "100%", 
        path: "/editor.md/lib/",
        onchange() {
          emit("update:value", textareaRef.value.value);
        },
      });
    });
    return {
      textareaRef,
    };
  },
});
</script>

<style scoped lang="less">
#editor {
  height: 750px !important;
}
</style>
