using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountOwnerRoleAndPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "security",
                table: "role",
                columns: new[] { "role_key", "created_by_id", "created_on", "description", "is_system_role", "scope", "title" },
                values: new object[] { "accounts-account-owner", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "account", "Account Owner" });

            migrationBuilder.InsertData(
                schema: "security",
                table: "role_permission",
                columns: new[] { "permission_key", "role_key", "scope", "created_by_id", "created_on", "is_system_role_permission" },
                values: new object[,]
                {
                    { "delete", "accounts-account-owner", "account", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "accounts-account-owner", "account", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "accounts-account-owner", "account", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "accounts-account-owner", "account" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "accounts-account-owner", "account" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "accounts-account-owner", "account" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role",
                keyColumn: "role_key",
                keyValue: "accounts-account-owner");
        }
    }
}
