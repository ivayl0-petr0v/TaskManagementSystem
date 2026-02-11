using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data.Configurations;
using TaskManagementSystem.Data.Models;
using TaskManagementSystem.Web.Data.Migrations;

namespace TaskManagementSystem.Web.Data
{
    public class TaskManagementDbContext : IdentityDbContext<ApplicationUser>
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

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        }
    }
}
