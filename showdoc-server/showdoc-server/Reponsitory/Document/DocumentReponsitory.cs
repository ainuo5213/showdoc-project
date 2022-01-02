using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Context;
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
    }
}
