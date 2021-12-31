[TOC]

## 用户注册、登录相关

### 【API】发送验证码

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



### 【API】注册

注册需要判断：

	1. 用户验证码是否合法
 	2. 用户是否已注册（手机号判重）

表单项有：

用户名、手机号、密码、确认密码、验证码，皆为必填项

url: `/api/auth/register`

method: `post`

data:

```json
{
  username: "",
  cellphone: "",
  password: ""
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



### 【API】登录

用户名和密码登录：需要判断用户是否存在、密码是否正确

登录之后获取数据存到本地的store

登录返回token

url: `/api/auth/login`

method: `post`

data:

```json
{
  field: "",
  type: 0,
  password: ""
}
```

return:

```json
{
  errno: 0,
  errmsg: "",
  data: {
    userId: 0,
    username: "",
    teamId: 0,
    expires: "",
    token: ""
  }
}
```



### 【API】找回密码

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
      userId: 0, // 创建者ID
      parentId: 0, // 父级文件夹ID
      type: 0, // 类型
      name: "", // 名字
      createTime: "", // 创建名字
      objectId: 0 // id
    }
  ]
}
```



## 首页相关

### 【API】获取用户信息

用户信息含用户名、头像地址、邮箱等信息

url: `/api/user/info?userId={userId}`

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
    teamId: "",
    teamName: ""
  }
}
```

### 【API】获取文件夹数据

url: `/api/project/list?folderId={folderId}`

method: `get`

return:

```json
{
  errno: 0,
  errmsg: "",
  data: [
    {
      userId: 0, // 创建者ID
      parentId: 0, // 父级文件夹ID
      type: 0, // 类型
      name: "", // 名字
      createTime: "", // 创建名字
      objectId: 0 // id
    }
  ]
}
```



### 【API】新建项目或文件夹

url: `/api/project/create`

method: `post`

data:

```json
{
  userId: 0,
  type: 0
}
```

return:

```json
{
  errno: 0,
  errmsg: "",
  data: {
    objectId: 0,
    type: 0
  }
}
```



### 【API】删除项目或文件夹

url: `/api/project/delete`

method: `post`

data:

```json
{
  userId: 0,
  type: 0,
  objectId: 0
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



### 【API】移动文件夹或移动项目

url: `/api/project/move`

method: `post`

data:

```json
{
  userId: 0,
  folderId: 1,
  type: 0,
  objectId: 0
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



### 【API】复制文件夹或复制项目

url: `/api/project/copy`

method: `post`

data:

```json
{
  userId: 0,
  folderId: 1,
  type: 0,
  objectId: 0
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



### 【API】重命名文件夹或项目

url: `/api/project/rename`

method: `post`

data:

```json
{
  userId: 0,
  name: "111",
  type: 0,
  objectId: 0
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

### 【API】创建团队

url: `/api/user/newTeam?userId={userId}`

method: `post`

return:

```json
{
  errno: 0,
  errmsg: "",
  data: 12 // team id
}
```



### 【API】邀请加入团队

url: `/api/user/invite`

method: `post`

data: 

```json
{
  invite: 1, // 邀请人id
  invited: 2, // 被邀请者id
  team: 3 // 团队id
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



### 【API】根据名字搜索用户

url: `/api/user/search?key={keyword}`

return:

```json
{
  errno: 0,
  errmsg: "",
  data: [
    {
      id: 123,
      username: "sad"
    }
  ]
}
```



### 【API】退出登录

url：`/api/user/logout?id={id}`

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

### 【API】获取邀请列表

url：`/api/invitation/list?page={page}&userId={userId}`

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
        invitationId: 1,
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



### 【API】接受团队邀请

url：`/api/invitation/operation`

method: `post`

data:

```json
{
  userId: 0,
  invitationId: 0,
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

url：`/api/document/menu?projedtId={projectId}`

method: `get`

return:

```json
{
  errno: 0,
  errmsg: "",
  data: [
    {
      projectId: 0,
      name: "",
      objectId: 0,
      parentId: "",
      type: 0
    }
  ]
}
```



### 【API】获取文档内容

url：`/api/document/content?documentId={documentId}`

method: `get`

return:

```json
{
  errno: 0,
  errmsg: "",
  data: {
    documentId: "",
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
  userId: 0,
  folderId: 0,
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
    projectId: 0,
    name: "",
    objectId: 0,
    parentId: "",
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
  userId: 0,
  objectId: 0,
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

url：`/api/document/history?documentId={documentId}&page={page}`

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
        userId: 0,
        creator: "",
        documentId: 0,
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
  userId: 0,
  content: "",
  documentId: 0
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
  userId: 0,
  objectId: 0,
  folderId: 0,
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
  userId: 0,
  objectId: 0,
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

url：`/api/document/historyDetail?historyId={historyId}`

method: `get`

return:

```json
{
  errno: 0,
  errmsg: "",
  data: {
    currentVersion: {
      documentId: 0,
      title: "",
      content: "",
      createTime: "",
      creatorId: ""
      creator: ""
    },
    historyVersion: {
      documentId: 0,
      title: "",
      content: "",
      createTime: "",
      creatorId: "",
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
  documentId: 123,
  historyId: 123,
  userId: 123
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









