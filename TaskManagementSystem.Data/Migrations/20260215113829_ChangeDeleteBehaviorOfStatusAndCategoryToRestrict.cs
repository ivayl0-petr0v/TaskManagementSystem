using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeleteBehaviorOfStatusAndCategoryToRestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Categories_CategoryId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Statuses_StatusId",
                table: "Projects");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "82b07b2b-3654-478b-9d3f-ae4cc4260d5b", "AQAAAAIAAYagAAAAEHHKLHwmxdOlPDTv8jrxa06yh3lL/+I2bPmeD5HU1VB2mQSX3oRJ31/SAuB4SNsWoQ==", "e9cb3d4c-7600-4ad4-8512-d64b0cf876e5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d76d8ea1-4573-4993-a198-caee797de0eb", "AQAAAAIAAYagAAAAEF8yu+hUiGWE8siKRW3KcZwjdaZZDEVeSs7v6htb3VPqdeX/t8LfJ5oGwaHsMr8Pog==", "04d5091c-37c6-4a21-b935-7ea30d706a5e" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "DueDateTime",
                value: new DateTime(2026, 2, 22, 11, 38, 27, 224, DateTimeKind.Utc).AddTicks(433));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                column: "DueDateTime",
                value: new DateTime(2026, 3, 1, 11, 38, 27, 224, DateTimeKind.Utc).AddTicks(443));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                column: "DueDateTime",
                value: new DateTime(2026, 2, 25, 11, 38, 27, 224, DateTimeKind.Utc).AddTicks(444));

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Categories_CategoryId",
                table: "Projects",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Statuses_StatusId",
                table: "Projects",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Categories_CategoryId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Statuses_StatusId",
                table: "Projects");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "873783dd-beaa-46ae-8b75-701e303c9d25", "AQAAAAIAAYagAAAAEMkC7OTn33w/jFdGuN6C370fGwn2c83BhOK/TeGsbFKsaxbEo/e5OJZlEGGW++zx3w==", "6d801d62-4cf1-4166-a5ad-a37dd0332414" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ec16974-7f58-4a66-a03e-164f0c1151bc", "AQAAAAIAAYagAAAAEMvuSVGd5hZj9HGMjU/BAr2qxGxvgB8uhMR9IWdfC61APk8xf89NXsHH+sDdgwf0yQ==", "b077c76b-95f2-417c-acf5-1a2e5610f274" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Categories_CategoryId",
                table: "Projects",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Statuses_StatusId",
                table: "Projects",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
