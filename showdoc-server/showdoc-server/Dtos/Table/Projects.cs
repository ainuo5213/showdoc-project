using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Json;
using SqlSugar;

namespace showdoc_server.Dtos.Table
{
    public class Projects
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ProjectID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CreatorID { get; set; }
        public int FolderID { get; set; } = 0;
        public DeleteStatuses DeleteStatus { get; set; } = DeleteStatuses.UnDelete;
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
