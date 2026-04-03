using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class DropRoleIDFieldAndReplaceWithRoleKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_role_permission_role_id",
                table: "role_permission");

            migrationBuilder.DropIndex(
                name: "ix_role_permission_role_id",
                table: "role_permission");

            migrationBuilder.DropPrimaryKey(
                name: "pk_role",
                table: "role");

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "role_permission");

            migrationBuilder.DropColumn(
                name: "id",
                table: "role");

            migrationBuilder.AddColumn<string>(
                name: "role_key",
                table: "role_permission",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "pk_role",
                table: "role",
                column: "role_key");

            migrationBuilder.CreateIndex(
                name: "ix_role_permission_role_key",
                table: "role_permission",
                column: "role_key");

            migrationBuilder.AddForeignKey(
                name: "fk_role_permission_role_key",
                table: "role_permission",
                column: "role_key",
                principalTable: "role",
                principalColumn: "role_key",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_role_permission_role_key",
                table: "role_permission");

            migrationBuilder.DropIndex(
                name: "ix_role_permission_role_key",
                table: "role_permission");

            migrationBuilder.DropPrimaryKey(
                name: "pk_role",
                table: "role");

            migrationBuilder.DropColumn(
                name: "role_key",
                table: "role_permission");

            migrationBuilder.AddColumn<int>(
                name: "role_id",
                table: "role_permission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "role",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_role",
                table: "role",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_role_permission_role_id",
                table: "role_permission",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "fk_role_permission_role_id",
                table: "role_permission",
                column: "role_id",
                principalTable: "role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
