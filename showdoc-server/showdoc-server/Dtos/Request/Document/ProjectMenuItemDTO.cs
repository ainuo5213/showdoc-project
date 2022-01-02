
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Document
{
    public class ProjectMenuItemDTO
    {
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public int ObjectID { get; set; }
        public int ParentID { get; set; }
        public DocumentObjectTypes Type { get; set; }
        public DateTime SortTime { get; internal set; }
    }
}
