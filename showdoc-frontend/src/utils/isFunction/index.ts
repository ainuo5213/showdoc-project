export function isDev(): boolean {
  return process.env.NODE_ENV === "development";
}

export function isEmptyObject(object: Record<string, unknown>): boolean {
  return Reflect.ownKeys(object).length === 0;
}
