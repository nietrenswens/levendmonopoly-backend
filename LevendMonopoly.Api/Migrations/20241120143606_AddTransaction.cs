using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LevendMonopoly.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("aeb346ba-b3a0-4fbb-8421-0a18d4e414a3"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("ce40edab-7fbd-4571-b3b4-75b36cc7ba19"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("284c9b91-0416-464c-84bf-947ee16b924a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e2225ba0-781b-4aa3-aa94-f9383116dd69"));

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Sender = table.Column<Guid>(type: "uuid", nullable: true),
                    Receiver = table.Column<Guid>(type: "uuid", nullable: true),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("083d8c7a-64a6-4887-b688-7ee4a85a538f"), "Moderator" },
                    { new Guid("c8636a47-1261-40a4-93f7-f198edc2d00e"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Balance", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { new Guid("d4b91a06-4e86-4d11-894a-098255dc7db7"), 5000, "RensTest", "8dAzbtuUuxNOml8/fba19SqzBZ+FDhR8S9or2/qsBcQ=", "a7+HYyS5g1odePrOfwalQw==" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt", "RoleId" },
                values: new object[] { new Guid("0933e7f1-b8c4-4f2f-9b43-7bbc9dcb2666"), "Rens", "cTkzcuMY6PmRMoSjfkmeyNNOrQqbd/Y+PucIF7CUEi4=", "HUCW8XkUJ68MYdqTneCHOw==", new Guid("c8636a47-1261-40a4-93f7-f198edc2d00e") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("083d8c7a-64a6-4887-b688-7ee4a85a538f"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("d4b91a06-4e86-4d11-894a-098255dc7db7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0933e7f1-b8c4-4f2f-9b43-7bbc9dcb2666"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c8636a47-1261-40a4-93f7-f198edc2d00e"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("aeb346ba-b3a0-4fbb-8421-0a18d4e414a3"), "Moderator" },
                    { new Guid("e2225ba0-781b-4aa3-aa94-f9383116dd69"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Balance", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { new Guid("ce40edab-7fbd-4571-b3b4-75b36cc7ba19"), 5000, "RensTest", "aF1P4nv3RoKmfcEXSe88yMLVJxfEcd/g4ns+IC1Or8Y=", "+vFKmWwTcQAEFMyFGHGmtg==" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt", "RoleId" },
                values: new object[] { new Guid("284c9b91-0416-464c-84bf-947ee16b924a"), "Rens", "z+5v/bBWpJJGs7H20AZM64LcBb2UDG0a5ZASEHx+cYM=", "1vsHz0XcUZzXd+dBGDZuQg==", new Guid("e2225ba0-781b-4aa3-aa94-f9383116dd69") });
        }
    }
}
