using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeleteMethodOfStatusAndCategoryToRestrict : Migration
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
                values: new object[] { "8e460ed7-5042-4aa1-9891-514478a20ab4", "AQAAAAIAAYagAAAAEE476DghadL2/bWjR8PtXrsFgOZno/qZ2UUHjJgywGckeTniHyodkaWQNqovQ+iLtg==", "ea6ee97e-fcf6-4b55-93ce-2b9deee1ca08" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f698d357-1fad-49da-9e7e-424d53c57d6f", "AQAAAAIAAYagAAAAEBj3/dNKVuBaSYNujY/ZYbxdEueWWHXtCeF98B3CtxVcmNZh97zl6PktS4Fmc4Mi+A==", "f377b664-bd9d-4846-aab2-7ab95671ba45" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "DueDateTime",
                value: new DateTime(2026, 2, 22, 13, 4, 1, 905, DateTimeKind.Utc).AddTicks(1096));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                column: "DueDateTime",
                value: new DateTime(2026, 3, 1, 13, 4, 1, 905, DateTimeKind.Utc).AddTicks(1105));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                column: "DueDateTime",
                value: new DateTime(2026, 2, 25, 13, 4, 1, 905, DateTimeKind.Utc).AddTicks(1107));

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
