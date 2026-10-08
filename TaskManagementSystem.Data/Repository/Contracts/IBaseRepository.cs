namespace TaskManagementSystem.Data.Repository.Contracts
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> All();

        IQueryable<TEntity> AllAsNoTracking();

        Task<TEntity?> GetByIdAsync(object id);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);
    }
}
