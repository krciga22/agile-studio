using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class MakeScopeNotNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "scope",
                schema: "security",
                table: "role_permission",
                type: "text",
                nullable: false,
                defaultValue: "global",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "scope",
                schema: "security",
                table: "role_permission",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "global");
        }
    }
}
