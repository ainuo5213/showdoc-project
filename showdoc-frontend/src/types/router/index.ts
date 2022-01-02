import { RouteMeta } from "vue-router";

export interface MetaData extends RouteMeta {
  requireAuth?: boolean;
  title?: string;
}
