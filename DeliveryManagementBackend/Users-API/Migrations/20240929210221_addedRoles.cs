using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Users_API.Migrations
{
    /// <inheritdoc />
    public partial class addedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "29c46726-44e8-4bb0-a0c8-d86b71af7aca", "2", "User", "User" },
                    { "c70752c9-d820-4432-bf5a-8868e505bfbe", "1", "Admin", "Admin" },
                    { "cd9593f3-9fd7-4d9a-b9ab-80127355413d", "3", "HR", "HR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29c46726-44e8-4bb0-a0c8-d86b71af7aca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c70752c9-d820-4432-bf5a-8868e505bfbe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd9593f3-9fd7-4d9a-b9ab-80127355413d");
        }
    }
}
