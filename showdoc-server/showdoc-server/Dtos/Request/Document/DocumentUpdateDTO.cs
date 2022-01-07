﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Document
{
    public class DocumentUpdateDTO
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public int DocumentID { get; set; }
        public int FolderID { get; set; }
        public int ProjectID { get; set; }
    }
}
