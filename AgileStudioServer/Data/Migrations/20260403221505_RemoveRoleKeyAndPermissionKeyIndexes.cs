using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRoleKeyAndPermissionKeyIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_role_rolekey_unique",
                table: "role");

            migrationBuilder.DropIndex(
                name: "ix_permission_permissionkey_unique",
                table: "permission");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_role_rolekey_unique",
                table: "role",
                column: "role_key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_permission_permissionkey_unique",
                table: "permission",
                column: "permission_key",
                unique: true);
        }
    }
}
