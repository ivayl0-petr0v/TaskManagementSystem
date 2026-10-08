using TaskManagementSystem.Data.Models;
using TaskManagementSystem.Data.Repository.Contracts;
using TaskManagementSystem.Web.Data;

namespace TaskManagementSystem.Data.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(TaskManagementDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
