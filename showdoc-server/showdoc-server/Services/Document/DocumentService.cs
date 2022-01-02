using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Json;
using showdoc_server.Dtos.Request.Document;
using showdoc_server.Reponsitory.Document;

namespace showdoc_server.Services.Document
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentReponsitory documentReponsitory;

        public DocumentService(IDocumentReponsitory documentReponsitory)
        {
            this.documentReponsitory = documentReponsitory;
        }

        public async Task<DocumentContentDTO> CreateDocumentOrFolder(int userID, CreateDocumentOrFolderDTO entity)
        {
            if (entity.Type == DocumentObjectTypes.Document)
            {
                return await this.documentReponsitory.CreateDocument(userID, entity.ProjectID, entity.FolderID, entity.Title);
            }
            else
            {
                return await this.documentReponsitory.CreateFolder(userID, entity.ProjectID, entity.FolderID, entity.Title);
            }
        }

        public async Task<bool> DeleteFolderOrDocument(int userID, DeleteFolderOrDocumentDTO entity)
        {
            if (entity.ObjectID == 0)
            {
                return false;
            }
            if (entity.Type == DocumentObjectTypes.Document)
            {
                return await this.documentReponsitory.DeleteDocument(userID, entity.ObjectID) > 0;
            }
            else
            {
                return await this.documentReponsitory.DeleteFolder(userID, entity.ObjectID) > 0;
            }
        }

        public async Task<DocumentContentDTO> GetDocumentContent(int userID, int documentID)
        {
            return await this.documentReponsitory.GetDocumentContent(userID, documentID);
        }

        public async Task<ListItemDTO<DocumentHistoryDTO>> GetDocumentHistory(int userID, int documentID, int page)
        {
            return await this.documentReponsitory.GetDocumentHistory(userID, documentID, page);
        }

        public async Task<HistoryComparisonDTO> HistoryDocumentComparison(int userID, int historyID)
        {
            return await this.documentReponsitory.HistoryDocumentComparison(userID, historyID);
        }

        public async Task<bool> RollbackDocument(int userID, int historyID)
        {
            return await this.documentReponsitory.RollbackDocument(userID, historyID) > 0;
        }

        public async Task<bool> UpdateDocument(int userID, DocumentUpdateDTO entity)
        {
            return await this.documentReponsitory.UpdateDocument(userID, entity);
        }
    }
}
