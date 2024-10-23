using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LevendMonopoly.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9005f9ab-6b62-4784-bc18-ba69d412ec21"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("4f491f90-1361-4305-b7a9-ec789e378e74"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c35f98c2-021d-4a5d-8a39-65d253b39fbf"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("482faab5-c9de-4060-ba5c-3b9b4696515a"));

            migrationBuilder.AddColumn<bool>(
                name: "Tax",
                table: "Buildings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Tax",
                table: "Buildings");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("482faab5-c9de-4060-ba5c-3b9b4696515a"), "Admin" },
                    { new Guid("9005f9ab-6b62-4784-bc18-ba69d412ec21"), "Moderator" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Balance", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { new Guid("4f491f90-1361-4305-b7a9-ec789e378e74"), 5000, "RensTest", "VrSJjr+9B7sdolHuZLWnMVED0bXm9R3miR5navS8Bns=", "M2Mk59lVllyVfi1BDlmlXw==" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "PasswordSalt", "RoleId" },
                values: new object[] { new Guid("c35f98c2-021d-4a5d-8a39-65d253b39fbf"), "mulderrens@outlook.com", "Admin", "m+NRVSbvOp8h9iVnRMVarES2pGMW2GIr1K36XrFL/sQ=", "FHF9KS9/BhD5xiFHKh2kUQ==", new Guid("482faab5-c9de-4060-ba5c-3b9b4696515a") });
        }
    }
}
