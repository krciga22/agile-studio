using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSystemPermissionColumnToPermissionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_system_permission",
                table: "permission",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-item-delete",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-item-read",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-item-update",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-items-create",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-items-read",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-delete",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-grant-role",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-read",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-remove",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-revoke-role",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-members-add",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-members-read",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-read",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-update",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-projects-create",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-projects-read",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-release-delete",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-release-read",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-release-update",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-releases-create",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-releases-read",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprint-delete",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprint-read",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprint-update",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprints-create",
                column: "is_system_permission",
                value: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprints-read",
                column: "is_system_permission",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_system_permission",
                table: "permission");
        }
    }
}
