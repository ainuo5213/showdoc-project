<template>
  <div class="markdown-body" v-html="content"></div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue-demi";
import MarkdownIt from "markdown-it";
import { IDocumentData } from "@/types/document";


const md: MarkdownIt = new MarkdownIt({
  linkify: true,
});

(window as any).md = md;

export default defineComponent({
  props: {
    data: {
      type: Object as PropType<IDocumentData>,
      required: true,
    },
  },
  setup(props) {
    const compiledMdContent = md.render(props.data.content);
    return {
      content: compiledMdContent,
    };
  },
});
</script>

<style lang="less" scoped>
@import url("~@/assets/editor/github-markdown.css");
</style>
