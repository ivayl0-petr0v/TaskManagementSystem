using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeNormalizedUserNameAsEmailAndCorrectPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4534d749-0435-44ac-a6a9-96d97ae7fe7a", "CRUZER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEC7b7iXxwhHmnqNL8DrDyxCnCs8ydeMKI0VLHNZAvJJejQT5M7kyLx9P/RfKQUU/ug==", "7376858b-fdfb-43de-9863-801b32ccf8f4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a547031-0c4a-4568-b33a-c5798c6e7a82", "CREATIVO@EXAMLE.COM", "AQAAAAIAAYagAAAAELI1tsKcGcUFgIbmvdKc9oMt1I7dR8URN14fEPNv9LneCXWdv5XcUTtBw07ly3vDBg==", "32e52ecd-6e6b-45ab-88f0-f4a679966384" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "DueDateTime",
                value: new DateTime(2026, 2, 18, 12, 42, 35, 76, DateTimeKind.Utc).AddTicks(8649));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                column: "DueDateTime",
                value: new DateTime(2026, 2, 25, 12, 42, 35, 76, DateTimeKind.Utc).AddTicks(8660));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                column: "DueDateTime",
                value: new DateTime(2026, 2, 21, 12, 42, 35, 76, DateTimeKind.Utc).AddTicks(8662));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "765f369c-164c-4280-b118-d54b9c565c7d", "CRUZER", "AQAAAAIAAYagAAAAEODt8eMkg4QIrPA0CWsqKFUnQxmDxZEuIoTbcrP75FpYKoFiMtZnd6BS4jwon9VVVQ==", "ca603683-91bc-45de-bf19-2b4d3aa0e1ad" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a92b4c0-f9b9-4daa-b8f8-1847d79fda5e", "CREATIVO", "AQAAAAIAAYagAAAAEAHdBEAKX99SE07BLK7h3WOhl9nB8ikl1m7G4FnEftH0K7e3K5YVRUewtMzMaa0FdQ==", "7922c276-99b2-4ee2-9495-9d9f1cb8b641" });

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
    }
}
