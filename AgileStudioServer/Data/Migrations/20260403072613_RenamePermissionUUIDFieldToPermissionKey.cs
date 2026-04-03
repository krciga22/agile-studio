using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class RenamePermissionUUIDFieldToPermissionKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_permission_uuid_unique",
                table: "permission");

            migrationBuilder.DropColumn(
                name: "uuid",
                table: "permission");

            migrationBuilder.AddColumn<string>(
                name: "permission_key",
                table: "permission",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_permission_permissionkey_unique",
                table: "permission",
                column: "permission_key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_permission_permissionkey_unique",
                table: "permission");

            migrationBuilder.DropColumn(
                name: "permission_key",
                table: "permission");

            migrationBuilder.AddColumn<string>(
                name: "uuid",
                table: "permission",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_permission_uuid_unique",
                table: "permission",
                column: "uuid",
                unique: true);
        }
    }
}
