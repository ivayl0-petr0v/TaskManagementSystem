using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseProjectDescriptionLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae293fcf-43e4-47fe-9352-507520164e76", "AQAAAAIAAYagAAAAEP3hgu6EcBV5TjXbR/1rrvpJu5962tlAWdC4jh5Dd+2qtvzQgnqhiRosAIbx0iI8ZQ==", "2ab12e96-d0cc-409c-9447-c27c86629e2e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c9cac3a9-0b61-4435-a9f4-9f59c363847c", "AQAAAAIAAYagAAAAEDAzv+JNHmUJlziHuJ01zrxRgZu9miic7va0CLoWdAqU0bMesOAaoShbElXtSzUvfA==", "e87b0449-4e4a-4183-b5ec-b74a9f4e85cf" });
        }
    }
}
