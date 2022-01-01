using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Request.Project;
using showdoc_server.Dtos.Table;

namespace showdoc_server.Reponsitory.Project
{
    public interface IProjectReponsitory
    {
        Task<IEnumerable<ProjectListItemDTO>> GetFolderContentAsync(int userID, int folderID);
        Task<ProjectListItemDTO> CreateProjectAsync(int v, Projects projects);
        Task<ProjectListItemDTO> CreateFolderAsync(int v, Folders folders);
        Task<int> DeleteFolderAsync(int v, DeleteProjectOrFolderDTO entity);
        Task<int> DeleteProjectAsync(int v, DeleteProjectOrFolderDTO entity);
        Task<int> MoveProjectAsync(int v, MoveProjectOrFolderDTO entity);
        Task<int> MoveFolderAsync(int v, MoveProjectOrFolderDTO entity);
        Task<int> RenameFolderAsync(int v, RenameProjectOrFolderDTO entity);
        Task<int> RenameProjectAsync(int v, RenameProjectOrFolderDTO entity);
        Task<IEnumerable<SearchProjectItemDTO>> SearchProjectByKeyAsync(int v, string key);
    }
}
