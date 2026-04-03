using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class DropPermissionIDFieldAndReplaceWithPermissionKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_role_permission_permission_id",
                table: "role_permission");

            migrationBuilder.DropIndex(
                name: "ix_role_permission_permission_id",
                table: "role_permission");

            migrationBuilder.DropPrimaryKey(
                name: "pk_permission",
                table: "permission");

            migrationBuilder.DropColumn(
                name: "permission_id",
                table: "role_permission");

            migrationBuilder.DropColumn(
                name: "id",
                table: "permission");

            migrationBuilder.AddColumn<string>(
                name: "permission_key",
                table: "role_permission",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "pk_permission",
                table: "permission",
                column: "permission_key");

            migrationBuilder.CreateIndex(
                name: "ix_role_permission_permission_key",
                table: "role_permission",
                column: "permission_key");

            migrationBuilder.AddForeignKey(
                name: "fk_role_permission_permission_key",
                table: "role_permission",
                column: "permission_key",
                principalTable: "permission",
                principalColumn: "permission_key",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_role_permission_permission_key",
                table: "role_permission");

            migrationBuilder.DropIndex(
                name: "ix_role_permission_permission_key",
                table: "role_permission");

            migrationBuilder.DropPrimaryKey(
                name: "pk_permission",
                table: "permission");

            migrationBuilder.DropColumn(
                name: "permission_key",
                table: "role_permission");

            migrationBuilder.AddColumn<int>(
                name: "permission_id",
                table: "role_permission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "permission",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_permission",
                table: "permission",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_role_permission_permission_id",
                table: "role_permission",
                column: "permission_id");

            migrationBuilder.AddForeignKey(
                name: "fk_role_permission_permission_id",
                table: "role_permission",
                column: "permission_id",
                principalTable: "permission",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
