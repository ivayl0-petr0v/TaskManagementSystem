using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "680d3bd5-2da8-4bb4-b0bc-47ac33f8c096", "AQAAAAIAAYagAAAAEAWLQZtNE/vHhU/9sehwmpSu+GtlK0giCAKuatxtnwMPg5R7iQ6jV9jEXr91qnaHfA==", "1c8c5b1c-ee27-4117-ad57-46e7ecdc6ccf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d7e10e6-5dc1-40aa-b3dd-0598c49cb4e8", "AQAAAAIAAYagAAAAEBfcOCrJhV7ZxbkXgADRqZJIWGJnGdyn+SPM3Tz9ayY5YLdC1w2gWa2cXWVdpMkq4w==", "dd528dfe-b751-4c1d-a5c4-1fb14f0e3ea8" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CategoryId", "Description", "DueDateTime", "StatusId", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, 2, "Designing the UI components and integrating third-party APIs for calorie tracking and workout logging.", new DateTime(2026, 2, 18, 8, 20, 0, 580, DateTimeKind.Utc).AddTicks(5963), 1, "Fitness Mobile App Development", "dea12856-c198-4129-b3f3-b893d8395082" },
                    { 2, 3, "Completing the final capstone project on image recognition and attending the peer-review session.", new DateTime(2026, 2, 25, 8, 20, 0, 580, DateTimeKind.Utc).AddTicks(5973), 2, "AI & Machine Learning Certification", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" },
                    { 3, 1, "Revamping the homepage and product pages to enhance user experience and increase conversion rates.", new DateTime(2026, 2, 21, 8, 20, 0, 580, DateTimeKind.Utc).AddTicks(5975), 3, "E-commerce Website Redesign", "dea12856-c198-4129-b3f3-b893d8395082" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae3e21ed-7ed0-4855-b37b-888ce83825a6", "AQAAAAIAAYagAAAAEMiRiHF2pStSpiCL5qtmeKRJ7erfdk52yaMXXPZBX2FM4fx/EZc0jSVOTsPVkMNoTw==", "75d3d362-b636-49c9-8099-7a24a3c86e70" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8c8bc7cb-d668-4f71-8961-2a685c03397f", "AQAAAAIAAYagAAAAEILjen+hjJyU/dKCopkQl0iizPZqGq84SsbvrcYZi1d14qmUWQIinLDw/eNAC8VIwQ==", "bc3302d7-71c3-48e3-b85d-4b53f1044d80" });
        }
    }
}
