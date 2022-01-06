import { customRef, computed, ref } from "vue";
import { EntityMode, IProjectItem } from "@/types/project";
import { default as contextmenuData } from "@/hooks/contextmenu";
import axios from "@/utils/http";
import { AxiosRequestConfig } from "axios";
import { IDataResult } from "@/types/data";

export function useDebouncedRef(value: string, delay = 200) {
  let timeout: any;
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
    // 左键选中的实体
    const selectedEntity =
      contextmenuData.selectEntity.value.objectID == data.objectID &&
      contextmenuData.selectEntity.value.type == data.type;

    // 右键选中的实体
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
    return contextEntitySelected || contextEntityClipboard || selectedEntity;
  });

  return {
    isSelected: isSelectedRef,
  };
}

export function useRequest<T>(delay?: number) {
  const loadingRef = ref(false);
  const delayRef = ref(delay || 0);
  const showDelayRef = ref(delayRef.value > 0);
  const request: (
    config: AxiosRequestConfig<any>
  ) => Promise<IDataResult<T>> = async (config: AxiosRequestConfig<any>) => {
    let res: any;
    try {
      loadingRef.value = true;
      res = await axios(config);
      showDelayRef.value = delay && delay > 0 && res.errno == 0 && res.data;
      if (showDelayRef.value) {
        const timerRef = ref();
        timerRef.value = setInterval(() => {
          if (delayRef.value <= 0) {
            delayRef.value = 0;
            loadingRef.value = false;
            setTimeout(() => (delayRef.value = delay || 0), 0);
            clearInterval(timerRef.value);
          } else {
            delayRef.value = delayRef.value - 1;
          }
        }, 1000);
      }
    } finally {
      if (!showDelayRef.value) {
        loadingRef.value = false;
      }
    }
    return res as IDataResult<T>;
  };

  return {
    request,
    loading: loadingRef,
    delay: delayRef,
  };
}
