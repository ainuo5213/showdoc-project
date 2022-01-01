using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Project
{
    public class RenameProjectOrFolderDTO
    {
        public string Name { get; set; }
        public ProjectListItemTypes Type { get; set; }
        public int ObjectID { get; set; }
    }
}
