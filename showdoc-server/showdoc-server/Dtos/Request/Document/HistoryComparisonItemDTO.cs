using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Document
{
    public class HistoryComparisonItemDTO
    {
        public int HistoryID { get; set; }
        public int DocumentID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreatorID { get; set; }
        public string Creator { get; set; }
    }
}
