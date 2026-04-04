using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class ChangeScopeIDToStringForRoleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "scope_id",
                table: "role",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "scope_id",
                table: "role",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
