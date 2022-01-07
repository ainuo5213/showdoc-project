<template>
  <div id="markdown-view" ref="viewRef"></div>
  <transition name="toc">
    <div id="toc-container"></div>
  </transition>
</template>

<script lang="ts">
import {
  defineComponent,
  onMounted,
  onUnmounted,
  onUpdated,
  PropType,
  ref,
} from "vue-demi";
import { IDocumentData } from "@/types/document";

const editormd = (window as any).editormd;
const $ = (window as any).$;

export default defineComponent({
  props: {
    data: {
      type: Object as PropType<IDocumentData>,
      required: true,
    },
  },
  setup(props) {
    const viewRef = ref();
    onMounted(() => {
      renderMarkdownView();
      $("#toc-container").on("click", "a", onTocClick);
    });
    onUpdated(() => {
      renderMarkdownView();
    });

    function renderMarkdownView() {
      $("#markdown-view").html("");
      if ($("#toc-container").length === 0) {
        $(`<div id="toc-container"></div>`).appendTo(
          $(".content-box .content")
        );
      }
      editormd.markdownToHTML("markdown-view", {
        width: "100%",
        height: "100%",
        markdown: props.data.content,
        path: "/editor.md/lib/",
        tocm: true,
        tocContainer: "#toc-container",
      });
      if ($(".markdown-toc-list").children().length == 0) {
        $("#toc-container").remove();
      }
    }

    function onTocClick(e: MouseEvent) {
      e.preventDefault();
      if ($(e.target).hasClass("current")) {
        return;
      }
      $(e.target)
        .parents(".markdown-toc-list")
        .eq(0)
        .find(".current")
        .removeClass("current");
      (e.target as HTMLElement).classList.add("current");
      const level = $(e.target).attr("level");
      const name = $(e.target).attr("href").slice(1);
      const targetDom = $(`h${level} a[name="${name}"]`);
      $("#app").animate(
        {
          scrollTop: targetDom.offset().top + "px",
        },
        500
      );
    }

    onUnmounted(() => {
      $("#toc-container").off("click", "a", onTocClick);
    });
    return {
      viewRef,
    };
  },
});
</script>

<style lang="less">
@import url("~@/assets/less/variable.less");
#toc-container {
  position: fixed;
  top: 230px;
  margin-left: 720px;

  .markdown-toc-list {
    min-width: 32px;
    min-height: 32px;
    max-width: 240px;
    max-height: 400px;
    overflow-y: auto;
    background: #fff;
    border: 1px solid #dcdfe6;
    border-radius: 5px;
    padding: 5px 0;
    box-shadow: 0 5px 5px #f2f6fc;

    li li a {
      padding-left: 30px;
    }
  }

  a {
    color: @text;
    text-decoration: none;
    display: block;
    padding: 3px 15px;
    font-size: 14px;
    color: #606266;
    white-space: nowrap;
    text-overflow: ellipsis;
    overflow: hidden;
    transition: 0.15s;
    box-sizing: border-box;

    &:hover {
      background: #d9ecff;
      color: #40a9ff;
      transition: 0s;
    }

    &.current {
      box-shadow: inset 2px 0 #40a9ff;
      background: #d9ecff;
      color: #40a9ff;
    }
  }
}
</style>

<style lang="less" scoped>
.toc-enter {
  transform: scale(0) translate(50px, 50px);
}

.toc-enter-active {
  transform: scale(1.5) translate(-20px, -20px);
  transition: 0.5s cubic-bezier(0.4, 1.7, 0.6, 1);
}

.toc-enter-to {
  transform: scale(1) translate(0px, 0px);
}
</style>
