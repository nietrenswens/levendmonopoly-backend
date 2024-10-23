using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LevendMonopoly.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("63a88998-bfdd-4029-a9bf-42523686fda4"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("002caa4d-1ea4-4136-a8cb-84269716e2f0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ef371c31-ce79-4a70-8bb9-0e37fe822623"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("64549f4f-6b22-44fd-907c-ebaab9124472"));

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("21e5698d-cb59-4ae8-af7a-f9eec0f55ad9"), "Moderator" },
                    { new Guid("51a9e58a-64d6-4ab2-84c0-5ef4384e352b"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Balance", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { new Guid("1c6ca39b-7383-4119-abf6-70a4beab2910"), 5000, "RensTest", "nrhLQYIC2RG3lM3cYYRgEThoMs+1/NR4BtKGqJdhpZw=", "450mnyQgdS8lCe/4LIq85w==" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt", "RoleId" },
                values: new object[] { new Guid("9fe52e10-3bcf-47d6-ba53-8d1d6302c465"), "Rens", "hTP6zJIRDxZltrwgXyH+Cu6b45DCMjfV61sRFX+iLfQ=", "VsDdYVNo6oP5B9H7whWmzg==", new Guid("51a9e58a-64d6-4ab2-84c0-5ef4384e352b") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("21e5698d-cb59-4ae8-af7a-f9eec0f55ad9"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("1c6ca39b-7383-4119-abf6-70a4beab2910"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9fe52e10-3bcf-47d6-ba53-8d1d6302c465"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("51a9e58a-64d6-4ab2-84c0-5ef4384e352b"));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("63a88998-bfdd-4029-a9bf-42523686fda4"), "Moderator" },
                    { new Guid("64549f4f-6b22-44fd-907c-ebaab9124472"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Balance", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { new Guid("002caa4d-1ea4-4136-a8cb-84269716e2f0"), 5000, "RensTest", "kPgOBsAkc/693Jemw6Xgkt4NFEWvtzBLlfTjtF6bGnM=", "ynvZxSg7GmF/yrrn20kVWw==" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "PasswordSalt", "RoleId" },
                values: new object[] { new Guid("ef371c31-ce79-4a70-8bb9-0e37fe822623"), "mulderrens@outlook.com", "Admin", "I5PX3Ny7dukFsvmb+l9FjqD4Jap0JuPr5OMUu7C48y4=", "POvT5LE7vegG9BctMJsq4A==", new Guid("64549f4f-6b22-44fd-907c-ebaab9124472") });
        }
    }
}
