[TOC]

## 用户注册、登录相关

### 【已完成】【API】发送验证码

发送验证码区分类型，注册和找回密码。

url: `/api/auth/message`

method: `post`

data:

```json
{
  cellphone: "",
  type: 0
}
```

return:

```json
{
  errno: 0,
  errmsg: "",
  data: null
}
```



### 【已完成】【API】注册

注册需要判断：

	1. 用户验证码是否合法
	2. 用户是否已注册（手机号判重）

表单项有：

用户名、手机号、密码、验证码，皆为必填项

url: `/api/auth/register`

method: `post`

data:

```json
{
  username: "",
  cellphone: "",
  password: "",
  verifyCode: ""
}
```

return:

```json
{
  errno: 0,
  errmsg: "",
  data: null
}
```



### 【已完成】【API】登录

用户名和密码登录：需要判断用户是否存在、密码是否正确

登录之后获取数据存到本地的store

登录返回token

url: `/api/auth/login`

method: `post`

data:

```json
{
  cellphone: "",
  password: ""
}
```

return:

```json
{
  errno: 0,
  errmsg: "",
  data: {
    userID: 0,
    username: "",
    expires: "",
    token: ""
  }
}
```



### 【已完成】【API】找回密码

找回密码表单项：手机号、验证码、新密码、确认密码，皆为必填项

url: `/api/auth/passforget`

method: `post`

data:

```json
{
  cellphone: "",
  password: "",
  verifyCode: ""
}
```

return:

```json
{
  errno: 0,
  errmsg: "",
  data: [
    {
      userID: 0, // 创建者ID
      parentID: 0, // 父级文件夹ID
      type: 0, // 类型
      name: "", // 名字
      createTime: "", // 创建名字
      objectID: 0 // ID
    }
  ]
}
```



## 首页相关

### 【已完成】【API】获取用户信息

用户信息含用户名、头像地址、邮箱等信息

url: `/api/user/info`

method: `get`

return:

```json
{
  errno: 0,
  errmsg: "",
  data: {
    email: "",
    cellphone: "",
    username: "",
    headImg: ""
  }
}
```

### 【已完成】【API】获取文件夹数据

url: `/api/project/list?folderID={folderID}`

method: `get`

return:

```json
{
  errno: 0,
  errmsg: "",
  data: [
    {
      userID: 0, // 创建者ID
      parentID: 0, // 父级文件夹ID
      type: 0, // 类型。0：文件夹；1：项目
      name: "", // 名字
      createTime: "", // 创建名字
      objectID: 0 // ID
    }
  ]
}
```



### 【已完成】【API】新建项目或文件夹

url: `/api/project/create`

method: `post`

data:

```json
{
  folderID: 1, // 当前所在的文件夹ID
  type: 0, // 文件夹为0；项目为1
  name: "" // 名字
}
```

return:

```json
{
  errno: 0,
  errmsg: "",
  data: {
      userID: 0,
      parentID: 0,
      type: 0,
      name: "",
      createTime: "",
      objectID: 0
  }
}
```



### 【已完成】【API】删除项目或文件夹

url: `/api/project/delete`

method: `post`

data:

```json
{
  type: 0, // 文件夹为0；项目为1
  objectID: 0 // 删除项ID
}
```

return:

```json
{
  errno: 0,
  errmsg: "",
  data: null
}
```



### 【已完成】【API】移动文件夹或移动项目

url: `/api/project/move`

method: `post`

data:

```json
{
  folderID: 1, // 目标文件夹ID
  type: 0, // 当前移动项类型。文件夹为0；项目为1
  objectID: 0 // 当前文件夹或项目ID
}
```

return:

```json
{
  errno: 0,
  errmsg: "",
  data: true
}
```



### 【API】复制文件夹或复制项目

url: `/api/project/copy`

method: `post`

data:

```json
{
  userID: 0,
  folderID: 1,
  type: 0,
  objectID: 0
}
```

return:

```json
{
  errno: 0,
  errmsg: "",
  data: null
}
```



### 【已完成】【API】重命名文件夹或项目

url: `/api/project/rename`

method: `post`

data:

```json
{
  name: "111",
  type: 0,
  objectID: 0
}
```

return:

```json
{
  errno: 0,
  errmsg: "",
  data: true
}
```

### 【API】搜索加入项目

url: `/api/invitation/search?key={key}`

method: `get`

return:

```json
{
  errno: 0,
  errmsg: "",
  data: [
      {
          projectID: 0,
          projectName: "",
          creator: ""
      }
  ]
}
```



### 【API】根据名字搜索用户

url: `/api/user/search?key={keyword}`

return:

```json
{
  errno: 0,
  errmsg: "",
  data: [
    {
      ID: 123,
      username: "sad"
    }
  ]
}
```



### 【已完成】【API】退出登录

url：`/api/user/logout`

method: `post`

return:

```json
{
  errno: 0,
  errmsg: "",
  data: null
}
```



## 邀请列表

### 【API】邀请加入项目

url: `/api/invitation/invite`

method: `post`

data: 

```json
{
  invite: 1, // 邀请人ID
  invited: 2, // 被邀请者ID
  projectID: 3 // 团队ID
}
```

return:

```json
{
  errno: 0,
  errmsg: "",
  data: null
}
```



### 【API】获取邀请列表

url：`/api/invitation/list?page={page}&userID={userID}`

method: `get`

return:

```json
{
  errno: 0,
  errmsg: "",
  data: {
    totalCount: 10,
    items: [
      {
        invitationID: 1,
        invite: 1,
        inviteUsername: "",
        invited: 1,
        invitedUsername: ""
        status: 1,
        createTime: ""
      }
    ]
  }
}
```



### 【API】接受项目邀请

url：`/api/invitation/operation`

method: `post`

data:

```json
{
  userID: 0,
  invitationID: 0,
  status: 0
}
```

return:

```json
{
  errno: 0,
  errmsg: "",
  data: null
}
```



## 项目详情

### 【API】获取项目文件夹、文档（左侧数据）

url：`/api/document/menu?projedtID={projectID}`

method: `get`

return:

```json
{
  errno: 0,
  errmsg: "",
  data: [
    {
      projectID: 0,
      name: "",
      objectID: 0,
      parentID: "",
      type: 0
    }
  ]
}
```



### 【API】获取文档内容

url：`/api/document/content?documentID={documentID}`

method: `get`

return:

```json
{
  errno: 0,
  errmsg: "",
  data: {
    documentID: "",
    title: "",
    content: "",
    createTime: ""
  }
}
```



### 【API】新建文档或新建文件夹

url：`/api/document/new`

method: `get`

data:

```json
{
  userID: 0,
  folderID: 0,
  title: "",
  type: 0
}
```

return:

```json
{
  errno: 0,
  errmsg: "",
  data: {
    projectID: 0,
    name: "",
    objectID: 0,
    parentID: "",
    type: 0
  }
}
```



### 【API】删除文件夹或删除文档

url：`/api/document/delete`

method: `post`

data:

```json
{
  userID: 0,
  objectID: 0,
  type: 0
}
```

return:

```json
{
  errno: 0,
  errmsg: "",
  data: null
}
```



### 【API】查看文档编辑历史

url：`/api/document/history?documentID={documentID}&page={page}`

method: `get`

return:

```json
{
  errno: 0,
  errmsg: "",
  data: {
    totalCount: 0,
    items: [
      {
        userID: 0,
        creator: "",
        documentID: 0,
        title: "",
        createTime: ""
      }
    ]
  }
}
```



### 【API】修改文档

url：`/api/document/save`

method: `post`

data:

```json
{
  userID: 0,
  content: "",
  documentID: 0
}
```



return:

```json
{
  errno: 0,
  errmsg: "",
  data: null
}
```



### 【API】修改文件夹路径

url：`/api/document/move`

method: `post`

data:

```json
{
  userID: 0,
  objectID: 0,
  folderID: 0,
  type: 0
}
```

return:

```json
{
  errno: 0,
  errmsg: "",
  data: null
}
```



### 【API】重命名文档或文件夹

url：`/api/document/rename`

method: `post`

data:

```json
{
  userID: 0,
  objectID: 0,
  name: "",
  type: 0
}
```

return:

```json
{
  errno: 0,
  errmsg: "",
  data: null
}
```



### 【API】对比版本

url：`/api/document/historyDetail?historyID={historyID}`

method: `get`

return:

```json
{
  errno: 0,
  errmsg: "",
  data: {
    currentVersion: {
      documentID: 0,
      title: "",
      content: "",
      createTime: "",
      creatorID: ""
      creator: ""
    },
    historyVersion: {
      documentID: 0,
      title: "",
      content: "",
      createTime: "",
      creatorID: "",
      creator: ""
    }
  }
}
```



### 【API】回滚到某版本

url：`/api/document/rollback`

method: `post`

data:

```json
{
  documentID: 123,
  historyID: 123,
  userID: 123
}
```



return:

```json
{
  errno: 0,
  errmsg: "",
  data: null
}
```









