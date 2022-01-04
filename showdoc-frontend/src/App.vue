<template>
  <router-view></router-view>
</template>

<script lang="ts">
import { closeContextMenu } from "@/hooks/contextmenu";
import { defineComponent, onBeforeMount, onMounted } from "vue-demi";


export default defineComponent({
  setup() {
    const onAppClick = function (e: MouseEvent) {
      closeContextMenu();
    };
    const onAppContextMenu = function (e: MouseEvent) {
      closeContextMenu();
    };
    onMounted(async () => {
      const app = document.getElementById("app");
      app!.addEventListener("click", onAppClick);
      app!.addEventListener("contextmenu", onAppContextMenu);
    });
    onBeforeMount(() => {
      const app = document.getElementById("app");
      app!.removeEventListener("click", onAppClick);
      app!.removeEventListener("contextmenu", onAppContextMenu);
    });
  },
});
</script>

<style>
#app {
  background-color: rgba(0, 0, 0, 0.2) !important;
  padding: 20px;
  box-sizing: border-box;
  max-height: 100vh;
  max-width: 100vw;
  overflow: hidden;
}
</style>
