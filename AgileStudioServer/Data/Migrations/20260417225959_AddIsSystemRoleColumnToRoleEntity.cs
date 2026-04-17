using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSystemRoleColumnToRoleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_system_role",
                table: "role",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-admin",
                column: "is_system_role",
                value: true);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-business-analyst",
                column: "is_system_role",
                value: true);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-developer",
                column: "is_system_role",
                value: true);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-manager",
                column: "is_system_role",
                value: true);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-tester",
                column: "is_system_role",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_system_role",
                table: "role");
        }
    }
}
