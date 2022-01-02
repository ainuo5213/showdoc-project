using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace showdoc_server.Dtos.Table
{
    public class HistoryDocuments
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int HistoryDocumentID { get; set; }
        public int DocumentID { get; set; }
        public string Content { get; set; }
        public int CreatorID { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
