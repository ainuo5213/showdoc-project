using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Project
{
    public class ProjectListItemDTO
    {
        public int UserID { get; set; }
        public int ParentId { get; set; }
        public ProjectListItemTypes Type { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public int ObjectID { get; set; }
        public DateTime SortTime { get; set; }
    }
}
