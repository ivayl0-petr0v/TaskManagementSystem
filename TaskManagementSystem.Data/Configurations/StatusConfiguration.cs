using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Data.Models;

namespace TaskManagementSystem.Data.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasData(
                new Status { Id = 1, Name = "Pending" },
                new Status { Id = 2, Name = "In Progress" },
                new Status { Id = 3, Name = "Completed" }
                );
        }
    }
}
