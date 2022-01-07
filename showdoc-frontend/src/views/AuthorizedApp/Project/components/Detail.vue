<template>
  <div class="detail-container">
    <div v-if="data.documentID > 0">
      <div class="title-box">
        <h2 class="title">{{ data.title }}</h2>
      </div>
      <div class="content-box">
        <div class="content">
          <md-preview v-if="data != null" :data="data"></md-preview>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue-demi";
import MdPreview from "@/components/MarkdownPreview/index.vue";
import { default as detailState } from "@/hooks/detail";

export default defineComponent({
  components: {
    MdPreview,
  },
  setup() {
    return {
      data: detailState.document,
    };
  },
});
</script>

<style scoped lang="less">
.detail-container {
  position: relative;
  box-shadow: 0 1px 6px #ccc;
  background-color: #fff;
  border-bottom: 1px solid #d9d9d9;
  margin-bottom: 20px;
  width: 95%;
  min-height: 750px;
  margin-left: auto;
  margin-right: auto;
  padding: 20px;

  .title-box {
    border-bottom: 1px solid #ebebeb;
    padding-bottom: 10px;
    width: 100%;
    margin: 10px auto;
    text-align: center;
  }

  h2 {
    display: block;
    font-size: 1.5em;
    margin-block-start: 0.83em;
    margin-block-end: 0.83em;
    margin-inline-start: 0px;
    margin-inline-end: 0px;
    font-weight: bold;
    text-align: center;
  }

  .content-box {
    width: 95%;
    margin: 0 auto;
    background-color: #fff;

    .content {
      padding: 10px 10px 90px;
      overflow: hidden;
      font-size: 11pt;
      line-height: 1.7;
      color: #333;
    }
  }
}
</style>
