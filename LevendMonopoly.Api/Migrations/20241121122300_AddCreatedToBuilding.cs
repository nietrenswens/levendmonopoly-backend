using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LevendMonopoly.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedToBuilding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Buildings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("b0aed420-a891-4723-a0e8-80da1a073340"), "Moderator" },
                    { new Guid("eaae3e99-b97b-4403-a191-178109ecfbcf"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Balance", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { new Guid("ca785ab9-a460-4ece-9b25-bc33c7b7c6d1"), 5000, "RensTest", "DZgNeTsAhfZou85pCunYRPz+nORPu+KK8mqnsj+tt38=", "Bzjfhn6NY9AodID5A8wq/w==" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt", "RoleId" },
                values: new object[] { new Guid("6c2426b2-65a5-469b-bef7-98061b0b3159"), "Rens", "8UOfJ5DdFwk1u+YVz0byBapwiXVKETmquDfU/z31sIw=", "1nk0NwNIHIvYUl2aFN+2SA==", new Guid("eaae3e99-b97b-4403-a191-178109ecfbcf") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b0aed420-a891-4723-a0e8-80da1a073340"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("ca785ab9-a460-4ece-9b25-bc33c7b7c6d1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6c2426b2-65a5-469b-bef7-98061b0b3159"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("eaae3e99-b97b-4403-a191-178109ecfbcf"));

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Buildings");

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
    }
}
