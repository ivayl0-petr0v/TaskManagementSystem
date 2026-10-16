using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data.Models;
using TaskManagementSystem.Data.Repository.Contracts;
using TaskManagementSystem.Web.Data;

namespace TaskManagementSystem.Data.Repository
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        public StatusRepository(TaskManagementDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Status?> GetByStatusNameAsync()
        {
            return await All().FirstOrDefaultAsync(s => s.Name == "Completed");
        }

        public IQueryable<Status> GetAllStatusesAsNoTracking()
        {
            return AllAsNoTracking();
        }

        public Task<bool> ExistsStatusByIdAsync(int id)
        {
            return AllAsNoTracking().AnyAsync(s => s.Id == id);
        }
    }
}
