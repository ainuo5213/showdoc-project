using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Context;
using showdoc_server.Dtos.Json;
using showdoc_server.Dtos.Request.Document;
using showdoc_server.Dtos.Table;

namespace showdoc_server.Reponsitory.Document
{
    public class DocumentReponsitory : IDocumentReponsitory
    {
        public async Task<DocumentContentDTO> CreateDocument(int userID, int projectID, int folderID, string title)
        {
            // 是否是自己的参与的项目，且文件夹是否存在
            bool canIncrease = await SugarContext.Context.Queryable<Folders>()
                .Where(folder => folder.FolderID == folderID && folder.Type == Dtos.Request.Folder.FolderTypes.DocumentFolder)
                .InnerJoin<Projects>((folder, project) => folder.ProjectID == project.ProjectID && project.ProjectID == projectID && project.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete)
                .InnerJoin<ProjectUsers>((folder, project, projectUser) => projectUser.ProjectID == project.ProjectID && projectUser.UserID == userID)
                .AnyAsync();

            // 如果可以增加或folderID=0
            if (canIncrease || folderID == 0)
            {
                await SugarContext.Context.Insertable(new Documents()
                {
                    Content = string.Empty,
                    CreateTime = DateTime.Now,
                    CreatorID = userID,
                    DeleteStatus = DeleteStatuses.UnDelete,
                    FolderID = folderID,
                    ProjectID = projectID,
                    Title = title,
                    UpdateTime = DateTime.Now,
                    UpdatorID = userID,
                })
                    .ExecuteCommandAsync();
            }

            throw new Exception("folder is not valid");
        }

        public async Task<DocumentContentDTO> CreateFolder(int userID, int projectID, int folderID, string title)
        {
            // 是否是自己的参与的项目，且文件夹是否存在
            bool canIncrease = await SugarContext.Context.Queryable<Folders>()
                .Where(folder => folder.FolderID == folderID && folder.Type == Dtos.Request.Folder.FolderTypes.DocumentFolder)
                .InnerJoin<Projects>((folder, project) => folder.ProjectID == project.ProjectID && project.ProjectID == projectID && project.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete)
                .InnerJoin<ProjectUsers>((folder, project, projectUser) => projectUser.ProjectID == project.ProjectID && projectUser.UserID == userID)
                .AnyAsync();
            // 如果可以增加或folderID=0
            if (canIncrease || folderID == 0)
            {
                await SugarContext.Context.Insertable(new Folders()
                {
                    UpdateTime = DateTime.Now,
                    CreateTime = DateTime.Now,
                    CreatorID = userID,
                    DeleteStatus = Dtos.Json.DeleteStatuses.UnDelete,
                    Name = title,
                    ParentID = folderID,
                    ProjectID = projectID,
                    SortTime = DateTime.Now,
                    Type = Dtos.Request.Folder.FolderTypes.DocumentFolder,
                })
                    .ExecuteCommandAsync();
            }

            throw new Exception("folder is not valid");
        }

        public async Task<int> DeleteDocument(int userID, int documentID)
        {
            // 是否是文档创建者创建的
            bool isMine = await SugarContext.Context.Queryable<Documents>()
                .AnyAsync(document => document.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && document.CreatorID == userID && document.DocumentID == documentID);
            if (isMine)
            {
                throw new Exception("no permission");
            }

            return await SugarContext.Context.Updateable<Documents>()
                .SetColumns(document => new Documents()
                {
                    UpdateTime = DateTime.Now,
                    DeleteStatus = Dtos.Json.DeleteStatuses.Deleted,
                })
                .Where(document => document.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && document.CreatorID == userID && document.DocumentID == documentID)
                .ExecuteCommandAsync();
        }

        public async Task<int> DeleteFolder(int userID, int folderID)
        {
            // 是否是文件夹创建者创建的
            bool isMine = await SugarContext.Context.Queryable<Folders>()
                .AnyAsync(folder => folder.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && folder.CreatorID == userID && folder.FolderID == folderID && folder.Type == Dtos.Request.Folder.FolderTypes.DocumentFolder);
            if (!isMine)
            {
                throw new Exception("no permission");
            }

            // 文档下是否还有子文件
            bool hasChildFolders = await SugarContext.Context.Queryable<Folders>().AnyAsync(folder => folder.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && folder.ParentID == folderID && folder.Type == Dtos.Request.Folder.FolderTypes.DocumentFolder);
            bool hasChildDocuments = await SugarContext.Context.Queryable<Documents>().AnyAsync(document => document.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && document.FolderID == folderID);

            if (hasChildFolders || hasChildDocuments)
            {
                throw new Exception("folder is using. if you want to delete the folder, please delete the children at first");
            }

            return await SugarContext.Context.Updateable<Folders>()
                .SetColumns(folder => new Folders()
                {
                    UpdateTime = DateTime.Now,
                    DeleteStatus = Dtos.Json.DeleteStatuses.Deleted,
                })
                .Where(folder => folder.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && folder.CreatorID == userID && folder.FolderID == folderID && folder.Type == Dtos.Request.Folder.FolderTypes.DocumentFolder)
                .ExecuteCommandAsync();
        }

        public async Task<DocumentContentDTO> GetDocumentContent(int userID, int documentID)
        {
            // 是否能查看详情有权限控制
            // 1. 加入了该项目
            // 2. 项目或文档没有被删除
            DocumentContentDTO data = await SugarContext.Context.Queryable<Documents>()
               .Where(document => document.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && document.DocumentID == documentID)
               .InnerJoin<ProjectUsers>((document, projectUser) => document.ProjectID == projectUser.ProjectID && projectUser.UserID == userID)
               .InnerJoin<Projects>((document, projectUser, project) => projectUser.ProjectID == project.ProjectID && project.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete)
               .InnerJoin<Users>((document, projectUser, project, _user) => document.CreatorID == _user.UserID)
               .InnerJoin<Folders>((document, projectUser, project, _user, folder) => document.FolderID == folder.FolderID && folder.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete)
               .Select((document, projectUser, project, _user, folder) => new DocumentContentDTO()
               {
                   Content = document.Content,
                   CreateTime = document.CreateTime,
                   DocumentID = document.DocumentID,
                   Title = document.Title,
                   Creator = _user.Username,
                   FolderID = folder.FolderID,
                   FolderName = folder.Name,
                   ProjectID = document.ProjectID,
                   ProjectName = project.Name,
               })
               .FirstAsync();
            if (data == null)
            {
                throw new Exception("document has been deleted");
            }

            return data;
        }

        public async Task<ListItemDTO<DocumentHistoryDTO>> GetDocumentHistory(int userID, int documentID, int page)
        {
            // 只能查看当前自己加入的项目中的文档历史
            var data = SugarContext.Context.Queryable<HistoryDocuments>()
                .Where(historyDocument => historyDocument.DocumentID == documentID)
                .InnerJoin<Documents>((historyDocument, document) => historyDocument.DocumentID == document.DocumentID && document.DeleteStatus == DeleteStatuses.UnDelete)
                .InnerJoin<Projects>((historyDocument, document, project) => document.ProjectID == project.ProjectID && project.DeleteStatus == DeleteStatuses.UnDelete)
                .InnerJoin<ProjectUsers>((historyDocument, document, project, projectUser) => project.ProjectID == projectUser.ProjectID && projectUser.UserID == userID)
                .InnerJoin<Users>((historyDocument, document, project, projectUser, _user) => historyDocument.CreatorID == _user.UserID)
                .Select((historyDocument, document, project, projectUser, _user) => new DocumentHistoryDTO()
                {
                    DocumentID = document.DocumentID,
                    CreateTime = historyDocument.CreateTime,
                    Creator = _user.Username,
                    HistoryID = historyDocument.HistoryDocumentID,
                    Title = document.Title,
                });
            int cnt = await data.CountAsync();
            IEnumerable<DocumentHistoryDTO> res = await data.OrderBy(r => r.CreateTime, SqlSugar.OrderByType.Desc).ToPageListAsync(page, 20);

            return new ListItemDTO<DocumentHistoryDTO>()
            {
                Items = res,
                TotalCount = cnt
            };
        }

        public async Task<HistoryComparisonDTO> HistoryDocumentComparison(int userID, int historyID)
        {
            // 仅需查找是否是该项目成员，文档删除与否
            var query = SugarContext.Context.Queryable<HistoryDocuments>()
                .Where(historyDocument => historyDocument.HistoryDocumentID == historyID)
                .InnerJoin<Documents>((historyDocument, document) => historyDocument.DocumentID == document.DocumentID && document.DeleteStatus == DeleteStatuses.UnDelete)
                .InnerJoin<Projects>((historyDocument, document, project) => document.ProjectID == project.ProjectID && project.DeleteStatus == DeleteStatuses.UnDelete)
                .InnerJoin<ProjectUsers>((historyDocument, document, project, projectUser) => project.ProjectID == projectUser.ProjectID && projectUser.UserID == userID)
                .InnerJoin<Users>((historyDocument, document, project, projectUser, _user) => historyDocument.CreatorID == _user.UserID);
            var currentVersion = await query.Select((historyDocument, document, project, projectUser, _user) => new HistoryComparisonItemDTO()
            {
                Content = document.Content,
                CreateTime = document.CreateTime,
                Creator = _user.Username,
                CreatorID = _user.UserID,
                DocumentID = document.DocumentID,
                HistoryID = historyDocument.HistoryDocumentID,
                Title = document.Title,
            }).FirstAsync();
            if (currentVersion == null)
            {
                throw new Exception("document has been deleted");
            }
            var historyVersion = await query.Select((historyDocument, document, project, projectUser, _user) => new HistoryComparisonItemDTO()
            {
                Content = historyDocument.Content,
                CreateTime = historyDocument.CreateTime,
                Creator = _user.Username,
                CreatorID = _user.UserID,
                DocumentID = historyDocument.DocumentID,
                HistoryID = historyDocument.HistoryDocumentID,
                Title = document.Title,
            }).FirstAsync();

            return new HistoryComparisonDTO()
            {
                CurrentVersion = currentVersion,
                HistoryVersion = historyVersion,
            };
        }

        public async Task<int> RenameDocument(int userID, int documentID, string name)
        {
            // 查看文档删除与否，项目是否参加
            var document = await SugarContext.Context.Queryable<Documents>()
                .Where(document => document.DocumentID == documentID && document.DeleteStatus == DeleteStatuses.UnDelete)
                .InnerJoin<Projects>((document, project) => document.ProjectID == project.ProjectID && project.DeleteStatus == DeleteStatuses.UnDelete)
                .InnerJoin<ProjectUsers>((document, project, projectUser) => project.ProjectID == projectUser.ProjectID && projectUser.UserID == userID)
                .Select((document, project, projectUser) => document)
                .FirstAsync();
            if (document == null)
            {
                throw new Exception("document has been deleted");
            }

            return await SugarContext.Context.Updateable<Documents>()
                .SetColumns(document => new Documents()
                {
                    Title = name,
                    UpdateTime = DateTime.Now,
                    UpdatorID = userID
                })
                .Where(document => document.DocumentID == documentID && document.DeleteStatus == DeleteStatuses.UnDelete)
                .ExecuteCommandAsync();
        }

        public async Task<int> RenameFolder(int userID, int folderID, string name)
        {
            // 查看文件夹删除与否，项目是否参加
            var folder = await SugarContext.Context.Queryable<Folders>()
                .Where(folder => folder.FolderID == folderID && folder.DeleteStatus == DeleteStatuses.UnDelete && folder.Type == Dtos.Request.Folder.FolderTypes.DocumentFolder)
                .InnerJoin<Projects>((folder, project) => folder.ProjectID == project.ProjectID && project.DeleteStatus == DeleteStatuses.UnDelete)
                .InnerJoin<ProjectUsers>((folder, project, projectUser) => project.ProjectID == projectUser.ProjectID && projectUser.UserID == userID)
                .Select((folder, project, projectUser) => folder)
                .FirstAsync();
            if (folder == null)
            {
                throw new Exception("folder has been deleted");
            }
            return await SugarContext.Context.Updateable<Folders>()
                .SetColumns(document => new Folders()
                {
                    Name = name,
                    UpdateTime = DateTime.Now,
                })
                .Where(folder => folder.FolderID == folderID && folder.DeleteStatus == DeleteStatuses.UnDelete && folder.Type == Dtos.Request.Folder.FolderTypes.DocumentFolder)
                .ExecuteCommandAsync();
        }

        public async Task<int> RollbackDocument(int userID, int historyID)
        {
            var query = SugarContext.Context.Queryable<HistoryDocuments>()
                .Where(historyDocument => historyDocument.HistoryDocumentID == historyID)
                .InnerJoin<Documents>((historyDocument, document) => historyDocument.DocumentID == document.DocumentID && document.DeleteStatus == DeleteStatuses.UnDelete)
                .InnerJoin<Projects>((historyDocument, document, project) => document.ProjectID == project.ProjectID && project.DeleteStatus == DeleteStatuses.UnDelete)
                .InnerJoin<ProjectUsers>((historyDocument, document, project, projectUser) => project.ProjectID == projectUser.ProjectID && projectUser.UserID == userID);

            var document = await query.Select((historyDocument, document, project, projectUser) => document).FirstAsync();
            var historyDocument = await query.Select((historyDocument, document, project, projectUser) => historyDocument).FirstAsync();
            if (document == null || historyDocument == null)
            {
                throw new Exception("history or document has been deleted");
            }
            // 将该版本及之后的历史删掉
            await SugarContext.Context.Deleteable<HistoryDocuments>()
                .Where(historyDocument => historyDocument.HistoryDocumentID >= historyDocument.HistoryDocumentID && historyDocument.DocumentID == document.DocumentID)
                .ExecuteCommandAsync();
            // 将当前版本的内容修改为还原版本的内容
            await SugarContext.Context.Updateable<Documents>()
                .SetColumns(document => new Documents()
                {
                    Content = historyDocument.Content,
                    UpdateTime = DateTime.Now,
                    UpdatorID = userID
                })
                .Where(document => document.DocumentID == document.DocumentID && document.DeleteStatus == DeleteStatuses.UnDelete)
                .ExecuteCommandAsync();
            return 1;
        }

        public async Task<bool> UpdateDocument(int userID, DocumentUpdateDTO entity)
        {
            // 是否能保存：1、是否参加这个项目，文档是否存在
            Documents document = await SugarContext.Context.Queryable<Documents>()
                .Where(document => document.DocumentID == entity.DocumentID && document.DeleteStatus == DeleteStatuses.UnDelete)
                .InnerJoin<Projects>((document, project) => document.ProjectID == project.ProjectID && project.DeleteStatus == DeleteStatuses.UnDelete)
                .InnerJoin<ProjectUsers>((document, project, projectUser) => project.ProjectID == projectUser.ProjectID && projectUser.UserID == userID)
                .Select((document, project, projectUser) => document)
                .FirstAsync();
            if (document == null)
            {
                throw new Exception("document has been deleted");
            }

            // 添加历史的前提是两次变更的内容对比不一样
            if (entity.Content == document.Content)
            {
                return true;
            }
            await SugarContext.Context.Insertable(new HistoryDocuments()
            {
                Content = document.Content,
                CreateTime = DateTime.Now,
                CreatorID = userID,
                DocumentID = document.DocumentID,
            }).ExecuteCommandAsync();
            await SugarContext.Context.Updateable(document)
                .SetColumns(r => new Documents()
                {
                    UpdateTime = DateTime.Now,
                    UpdatorID = userID,
                    Content = entity.Content,
                })
                .Where(r => r.DocumentID == entity.DocumentID && r.DeleteStatus == DeleteStatuses.UnDelete)
                .ExecuteCommandAsync();
            return true;
        }
    }
}
