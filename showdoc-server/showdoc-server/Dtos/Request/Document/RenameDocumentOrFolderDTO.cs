using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Document
{
    public class RenameDocumentOrFolderDTO
    {
        public int ObjectID { get; set; }
        public string Name { get; set; }
        public DocumentObjectTypes Type { get; set; }
    }
}
