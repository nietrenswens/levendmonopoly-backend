using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LevendMonopoly.Api.Migrations
{
    /// <inheritdoc />
    public partial class MakeCreatedOfBuildingNotRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
