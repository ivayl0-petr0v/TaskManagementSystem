using TaskManagementSystem.Services.Core.Contracts.Project;

namespace TaskManagementSystem.Services.Core.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectListItemDto>> GetAllProjectsAsync();
        Task<ProjectDetailsDto?> GetProjectDetailsByIdAsync(int id, string currentUserId);
        Task<ProjectCreateDataDto> GetProjectCreateDataAsync();
        Task<int> CreateProjectAsync(CreateProjectRequest request, string currentUserId);
        Task<UpdateProjectRequest?> GetProjectForEditAsync(int id, string currentUserId);
        Task EditProjectAsync(int id, UpdateProjectRequest inputModel, string currentUserId);
        Task<ProjectDeleteDto?> GetProjectForDeleteAsync(int id, string currentUserId);
        Task DeleteProjectAsync(int id, string currentUserId);
        Task CompleteProjectAsync(int id, string currentUserId);
    }
}
