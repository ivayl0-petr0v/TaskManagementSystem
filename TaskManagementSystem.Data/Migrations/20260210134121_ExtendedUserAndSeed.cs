using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUserAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 0, "ae293fcf-43e4-47fe-9352-507520164e76", "cruzer@example.com", false, "John", "Burrows", false, null, "CRUZER@EXAMPLE.COM", "CRUZER", "AQAAAAIAAYagAAAAEP3hgu6EcBV5TjXbR/1rrvpJu5962tlAWdC4jh5Dd+2qtvzQgnqhiRosAIbx0iI8ZQ==", null, false, "2ab12e96-d0cc-409c-9447-c27c86629e2e", false, "cruzer" },
                    { "dea12856-c198-4129-b3f3-b893d8395082", 0, "c9cac3a9-0b61-4435-a9f4-9f59c363847c", "creativo@examle.com", false, "James", "Smith", false, null, "CREATIVO@EXAMLE.COM", "CREATIVO", "AQAAAAIAAYagAAAAEDAzv+JNHmUJlziHuJ01zrxRgZu9miic7va0CLoWdAqU0bMesOAaoShbElXtSzUvfA==", null, false, "e87b0449-4e4a-4183-b5ec-b74a9f4e85cf", false, "creativo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
