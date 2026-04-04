using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class DropScopeIDFromRoleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "scope_id",
                table: "role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "scope_id",
                table: "role",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-admin",
                column: "scope_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-business-analyst",
                column: "scope_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-developer",
                column: "scope_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-manager",
                column: "scope_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-tester",
                column: "scope_id",
                value: null);
        }
    }
}
