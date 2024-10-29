using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LevendMonopoly.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c84356cb-fdc3-4dc8-9927-76cb38fca0f2"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("c87f091b-39c4-4ce4-8b46-94d89d656ca8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("09b6f8bc-f953-4160-982b-395c2de7ca82"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0e7253e6-9e5b-475c-b5fa-0b5f475383a5"));

            migrationBuilder.CreateTable(
                name: "GameSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaxRate = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSettings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "GameSettings",
                columns: new[] { "Id", "TaxRate" },
                values: new object[] { 1, 0.6m });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("689a58d6-d1de-484a-ab48-449776f53896"), "Admin" },
                    { new Guid("d5ea985c-196b-4c03-b874-ce5b86e3ed4d"), "Moderator" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Balance", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { new Guid("c3f9a3eb-1aed-41ac-8823-7308bbbba645"), 5000, "RensTest", "Qy4tc5eEQPrZMD6smKUrxBAUiLfmU7nagAzeGJcxlzA=", "h7XhhEdLZPZIbgafda/6Lg==" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt", "RoleId" },
                values: new object[] { new Guid("b9d9de64-5d2a-44b9-be34-115c40ddcec5"), "Rens", "VX0mLA4PR7m/xm3MIFiIaJUSRNXtoFgGi5gl6iuN1Z8=", "WZhTj6YxrudRZa1qKQa9oQ==", new Guid("689a58d6-d1de-484a-ab48-449776f53896") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameSettings");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d5ea985c-196b-4c03-b874-ce5b86e3ed4d"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("c3f9a3eb-1aed-41ac-8823-7308bbbba645"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b9d9de64-5d2a-44b9-be34-115c40ddcec5"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("689a58d6-d1de-484a-ab48-449776f53896"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0e7253e6-9e5b-475c-b5fa-0b5f475383a5"), "Admin" },
                    { new Guid("c84356cb-fdc3-4dc8-9927-76cb38fca0f2"), "Moderator" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Balance", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { new Guid("c87f091b-39c4-4ce4-8b46-94d89d656ca8"), 5000, "RensTest", "k9dDQ9xz7sU/oRZJT7awMNIHVCfkaLlFr4A1sB7H0dA=", "xvvz8G8N8tnjguHUu4wQhA==" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt", "RoleId" },
                values: new object[] { new Guid("09b6f8bc-f953-4160-982b-395c2de7ca82"), "Rens", "iQm1CjtTEb/lTF4GcwfcQWl38cxtAts6x6bKuESUb7k=", "A7mOhEPNlMZ036EkgFD0xg==", new Guid("0e7253e6-9e5b-475c-b5fa-0b5f475383a5") });
        }
    }
}
