using TaskManagementSystem.ViewModels.Project;

namespace TaskManagementSystem.Services.Core.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectAllViewModel>> GetAllProjectsAsync();
        Task<ProjectDetailsViewModel?> GetProjectDetailsByIdAsync(int id, string currentUserId);
        Task<ProjectInputModel> GetProjectForCreateAsync();
        Task<int> CreateProjectAsync(ProjectInputModel inputModel, string currentUserId);
        Task<ProjectEditInputModel?> GetProjectForEditAsync(int id, string currentUserId);
        Task EditProjectAsync(int id, ProjectEditInputModel inputModel, string currentUserId);
        Task<ProjectDeleteViewModel?> GetProjectForDeleteAsync(int id, string currentUserId);
        Task DeleteProjectAsync(int id, string currentUserId);
        Task CompleteProjectAsync(int id, string currentUserId);
    }
}
