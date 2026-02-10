using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Data.Models;

namespace TaskManagementSystem.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id = 1, Name = "Work" },
                new Category { Id = 2, Name = "Personal" },
                new Category { Id = 3, Name = "Study" }
                );
        }
    }
}
