using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Reponsitory.Document
{
    public interface IDocumentReponsitory
    {
        Task<int> DeleteDocument(int userID, int documentID);
        Task<int> DeleteFolder(int userID, int folderID);
    }
}
