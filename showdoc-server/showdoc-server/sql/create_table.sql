GO

/****** Object:  Table [dbo].[Documents]    Script Date: 2022/1/2 0:58:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Documents](
	[DocumentID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[Title] [nvarchar](20) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[FolderID] [int] NOT NULL,
	[CreatorID] [int] NOT NULL,
	[UpdatorID] [int] NOT NULL,
	[DeleteStatus] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[DocumentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Folders]    Script Date: 2022/1/2 0:58:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Folders](
	[FolderID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[ParentID] [int] NOT NULL,
	[CreatorID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[DeleteStatus] [int] NOT NULL,
	[SortTime] [datetime] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Folders] PRIMARY KEY CLUSTERED 
(
	[FolderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[HistoryDocuments]    Script Date: 2022/1/2 0:58:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HistoryDocuments](
	[HistoryDocumentID] [int] IDENTITY(1,1) NOT NULL,
	[DocumentID] [int] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[CreatorID] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_HistoryDocuments] PRIMARY KEY CLUSTERED 
(
	[HistoryDocumentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ProjectInvitations]    Script Date: 2022/1/2 0:58:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProjectInvitations](
	[ProjectInvitationID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[InviteByID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[TeamID] [int] NOT NULL,
	[CheckUserID] [int] NOT NULL,
	[CheckTime] [datetime] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_TeamInvitations] PRIMARY KEY CLUSTERED 
(
	[ProjectInvitationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Projects]    Script Date: 2022/1/2 0:58:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Projects](
	[ProjectID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[CreatorID] [int] NOT NULL,
	[DeleteStatus] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[ProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ProjectUsers]    Script Date: 2022/1/2 0:58:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProjectUsers](
	[ProjectUserID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[SortTime] [datetime] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ProjectUsers] PRIMARY KEY CLUSTERED 
(
	[ProjectUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Sms]    Script Date: 2022/1/2 0:58:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Sms](
	[SmsID] [int] IDENTITY(1,1) NOT NULL,
	[Cellphone] [nvarchar](20) NOT NULL,
	[Content] [nvarchar](128) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_VerifyCodes] PRIMARY KEY CLUSTERED 
(
	[SmsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 2022/1/2 0:58:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](20) NOT NULL,
	[Cellphone] [nvarchar](20) NOT NULL,
	[HeadImg] [nvarchar](512) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Documents] ADD  CONSTRAINT [DF_Documents_Title]  DEFAULT ('') FOR [Title]
GO

ALTER TABLE [dbo].[Documents] ADD  CONSTRAINT [DF_Documents_Content]  DEFAULT ('') FOR [Content]
GO

ALTER TABLE [dbo].[Documents] ADD  CONSTRAINT [DF_Documents_FolderID]  DEFAULT ((0)) FOR [FolderID]
GO

ALTER TABLE [dbo].[Documents] ADD  CONSTRAINT [DF_Documents_DeleteStatus]  DEFAULT ((0)) FOR [DeleteStatus]
GO

ALTER TABLE [dbo].[Documents] ADD  CONSTRAINT [DF_Documents_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO

ALTER TABLE [dbo].[Documents] ADD  CONSTRAINT [DF_Documents_UpdateTime]  DEFAULT (getdate()) FOR [UpdateTime]
GO

ALTER TABLE [dbo].[Folders] ADD  CONSTRAINT [DF_Folders_Name]  DEFAULT ('') FOR [Name]
GO

ALTER TABLE [dbo].[Folders] ADD  CONSTRAINT [DF_Folders_ParentID]  DEFAULT ((0)) FOR [ParentID]
GO

ALTER TABLE [dbo].[Folders] ADD  CONSTRAINT [DF_Folders_DeleteStatus]  DEFAULT ((0)) FOR [DeleteStatus]
GO

ALTER TABLE [dbo].[Folders] ADD  CONSTRAINT [DF_Folders_SortTime]  DEFAULT (getdate()) FOR [SortTime]
GO

ALTER TABLE [dbo].[Folders] ADD  CONSTRAINT [DF_Folders_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO

ALTER TABLE [dbo].[Folders] ADD  CONSTRAINT [DF_Folders_UpdateTime]  DEFAULT (getdate()) FOR [UpdateTime]
GO

ALTER TABLE [dbo].[HistoryDocuments] ADD  CONSTRAINT [DF_HistoryDocuments_Content]  DEFAULT ('') FOR [Content]
GO

ALTER TABLE [dbo].[HistoryDocuments] ADD  CONSTRAINT [DF_HistoryDocuments_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO

ALTER TABLE [dbo].[ProjectInvitations] ADD  CONSTRAINT [DF_ProjectInvitations_CheckUserID]  DEFAULT ((-100)) FOR [CheckUserID]
GO

ALTER TABLE [dbo].[ProjectInvitations] ADD  CONSTRAINT [DF_ProjectInvitations_CheckTime]  DEFAULT ('1900-01-01') FOR [CheckTime]
GO

ALTER TABLE [dbo].[ProjectInvitations] ADD  CONSTRAINT [DF_ProjectInvitations_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO

ALTER TABLE [dbo].[ProjectInvitations] ADD  CONSTRAINT [DF_ProjectInvitations_UpdateTime]  DEFAULT (getdate()) FOR [UpdateTime]
GO

ALTER TABLE [dbo].[Projects] ADD  CONSTRAINT [DF_Projects_Name]  DEFAULT ('') FOR [Name]
GO

ALTER TABLE [dbo].[Projects] ADD  CONSTRAINT [DF_Projects_DeleteStatus]  DEFAULT ((0)) FOR [DeleteStatus]
GO

ALTER TABLE [dbo].[Projects] ADD  CONSTRAINT [DF_Projects_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO

ALTER TABLE [dbo].[Projects] ADD  CONSTRAINT [DF_Projects_UpdateTime]  DEFAULT (getdate()) FOR [UpdateTime]
GO

ALTER TABLE [dbo].[ProjectUsers] ADD  CONSTRAINT [DF_ProjectUsers_SortTime]  DEFAULT (getdate()) FOR [SortTime]
GO

ALTER TABLE [dbo].[ProjectUsers] ADD  CONSTRAINT [DF_ProjectUsers_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO

ALTER TABLE [dbo].[Sms] ADD  CONSTRAINT [DF_Sms_Content]  DEFAULT (N'��������') FOR [Content]
GO

ALTER TABLE [dbo].[Sms] ADD  CONSTRAINT [DF_Sms_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Username]  DEFAULT ('') FOR [Username]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Email]  DEFAULT ('') FOR [Email]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Cellphone]  DEFAULT ('') FOR [Cellphone]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_HeadImg]  DEFAULT ('') FOR [HeadImg]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Password]  DEFAULT ('') FOR [Password]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ĵ�ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'DocumentID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������ĿID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'ProjectID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ļ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'Title'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ļ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'Content'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ŀ�����ļ���ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'FolderID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'UpdatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ɾ��״̬��0��δɾ����1����ɾ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'DeleteStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ĵ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID��Ĭ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Folders', @level2type=N'COLUMN',@level2name=N'FolderID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ļ�������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Folders', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ļ���ID��Ĭ���޸��ļ��С�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Folders', @level2type=N'COLUMN',@level2name=N'ParentID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ģ�崴����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Folders', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ļ���������ĿID��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Folders', @level2type=N'COLUMN',@level2name=N'ProjectID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ļ������͡�0����Ŀ�ļ��У�1���ĵ��ļ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Folders', @level2type=N'COLUMN',@level2name=N'Type'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ɾ��״̬��0��δɾ����1����ɾ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Folders', @level2type=N'COLUMN',@level2name=N'DeleteStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Folders', @level2type=N'COLUMN',@level2name=N'SortTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Folders', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Folders', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ļ��б�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Folders'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HistoryDocuments', @level2type=N'COLUMN',@level2name=N'HistoryDocumentID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ĵ�ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HistoryDocuments', @level2type=N'COLUMN',@level2name=N'DocumentID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ĵ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HistoryDocuments', @level2type=N'COLUMN',@level2name=N'Content'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ĵ�������ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HistoryDocuments', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ģ�崴��ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HistoryDocuments', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ʷ�ĵ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HistoryDocuments'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProjectInvitations', @level2type=N'COLUMN',@level2name=N'ProjectInvitationID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProjectInvitations', @level2type=N'COLUMN',@level2name=N'UserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������ID���������ߺͱ�������Ϊͬһ��ʱ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProjectInvitations', @level2type=N'COLUMN',@level2name=N'InviteByID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ǰ״̬��-1���Ѿܾ���0�������룻1���Ѽ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProjectInvitations', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ŷ�ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProjectInvitations', @level2type=N'COLUMN',@level2name=N'TeamID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProjectInvitations', @level2type=N'COLUMN',@level2name=N'CheckUserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProjectInvitations', @level2type=N'COLUMN',@level2name=N'CheckTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProjectInvitations', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProjectInvitations', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ŷ����뺯��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProjectInvitations'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ĿID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Projects', @level2type=N'COLUMN',@level2name=N'ProjectID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ŀ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Projects', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ŀ������ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Projects', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ɾ��״̬��0��δɾ����1����ɾ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Projects', @level2type=N'COLUMN',@level2name=N'DeleteStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Projects', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Projects', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ŀ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Projects'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProjectUsers', @level2type=N'COLUMN',@level2name=N'ProjectUserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�û�ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProjectUsers', @level2type=N'COLUMN',@level2name=N'UserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ĿID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProjectUsers', @level2type=N'COLUMN',@level2name=N'ProjectID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProjectUsers', @level2type=N'COLUMN',@level2name=N'SortTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProjectUsers', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ŀ�û���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProjectUsers'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sms', @level2type=N'COLUMN',@level2name=N'SmsID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���շ��ֻ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sms', @level2type=N'COLUMN',@level2name=N'Cellphone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sms', @level2type=N'COLUMN',@level2name=N'Content'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ��ͳɹ���true���ɹ���false��ʧ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sms', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sms', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID��Ĭ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'UserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�û���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'Username'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ַ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'Email'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�û��ֻ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'Cellphone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ͷ���ַ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'HeadImg'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'MD5����֮�������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'Password'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�û�ע�ᴴ��ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�û���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users'
GO


