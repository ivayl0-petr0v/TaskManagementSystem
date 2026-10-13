using TaskManagementSystem.Data.Models;

namespace TaskManagementSystem.Data.Repository.Contracts
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        IQueryable<Category> GetAllCategoriesAsNoTracking();

        Task<bool> ExistsCategoryByIdAsync(int id);
    }
}
