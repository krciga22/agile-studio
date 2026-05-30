using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "accounts",
                table: "account_type",
                columns: new[] { "id", "created_by_id", "created_on", "description", "title" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, "Individual" },
                    { 2, null, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, "Organization" },
                    { 3, null, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), null, "Business" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "accounts",
                table: "account_type",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "accounts",
                table: "account_type",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "accounts",
                table: "account_type",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
