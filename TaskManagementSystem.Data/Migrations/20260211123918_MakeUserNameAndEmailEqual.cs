using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeUserNameAndEmailEqual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "765f369c-164c-4280-b118-d54b9c565c7d", "AQAAAAIAAYagAAAAEODt8eMkg4QIrPA0CWsqKFUnQxmDxZEuIoTbcrP75FpYKoFiMtZnd6BS4jwon9VVVQ==", "ca603683-91bc-45de-bf19-2b4d3aa0e1ad", "cruzer@example.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "5a92b4c0-f9b9-4daa-b8f8-1847d79fda5e", "AQAAAAIAAYagAAAAEAHdBEAKX99SE07BLK7h3WOhl9nB8ikl1m7G4FnEftH0K7e3K5YVRUewtMzMaa0FdQ==", "7922c276-99b2-4ee2-9495-9d9f1cb8b641", "creativo@examle.com" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "DueDateTime",
                value: new DateTime(2026, 2, 18, 12, 39, 18, 45, DateTimeKind.Utc).AddTicks(4053));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                column: "DueDateTime",
                value: new DateTime(2026, 2, 25, 12, 39, 18, 45, DateTimeKind.Utc).AddTicks(4259));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                column: "DueDateTime",
                value: new DateTime(2026, 2, 21, 12, 39, 18, 45, DateTimeKind.Utc).AddTicks(4261));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "680d3bd5-2da8-4bb4-b0bc-47ac33f8c096", "AQAAAAIAAYagAAAAEAWLQZtNE/vHhU/9sehwmpSu+GtlK0giCAKuatxtnwMPg5R7iQ6jV9jEXr91qnaHfA==", "1c8c5b1c-ee27-4117-ad57-46e7ecdc6ccf", "cruzer" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "4d7e10e6-5dc1-40aa-b3dd-0598c49cb4e8", "AQAAAAIAAYagAAAAEBfcOCrJhV7ZxbkXgADRqZJIWGJnGdyn+SPM3Tz9ayY5YLdC1w2gWa2cXWVdpMkq4w==", "dd528dfe-b751-4c1d-a5c4-1fb14f0e3ea8", "creativo" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "DueDateTime",
                value: new DateTime(2026, 2, 18, 8, 20, 0, 580, DateTimeKind.Utc).AddTicks(5963));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                column: "DueDateTime",
                value: new DateTime(2026, 2, 25, 8, 20, 0, 580, DateTimeKind.Utc).AddTicks(5973));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                column: "DueDateTime",
                value: new DateTime(2026, 2, 21, 8, 20, 0, 580, DateTimeKind.Utc).AddTicks(5975));
        }
    }
}
