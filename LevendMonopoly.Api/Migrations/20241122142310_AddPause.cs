using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LevendMonopoly.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPause : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9f2b1d74-ee9d-4b44-b6ec-aee6b2be9e20"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("a1c6ac87-e2dc-4a2c-9b53-8765d1472c02"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("245cc39a-5db6-497b-a558-57ce981ddf7c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d5750175-ba10-4fe6-b1d3-ce4bcaf2ebd2"));

            migrationBuilder.AddColumn<bool>(
                name: "Paused",
                table: "GameSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "GameSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Paused",
                value: false);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3e964950-eb32-443a-afd8-e76124bfe616"), "Admin" },
                    { new Guid("f07eff9a-b81a-4ea3-9da2-506f447889fa"), "Moderator" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Balance", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { new Guid("60d53f10-f2c4-464d-b02c-b0ce98f35d07"), 5000, "RensTest", "VMLjnpipPgD04M60eJB5W1TJ1nG7bOSmfnUlZ8R6+FQ=", "WUN41CnApyWW8eQpuIZqqg==" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt", "RoleId" },
                values: new object[] { new Guid("08ca0924-39f9-4d65-809a-1ddafd95a761"), "Rens", "K2NT0vUtwqoakiQbJOTupoM6OV5yxVW5wa5s+jRa6Rw=", "mpvVxaYCDkpW4oMsuSqs8w==", new Guid("3e964950-eb32-443a-afd8-e76124bfe616") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f07eff9a-b81a-4ea3-9da2-506f447889fa"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("60d53f10-f2c4-464d-b02c-b0ce98f35d07"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("08ca0924-39f9-4d65-809a-1ddafd95a761"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3e964950-eb32-443a-afd8-e76124bfe616"));

            migrationBuilder.DropColumn(
                name: "Paused",
                table: "GameSettings");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("9f2b1d74-ee9d-4b44-b6ec-aee6b2be9e20"), "Moderator" },
                    { new Guid("d5750175-ba10-4fe6-b1d3-ce4bcaf2ebd2"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Balance", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { new Guid("a1c6ac87-e2dc-4a2c-9b53-8765d1472c02"), 5000, "RensTest", "BHBba1WjE+OKtYDOnRtf0CpQcsyBcOOPwmgh/m2M1ds=", "lyqtaT3wMPDd04obK+NIpQ==" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt", "RoleId" },
                values: new object[] { new Guid("245cc39a-5db6-497b-a558-57ce981ddf7c"), "Rens", "n2uFrCPc6kUwadvX0m2C0KoI4NJLm04RyBZ/P4BKzMM=", "J23Fos4xEKv1/4ePRMRS3Q==", new Guid("d5750175-ba10-4fe6-b1d3-ce4bcaf2ebd2") });
        }
    }
}
