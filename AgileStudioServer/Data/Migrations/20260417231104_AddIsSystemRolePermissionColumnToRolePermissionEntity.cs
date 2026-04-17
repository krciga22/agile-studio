using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSystemRolePermissionColumnToRolePermissionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_system_role_permission",
                table: "role_permission",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-create", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-delete", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-grant-role", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-remove", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-revoke-role", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-add", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-read", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-update", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-delete", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-read", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-update", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-releases-create", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-releases-read", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-delete", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-read", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-update", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprints-create", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprints-read", "projects-project-admin" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-business-analyst" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-business-analyst" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-business-analyst" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-business-analyst" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-business-analyst" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-business-analyst" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-read", "projects-project-business-analyst" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-developer" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-developer" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-developer" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-create", "projects-project-developer" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-developer" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-developer" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-developer" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-read", "projects-project-developer" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-create", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-grant-role", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-remove", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-revoke-role", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-add", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-read", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-update", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-delete", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-read", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-update", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-releases-create", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-releases-read", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-delete", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-read", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-update", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprints-create", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprints-read", "projects-project-manager" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-tester" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-tester" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-tester" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-tester" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-tester" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-tester" },
                column: "is_system_role_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-read", "projects-project-tester" },
                column: "is_system_role_permission",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_system_role_permission",
                table: "role_permission");
        }
    }
}
