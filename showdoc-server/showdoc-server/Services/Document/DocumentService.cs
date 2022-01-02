using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
