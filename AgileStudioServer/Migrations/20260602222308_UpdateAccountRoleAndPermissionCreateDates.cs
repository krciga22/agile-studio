using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccountRoleAndPermissionCreateDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "security",
                table: "role",
                keyColumn: "role_key",
                keyValue: "accounts-account-owner",
                column: "created_on",
                value: new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "accounts-account-owner", "account" },
                column: "created_on",
                value: new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "accounts-account-owner", "account" },
                column: "created_on",
                value: new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "accounts-account-owner", "account" },
                column: "created_on",
                value: new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "security",
                table: "role",
                keyColumn: "role_key",
                keyValue: "accounts-account-owner",
                column: "created_on",
                value: new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "accounts-account-owner", "account" },
                column: "created_on",
                value: new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "accounts-account-owner", "account" },
                column: "created_on",
                value: new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "accounts-account-owner", "account" },
                column: "created_on",
                value: new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
