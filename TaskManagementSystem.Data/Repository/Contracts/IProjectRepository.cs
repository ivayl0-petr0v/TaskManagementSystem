using TaskManagementSystem.Data.Models;

namespace TaskManagementSystem.Data.Repository.Contracts
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Task<Project?> GetByIdWithDetailsAsync(int id);

        IQueryable<Project> GetAllForListAsNoTracking();

        Task<Project?> GetByIdAsNoTrackingAsync(int id);

        Task<Project?> GetByIdWithStatusAsync(int id);
    }
}
