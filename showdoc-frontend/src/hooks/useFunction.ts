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

export function useRequest<T>() {
  const loadingRef = ref(false);
  const request: (config: AxiosRequestConfig<any>) => Promise<IDataResult<T>> = async (config: AxiosRequestConfig<any>) => {
    let res: any;
    try {
      loadingRef.value = true;
      res = await axios(config);
    } finally {
      loadingRef.value = false;
    }
    return res as IDataResult<T>;
  };

  return {
    request,
    loading: loadingRef,
  };
}
