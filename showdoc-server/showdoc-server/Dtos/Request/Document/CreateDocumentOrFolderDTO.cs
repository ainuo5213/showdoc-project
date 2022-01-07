using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Document
{
    public class CreateDocumentOrFolderDTO
    {
        public int FolderID { get; set; }
        public string Title { get; set; }
        public DocumentObjectTypes Type { get; set; }
        public int ProjectID { get; set; }
        public string Content { get; set; }
    }
}
