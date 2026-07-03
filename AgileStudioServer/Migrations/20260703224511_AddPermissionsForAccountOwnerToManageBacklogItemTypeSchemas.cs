using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissionsForAccountOwnerToManageBacklogItemTypeSchemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "security",
                table: "role_permission",
                columns: new[] { "permission_key", "role_key", "scope", "created_by_id", "created_on", "is_system_role_permission" },
                values: new object[,]
                {
                    { "create", "accounts-account-owner", "account.backlogItemTypeSchema", null, new DateTime(2025, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "accounts-account-owner", "account.backlogItemTypeSchema", null, new DateTime(2025, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "accounts-account-owner", "account.backlogItemTypeSchema", null, new DateTime(2025, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "accounts-account-owner", "account.backlogItemTypeSchema", null, new DateTime(2025, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "accounts-account-owner", "account.backlogItemTypeSchema", null, new DateTime(2025, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc), true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "accounts-account-owner", "account.backlogItemTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "accounts-account-owner", "account.backlogItemTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "accounts-account-owner", "account.backlogItemTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "accounts-account-owner", "account.backlogItemTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "accounts-account-owner", "account.backlogItemTypeSchema" });
        }
    }
}
