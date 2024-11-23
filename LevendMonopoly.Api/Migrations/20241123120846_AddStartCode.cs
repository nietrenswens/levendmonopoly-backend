using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LevendMonopoly.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddStartCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "StartcodePull",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Startcode = table.Column<string>(type: "text", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartcodePull", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("532c33b2-6ba3-4433-b05d-38becf9196d2"), "Moderator" },
                    { new Guid("cf670bc6-7c2c-4623-a4d9-e9fba7301fbd"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Balance", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { new Guid("e6c4e92c-1315-4cb4-be5a-6063a347a757"), 5000, "RensTest", "E5DYIwmlJ7vpwjg7lEPGn13QoFMf3dxDtcX4rUvM8fY=", "U9V0DaBkWpahR1DVfLrzAw==" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt", "RoleId" },
                values: new object[] { new Guid("6b6b9357-aae9-466f-b65c-96e49cad0072"), "Rens", "WUaTfb77tlrAN56kOucZJsBUXgIwWXLTfS25aKp2SvI=", "Fx/IsjDpKoDUs7VcKf5xug==", new Guid("cf670bc6-7c2c-4623-a4d9-e9fba7301fbd") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StartcodePull");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("532c33b2-6ba3-4433-b05d-38becf9196d2"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("e6c4e92c-1315-4cb4-be5a-6063a347a757"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6b6b9357-aae9-466f-b65c-96e49cad0072"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cf670bc6-7c2c-4623-a4d9-e9fba7301fbd"));

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
    }
}
