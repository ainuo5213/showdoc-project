using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Request.Project;

namespace showdoc_server.Services.Project
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectListItemDTO>> GetFolderContentAsync(int userID, int folderID);
        Task<ProjectListItemDTO> CreateFolderOrProjectAsync(int v, CreateProjectOrFolderDTO entity);
        Task<bool> DeleteFolderOrProjectAsync(int v, DeleteProjectOrFolderDTO entity);
        Task<bool> MoveFolderOrProjectAsync(int v, MoveProjectOrFolderDTO entity);
    }
}
