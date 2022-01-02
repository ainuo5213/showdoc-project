<template>
  <v-btn depressed large color="success" @click="log">click</v-btn>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "HelloWorld",

  data() {
    return {};
  },
  methods: {
    log() {
      console.log(111);
    }
  }
});
</script>
