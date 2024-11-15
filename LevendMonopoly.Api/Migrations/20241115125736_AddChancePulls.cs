using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LevendMonopoly.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddChancePulls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "ChanceCardPulls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChanceCardPulls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChanceCardPulls_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ChanceCardPulls_TeamId",
                table: "ChanceCardPulls",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChanceCardPulls");

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
    }
}
