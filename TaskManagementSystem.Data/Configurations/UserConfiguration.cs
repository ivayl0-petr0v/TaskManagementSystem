using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Data.Models;

namespace TaskManagementSystem.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            PasswordHasher<ApplicationUser> passwordHasher = new();

            ApplicationUser creativoUser = new()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "creativo@examle.com",
                NormalizedUserName = "CREATIVO@EXAMLE.COM",
                FirstName = "James",
                LastName = "Smith",
                Email = "creativo@examle.com",
                NormalizedEmail = "CREATIVO@EXAMLE.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true
            };
            creativoUser.PasswordHash = passwordHasher.HashPassword(creativoUser, "Creativo123!");

            ApplicationUser cruzerUser = new()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "cruzer@example.com",
                NormalizedUserName = "CRUZER@EXAMPLE.COM",
                FirstName = "John",
                LastName = "Burrows",
                Email = "cruzer@example.com",
                NormalizedEmail = "CRUZER@EXAMPLE.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true
            };
            cruzerUser.PasswordHash = passwordHasher.HashPassword(cruzerUser, "Cruzer123!");

            builder.HasData(creativoUser, cruzerUser);
        }
    }
}