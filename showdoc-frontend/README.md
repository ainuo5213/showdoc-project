### 摘要

showdoc 前端基于 vue3.0，UI 框架部分使用`element-plus`

技术栈主要是：`vue3`+`element-plus`+`vue-router4`

### 收获与感悟

深入理解vue响应式系统原理

vue插件的封装和使用

### 关于reactivity

reactivity api，与 react的`useState`不同，其不依赖组件，在浏览器环境和node环境均可正常使用，所以我觉得这是一个很好的替代vuex成为响应式数仓的方案。

例如项目中`hooks`下的文件。

响应式相关问题：

1. 关于使用展开运算符展开reactive的数据之后，无响应式的问题：

>由于Proxy的机制，展开运算符执行的是getter方法，展开之后的数据不是一个响应式数据，而是一个普通的对象，要想使展开之后的对象仍然具有响应式可以使用`...toRefs(state)`

2. watch(composition api)监听的数据，必须是一个响应式数据，例如:`ref`、`reactive`，而不能监听一个普通对象或基础类型的数据。