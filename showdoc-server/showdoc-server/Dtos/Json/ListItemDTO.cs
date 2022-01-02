using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Json
{
    public class ListItemDTO<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
