using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Json;
using showdoc_server.Dtos.Request.Document;

namespace showdoc_server.Services.Document
{
    public interface IDocumentService
    {
        Task<bool> DeleteFolderOrDocument(int userID, DeleteFolderOrDocumentDTO entity);
        Task<DocumentContentDTO> GetDocumentContent(int userID, int documentID);
        Task<DocumentContentDTO> CreateDocumentOrFolder(int userID, CreateDocumentOrFolderDTO entity);
        Task<ListItemDTO<DocumentHistoryDTO>> GetDocumentHistory(int userID, int documentID, int page);
        Task<bool> UpdateDocument(int userID, DocumentUpdateDTO entity);
        Task<HistoryComparisonDTO> HistoryDocumentComparison(int userID, int historyID);
        Task<bool> RollbackDocument(int userID, int historyID);
        Task<bool> RenameDocumentOrFolder(int userID, RenameDocumentOrFolderDTO entity);
        Task<bool> MoveDocumentOrFolder(int userID, MoveDocumentOrFolderDTO entity);
    }
}
