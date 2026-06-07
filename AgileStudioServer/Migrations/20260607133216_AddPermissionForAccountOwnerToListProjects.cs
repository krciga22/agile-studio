using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissionForAccountOwnerToListProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "security",
                table: "role_permission",
                columns: new[] { "permission_key", "role_key", "scope", "created_by_id", "created_on", "is_system_role_permission" },
                values: new object[] { "list", "accounts-account-owner", "project", null, new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "accounts-account-owner", "project" });
        }
    }
}
