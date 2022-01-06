<template>
  <router-view v-slot="{ Component }">
    <transition name="app" mode="out-in">
      <component :is="Component" />
    </transition>
  </router-view>
</template>

<script lang="ts">
import { closeContextMenu, clearSelectedEntity } from "@/hooks/contextmenu";
import { defineComponent, onBeforeMount, onMounted } from "vue-demi";

export default defineComponent({
  setup() {
    const onAppClick = function (e: MouseEvent) {
      closeContextMenu();
      clearSelectedEntity();
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

<style scoped lang="less">
.app-enter-from,
.app-leave-to {
  opacity: 0;
}
.app-enter-active,
.app-leave-active {
  transition: all 0.5s;
}
.app-enter-to,
.app-leave-from {
  opacity: 1;
}
.app-leave-to {
  position: absolute;
}
.app-move {
  transition: transform 0.5s ease;
}
</style>
