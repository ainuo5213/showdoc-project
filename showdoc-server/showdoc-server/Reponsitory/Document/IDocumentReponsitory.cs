using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Json;
using showdoc_server.Dtos.Request.Document;

namespace showdoc_server.Reponsitory.Document
{
    public interface IDocumentReponsitory
    {
        Task<int> DeleteDocument(int userID, int documentID);
        Task<int> DeleteFolder(int userID, int folderID);
        Task<DocumentContentDTO> GetDocumentContent(int userID, int documentID);
        Task<DocumentContentDTO> CreateDocument(int userID, int projectID, int objectID, string title);
        Task<DocumentContentDTO> CreateFolder(int userID, int projectID, int objectID, string title);
        Task<ListItemDTO<DocumentHistoryDTO>> GetDocumentHistory(int userID, int documentID, int page);
        Task<bool> UpdateDocument(int userID, DocumentUpdateDTO entity);
        Task<HistoryComparisonDTO> HistoryDocumentComparison(int userID, int historyID);
        Task<int> RollbackDocument(int userID, int historyID);
        Task<int> RenameFolder(int userID, int objectID, string name);
        Task<int> RenameDocument(int userID, int objectID, string name);
        Task<int> MoveDocument(int userID, int objectID, int folderID);
        Task<int> MoveFolder(int userID, int objectID, int folderID);
        Task<IEnumerable<ProjectMenuItemDTO>> GetProjectMenu(int userID, int projectID);
    }
}
