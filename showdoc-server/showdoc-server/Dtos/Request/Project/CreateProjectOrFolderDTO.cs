using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Project
{
    public class CreateProjectOrFolderDTO
    {
        public int FolderID { get; set; }
        public ProjectListItemTypes Type { get; set; }

        [Required(ErrorMessage = "folder name or project name needed")]
        [MinLength(1, ErrorMessage = "folder name or project name must be 1 char at least")]
        [MaxLength(18, ErrorMessage = "folder name or project name must be 18 char at most")]
        public string Name { get; set; }
    }
}
