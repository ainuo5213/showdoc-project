using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Context;
using showdoc_server.Dtos.Request.Document;
using showdoc_server.Dtos.Table;

namespace showdoc_server.Reponsitory.Document
{
    public class DocumentReponsitory : IDocumentReponsitory
    {
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
    }
}
