## 用户部分

redis存储验证码，并用于验证码的验证。

验证码存储进sql server用于存储验证码

设计规格：

```js
redis.set('register:17738389852', 'verify code')
redis.set('forgetpwd:17738389852', 'verify code')
```

### 注册

用户根据手机号注册，注册时发送短信验证码（已购买短信4000条用于测试）

注册时需要判断验证码是否合法
一个手机号仅可注册一个用户

### 登录

用户登录时以手机号和密码进行登录

需要区分登录账户存在与否、登录密码是否正确。

登录返回jwt，时效为30天。

后端需要提供刷新token接口，前端按需进行刷新token，前端请求接口时需要传递token。

后端将token存到redis，如果用户信息不再token里，则说明没有登录

设计规格：

```js
redis.set('token:{userid}', 'token')
```

### 找回密码

在登录页面增加一个跳转按钮到找回密码页面，页面提供两个输入框：手机号、验证码

用户可通过验证码找回密码

### 个人中心

在首页显示一个头像，如果没有头像显示默认头像

头像支持hover效果，移入时有如下功能按钮：用户信息、我的邀请、退出登录

点击我的邀请：跳转至我的邀请页面，展示历史邀请信息，可能有可操作的项（目前消息仅有一个团队邀请信息），

点击用户信息：弹出模态框，展示如下数据：邮箱、手机号、用户名；

点击退出登录：调用退出登录接口，清空localstorage，并返回至登录页

## 项目部分

### 首页

首页显示项目列表，项目可归属文件夹，项目可以拖动排序，并可以拖动到文件夹内部，文件夹也可以拖动到另一个文件夹内部

快捷键操作仅实现：`ctrl+c`、`ctrl+v`

右键菜单实现，新建项目、新建文件夹、复制，剪切，粘贴、删除功能，复制、剪切、粘贴、删除对项目和文件夹均适用

双击文件夹操作可跳转下一个文 件内部，如果双击的是项目则跳转项目详情

### 创建项目

创建项目需要输入项目名字

### 项目详情

项目详情左侧显示项目的文件夹和文件夹内的文档

中间显示文档内容，如果没有选择到文档显示空。

左侧文件夹有右键功能：新建文件夹、重命名文件夹、删除文件夹、新建文档

左侧文档有右键功能：删除文档、查看历史信息

新建文件夹按钮：点击之后，弹出模态框，输入文件夹名字，并选择父级文件夹（父级文件夹可以从外部选择传入）进行创建。

重命名文件夹：可以对文件夹进行重命名，仅限项目拥有者和文件夹创建者

删除文件夹：可以对文件夹进行删除，仅限项目拥有者和文件夹创建者

新建文档：点击之后，弹出模态框，输入文件名字，并可以选择所属文件夹（所属文件夹可以从外部选择传入）进行创建。

删除文档：可以对文档进行删除，仅限项目拥有者和文档创建者

查看历史信息：可以对一个文档进行历史信息查看，显示历史编辑列表，列表项有修改人，修改时间，预览更改，回滚到该版本。
