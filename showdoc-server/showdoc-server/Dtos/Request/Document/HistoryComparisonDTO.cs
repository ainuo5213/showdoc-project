using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Document
{
    public class HistoryComparisonDTO
    {
        public HistoryComparisonItemDTO CurrentVersion { get; set; }
        public HistoryComparisonItemDTO HistoryVersion { get; set; }
    }
}
