using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissionsForProjectRolesToListAndReadBacklogItemWorkflowStates : Migration
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
                    { "list", "projects-project-admin", "project.backlogItemWorkflowState", null, new DateTime(2026, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-admin", "project.backlogItemWorkflowState", null, new DateTime(2026, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-business-analyst", "project.backlogItemWorkflowState", null, new DateTime(2026, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-business-analyst", "project.backlogItemWorkflowState", null, new DateTime(2026, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-developer", "project.backlogItemWorkflowState", null, new DateTime(2026, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-developer", "project.backlogItemWorkflowState", null, new DateTime(2026, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-manager", "project.backlogItemWorkflowState", null, new DateTime(2026, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-manager", "project.backlogItemWorkflowState", null, new DateTime(2026, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-tester", "project.backlogItemWorkflowState", null, new DateTime(2026, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-tester", "project.backlogItemWorkflowState", null, new DateTime(2026, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-admin", "project.backlogItemWorkflowState" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-admin", "project.backlogItemWorkflowState" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-business-analyst", "project.backlogItemWorkflowState" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-business-analyst", "project.backlogItemWorkflowState" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-developer", "project.backlogItemWorkflowState" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-developer", "project.backlogItemWorkflowState" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-manager", "project.backlogItemWorkflowState" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-manager", "project.backlogItemWorkflowState" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-tester", "project.backlogItemWorkflowState" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-tester", "project.backlogItemWorkflowState" });
        }
    }
}
