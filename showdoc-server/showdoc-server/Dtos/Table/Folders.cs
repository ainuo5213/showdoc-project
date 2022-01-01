using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Json;
using showdoc_server.Dtos.Request.Folder;
using SqlSugar;

namespace showdoc_server.Dtos.Table
{
    public class Folders
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int FolderID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ParentID { get; set; } = 0;
        public int CreatorID { get; set; }
        public int ProjectID { get; set; } = 0;
        public FolderTypes Type { get; set; }
        public DeleteStatuses DeleteStatus { get; set; } = DeleteStatuses.UnDelete;
        public DateTime SortTime { get; set; } = DateTime.Now;
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
