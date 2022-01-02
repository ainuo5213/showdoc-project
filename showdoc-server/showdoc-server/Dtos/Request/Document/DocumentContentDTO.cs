using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Document
{
    public class DocumentContentDTO
    {
        public int DocumentID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public string ProjectName { get; set; }
        public int ProjectID { get; set; }
        public int FolderID { get; set; }
        public string FolderName { get; set; }
    }
}
