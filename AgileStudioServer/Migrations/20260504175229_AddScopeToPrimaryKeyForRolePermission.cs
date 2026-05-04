using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddScopeToPrimaryKeyForRolePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_role_permission",
                schema: "security",
                table: "role_permission");

            migrationBuilder.AddPrimaryKey(
                name: "pk_role_permission",
                schema: "security",
                table: "role_permission",
                columns: new[] { "role_key", "permission_key", "scope" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_role_permission",
                schema: "security",
                table: "role_permission");

            migrationBuilder.AddPrimaryKey(
                name: "pk_role_permission",
                schema: "security",
                table: "role_permission",
                columns: new[] { "role_key", "permission_key" });
        }
    }
}
