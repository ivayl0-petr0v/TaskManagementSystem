using TaskManagementSystem.Data.Models;

namespace TaskManagementSystem.Data.Repository.Contracts
{
    public interface IStatusRepository : IBaseRepository<Status>
    {
        Task<Status?> GetByStatusNameAsync();

        IQueryable<Status> GetAllStatusesAsNoTracking();

        Task<bool> ExistsStatusByIdAsync(int id);
    }
}
