import { customRef, computed } from "vue";
import { EntityMode, IProjectItem, ProjectItemEnums } from "@/types/project";
import { default as contextmenuData } from "@/hooks/contextmenu";

export function useDebouncedRef(value: string, delay = 200) {
  let timeout: number;
  return customRef((track, trigger) => {
    return {
      get() {
        track();
        return value;
      },
      set(newValue: string) {
        clearTimeout(timeout);
        timeout = setTimeout(() => {
          value = newValue;
          trigger();
        }, delay);
      },
    };
  });
}

export function useEntitySelection(data: IProjectItem) {
  const isSelectedRef = computed(() => {
    // 选中实体
    const contextEntitySelected =
      contextmenuData.entity?.value?.objectID == data.objectID &&
      contextmenuData.entity?.value?.type == data.type;

    // 有clipboard的操作
    const contextEntityClipboard =
      contextmenuData.clipBoard.value.mode != EntityMode.None &&
      contextmenuData.clipBoard.value.clipBoardEntity != null &&
      contextmenuData.clipBoard.value.clipBoardEntity.objectID ==
        data.objectID &&
      contextmenuData.clipBoard?.value?.clipBoardEntity.type == data.type;
    return contextEntitySelected || contextEntityClipboard;
  });

  return {
    isSelected: isSelectedRef,
  };
}
