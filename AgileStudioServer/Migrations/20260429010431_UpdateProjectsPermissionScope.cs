using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProjectsPermissionScope : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-item-delete",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-item-read",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-item-update",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-items-create",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-items-read",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-delete",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-grant-role",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-read",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-remove",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-revoke-role",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-members-add",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-members-read",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-read",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-update",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-projects-create",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-projects-read",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-release-delete",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-release-read",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-release-update",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-releases-create",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-releases-read",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprint-delete",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprint-read",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprint-update",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprints-create",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprints-read",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-admin",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-business-analyst",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-developer",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-manager",
                column: "scope",
                value: "projects.project");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-tester",
                column: "scope",
                value: "projects.project");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-item-delete",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-item-read",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-item-update",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-items-create",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-items-read",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-delete",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-grant-role",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-read",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-remove",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-revoke-role",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-members-add",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-members-read",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-read",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-update",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-projects-create",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-projects-read",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-release-delete",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-release-read",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-release-update",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-releases-create",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-releases-read",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprint-delete",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprint-read",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprint-update",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprints-create",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprints-read",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-admin",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-business-analyst",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-developer",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-manager",
                column: "scope",
                value: "projects");

            migrationBuilder.UpdateData(
                schema: "security",
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-tester",
                column: "scope",
                value: "projects");
        }
    }
}
