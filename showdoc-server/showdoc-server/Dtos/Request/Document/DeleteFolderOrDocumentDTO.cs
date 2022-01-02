using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Document
{
    public class DeleteFolderOrDocumentDTO
    {
        public int ObjectID { get; set; }
        public DocumentObjectTypes Type { get; set; }
    }
}
