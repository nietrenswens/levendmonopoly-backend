using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LevendMonopoly.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddImageToBuilding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Image",
                table: "Buildings",
                type: "text",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Buildings");

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
    }
}
