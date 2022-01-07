using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Document
{
    public class FolderItemDTO
    {
        public string Name { get; set; }
        public int ParentID { get; set; }
        public int FolderID { get; set; }
    }
}
