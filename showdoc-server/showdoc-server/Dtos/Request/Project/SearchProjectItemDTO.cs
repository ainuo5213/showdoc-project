using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Project
{
    public class SearchProjectItemDTO
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string Creator { get; set; }
    }
}
