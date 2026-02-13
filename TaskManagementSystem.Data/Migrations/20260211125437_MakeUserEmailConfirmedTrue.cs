using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeUserEmailConfirmedTrue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "873783dd-beaa-46ae-8b75-701e303c9d25", true, "AQAAAAIAAYagAAAAEMkC7OTn33w/jFdGuN6C370fGwn2c83BhOK/TeGsbFKsaxbEo/e5OJZlEGGW++zx3w==", "6d801d62-4cf1-4166-a5ad-a37dd0332414" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ec16974-7f58-4a66-a03e-164f0c1151bc", true, "AQAAAAIAAYagAAAAEMvuSVGd5hZj9HGMjU/BAr2qxGxvgB8uhMR9IWdfC61APk8xf89NXsHH+sDdgwf0yQ==", "b077c76b-95f2-417c-acf5-1a2e5610f274" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "DueDateTime",
                value: new DateTime(2026, 2, 18, 12, 54, 37, 117, DateTimeKind.Utc).AddTicks(8592));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                column: "DueDateTime",
                value: new DateTime(2026, 2, 25, 12, 54, 37, 117, DateTimeKind.Utc).AddTicks(8694));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                column: "DueDateTime",
                value: new DateTime(2026, 2, 21, 12, 54, 37, 117, DateTimeKind.Utc).AddTicks(8696));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4534d749-0435-44ac-a6a9-96d97ae7fe7a", false, "AQAAAAIAAYagAAAAEC7b7iXxwhHmnqNL8DrDyxCnCs8ydeMKI0VLHNZAvJJejQT5M7kyLx9P/RfKQUU/ug==", "7376858b-fdfb-43de-9863-801b32ccf8f4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a547031-0c4a-4568-b33a-c5798c6e7a82", false, "AQAAAAIAAYagAAAAELI1tsKcGcUFgIbmvdKc9oMt1I7dR8URN14fEPNv9LneCXWdv5XcUTtBw07ly3vDBg==", "32e52ecd-6e6b-45ab-88f0-f4a679966384" });

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
    }
}
