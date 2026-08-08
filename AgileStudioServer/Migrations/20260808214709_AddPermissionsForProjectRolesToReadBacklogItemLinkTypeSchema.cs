using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissionsForProjectRolesToReadBacklogItemLinkTypeSchema : Migration
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
                    { "read", "projects-project-admin", "project.backlogItemLinkTypeSchema", null, new DateTime(2026, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-business-analyst", "project.backlogItemLinkTypeSchema", null, new DateTime(2026, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-developer", "project.backlogItemLinkTypeSchema", null, new DateTime(2026, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-manager", "project.backlogItemLinkTypeSchema", null, new DateTime(2026, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-tester", "project.backlogItemLinkTypeSchema", null, new DateTime(2026, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-admin", "project.backlogItemLinkTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-business-analyst", "project.backlogItemLinkTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-developer", "project.backlogItemLinkTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-manager", "project.backlogItemLinkTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-tester", "project.backlogItemLinkTypeSchema" });
        }
    }
}
