using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data.Repository.Contracts;
using TaskManagementSystem.Web.Data;

namespace TaskManagementSystem.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly TaskManagementDbContext dbContext;
        private readonly DbSet<T> dbSet;

        public BaseRepository(TaskManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        public IQueryable<T> All()
            => dbSet;

        public IQueryable<T> AllAsNoTracking()
            => dbSet.AsNoTracking();

        public async Task<T?> GetByIdAsync(object id)
            => await dbSet.FindAsync(id);

        public async Task AddAsync(T entity)
            => await dbSet.AddAsync(entity);

        public void Update(T entity)
            => dbSet.Update(entity);

        public void Remove(T entity)
            => dbSet.Remove(entity);
    }
}
