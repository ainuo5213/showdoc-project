export function isDev() {
  return process.env.NODE_ENV === "development";
}

export function isEmptyObject(object: {}) {
  return Reflect.ownKeys(object).length === 0;
}
