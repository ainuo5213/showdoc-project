using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Json;
using SqlSugar;

namespace showdoc_server.Dtos.Table
{
    public class ProjectUsers
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ProjectUserID { get; set; }
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime SortTime { get; set; } = DateTime.Now;
    }
}
