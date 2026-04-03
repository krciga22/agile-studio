using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class RenameRoleUUIDFieldToRoleKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_role_uuid_unique",
                table: "role");

            migrationBuilder.DropColumn(
                name: "uuid",
                table: "role");

            migrationBuilder.AddColumn<string>(
                name: "role_key",
                table: "role",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_role_rolekey_unique",
                table: "role",
                column: "role_key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_role_rolekey_unique",
                table: "role");

            migrationBuilder.DropColumn(
                name: "role_key",
                table: "role");

            migrationBuilder.AddColumn<string>(
                name: "uuid",
                table: "role",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_role_uuid_unique",
                table: "role",
                column: "uuid",
                unique: true);
        }
    }
}
