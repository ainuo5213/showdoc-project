using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
