using TaskManagementSystem.Data.Repository.Contracts;
using TaskManagementSystem.Web.Data;

namespace TaskManagementSystem.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskManagementDbContext dbContext;

        public UnitOfWork(TaskManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<int> SaveChangesAsync()
            => dbContext.SaveChangesAsync();
    }
}
