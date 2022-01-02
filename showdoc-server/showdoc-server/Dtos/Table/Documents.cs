using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Json;
using SqlSugar;

namespace showdoc_server.Dtos.Table
{
    public class Documents
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int DocumentID { get; set; }
        public int ProjectID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int FolderID { get; set; }
        public int CreatorID { get; set; }
        public int UpdatorID { get; set; }
        public DeleteStatuses DeleteStatus { get; set; } = DeleteStatuses.UnDelete;
        public DateTime SortTime { get; set; } = DateTime.Now;
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
