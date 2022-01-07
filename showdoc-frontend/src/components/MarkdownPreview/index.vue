<template>
  <div class="markdown-body" v-html="content"></div>
</template>

<script lang="ts">
import { defineComponent, PropType, computed } from "vue-demi";
import MarkdownIt from "markdown-it";


const md: MarkdownIt = new MarkdownIt({
  linkify: true,
});

(window as any).md = md;

export default defineComponent({
  props: {
    data: {
      type: String as PropType<string>,
      required: true,
    },
  },
  setup(props) {
    console.log(props.data);
    const compiledMdContent = md.render(props.data);
    console.log(compiledMdContent);
    return {
      content: compiledMdContent,
    };
  },
});
</script>

<style lang="less" scoped>
@import url("~@/assets/editor/github-markdown.css");
</style>
