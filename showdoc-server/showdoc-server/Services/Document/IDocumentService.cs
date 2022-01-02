﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Request.Document;

namespace showdoc_server.Services.Document
{
    public interface IDocumentService
    {
        Task<bool> DeleteFolderOrDocument(int userID, DeleteFolderOrDocumentDTO entity);
        Task<DocumentContentDTO> GetDocumentContent(int userID, int documentID);
    }
}
