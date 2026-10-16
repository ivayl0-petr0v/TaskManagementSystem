using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data.Models;
using TaskManagementSystem.Data.Repository.Contracts;
using TaskManagementSystem.Web.Data;

namespace TaskManagementSystem.Data.Repository
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(TaskManagementDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Project?> GetByIdWithDetailsAsync(int id)
        {
            return await AllAsNoTracking()
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Status)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public IQueryable<Project> GetAllForListAsNoTracking()
        {
            return AllAsNoTracking()
                .OrderBy(p => p.Title)
                .ThenBy(p => p.DueDateTime);
        }

        public async Task<Project?> GetByIdAsNoTrackingAsync(int id)
        {
            return await AllAsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project?> GetByIdWithStatusAsync(int id)
        {
            return await All()
                .Include(p => p.Status)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
