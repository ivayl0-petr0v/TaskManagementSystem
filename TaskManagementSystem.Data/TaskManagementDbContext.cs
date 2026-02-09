using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data.Models;

namespace TaskManagementSystem.Web.Data
{
    public class TaskManagementDbContext : IdentityDbContext
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
