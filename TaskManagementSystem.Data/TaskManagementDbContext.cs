using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data.Models;

namespace TaskManagementSystem.Web.Data
{
    public class TaskManagementDbContext : IdentityDbContext
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagementDbContext).Assembly);
        }
    }
}
