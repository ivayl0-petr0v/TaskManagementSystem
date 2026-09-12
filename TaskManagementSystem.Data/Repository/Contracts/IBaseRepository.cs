namespace TaskManagementSystem.Data.Repository.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> All();

        IQueryable<T> AllAsNoTracking();

        Task<T?> GetByIdAsync(object id);

        Task AddAsync(T entity);

        void Update(T entity);

        void Remove(T entity);
        }
}
