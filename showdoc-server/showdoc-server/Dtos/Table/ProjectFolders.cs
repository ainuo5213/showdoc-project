using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace showdoc_server.Dtos.Table
{
    public class ProjectFolders
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ProjectFolderID { get; set; }
        public int UserID { get; set; }
        public int FolderID { get; set; }
        public int ProjectID { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
