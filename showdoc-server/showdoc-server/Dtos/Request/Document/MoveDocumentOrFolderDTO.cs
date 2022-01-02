using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Document
{
    public class MoveDocumentOrFolderDTO
    {
        public int ObjectID { get; set; }
        public int FolderID { get; set; }
        public DocumentObjectTypes Type { get; set; }
    }
}
