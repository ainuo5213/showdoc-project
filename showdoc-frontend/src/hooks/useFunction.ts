import { customRef, computed, ref, onMounted, onBeforeUnmount } from "vue";
import { EntityMode, IProjectItem } from "@/types/project";
import {
  clearSelectedEntity,
  copyToClipBardMouseLeft,
  default as contextmenuData,
} from "@/hooks/contextmenu";
import axios from "@/utils/http";
import { AxiosRequestConfig } from "axios";
import { IDataResult } from "@/types/data";
import { ElMessage } from "element-plus";
import {
  cancelRenaming,
  default as projectState,
  renameData,
} from "@/hooks/project";
import { renamFolderOrProject } from "@/api/project";

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

export function useShortCut(ref: { value: any }) {
  onMounted(() => {
    (ref.value as HTMLElement).addEventListener("keyup", onKeyUp);
  });

  onBeforeUnmount(() => {
    (ref.value as HTMLElement).removeEventListener("keyup", onKeyUp);
  });

  async function onKeyUp(e: KeyboardEvent) {
    // cut
    if (e.ctrlKey && e.code == "KeyX") {
      ElMessage({
        type: "success",
        message: "已加入剪切板",
      });
      copyToClipBardMouseLeft(EntityMode.Cut);
      clearSelectedEntity();
    }
    // copy：暂时没有实现
    else if (e.ctrlKey && e.code == "KeyC") {
      console.log(111);
    }
  }

  return {};
}

export function useRename(data: IProjectItem) {
  const nameRef = ref("");
  const isRenaming = computed(() => {
    return (
      projectState.namedEntity.value.objectID != 0 &&
      projectState.namedEntity.value.objectID == data.objectID &&
      projectState.namedEntity.value.type == data.type
    );
  });
  const onRename = async (): Promise<void> => {
    if (nameRef.value.length == 0) {
      ElMessage({
        type: "error",
        message: "项目名长度必须大于1",
      });
      return;
    }

    if (nameRef.value == projectState.namedEntity.value.name) {
      cancelRenaming();
      return;
    }
    const res = await renamFolderOrProject({
      objectID: projectState.namedEntity.value.objectID,
      name: nameRef.value,
      type: projectState.namedEntity.value.type,
    });
    if (res.errno == 0 && res.data) {
      ElMessage({
        type: "success",
        message: "重命名成功",
      });
    } else {
      ElMessage({
        type: "error",
        message: res.errmsg || "重命名失败",
      });
    }
    renameData(nameRef.value);
  };

  return {
    onRename,
    name: nameRef,
    isRenaming,
  };
}
