using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Data.Models;

namespace TaskManagementSystem.Data.Configurations
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
                .HasOne(p => p.Status)
                .WithMany(s => s.Projects)
                .HasForeignKey(p => p.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Category)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Project
                {
                    Id = 1,
                    Title = "Fitness Mobile App Development",
                    Description = "Designing the UI components and integrating third-party APIs for calorie tracking and workout logging.",
                    DueDateTime = DateTime.UtcNow.AddDays(7),
                    StatusId = 1,
                    CategoryId = 2,
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082"
                },
                new Project
                {
                    Id = 2,
                    Title = "AI & Machine Learning Certification",
                    Description = "Completing the final capstone project on image recognition and attending the peer-review session.",
                    DueDateTime = DateTime.UtcNow.AddDays(14),
                    StatusId = 2,
                    CategoryId = 3,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"
                },
                new Project
                {
                    Id = 3,
                    Title = "E-commerce Website Redesign",
                    Description = "Revamping the homepage and product pages to enhance user experience and increase conversion rates.",
                    DueDateTime = DateTime.UtcNow.AddDays(10),
                    StatusId = 3,
                    CategoryId = 1,
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082"
                }
                );
        }
    }
}
