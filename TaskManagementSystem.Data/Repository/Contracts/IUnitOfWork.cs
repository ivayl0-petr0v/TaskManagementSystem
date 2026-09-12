namespace TaskManagementSystem.Data.Repository.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
