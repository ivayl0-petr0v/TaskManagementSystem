using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data.Repository.Contracts;
using TaskManagementSystem.Web.Data;

namespace TaskManagementSystem.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        private readonly TaskManagementDbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public BaseRepository(TaskManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> All()
            => dbSet;

        public IQueryable<TEntity> AllAsNoTracking()
            => dbSet.AsNoTracking();

        public async Task<TEntity?> GetByIdAsync(object id)
            => await dbSet.FindAsync(id);

        public async Task AddAsync(TEntity entity)
            => await dbSet.AddAsync(entity);

        public void Update(TEntity entity)
            => dbSet.Update(entity);

        public void Remove(TEntity entity)
            => dbSet.Remove(entity);
    }
}
