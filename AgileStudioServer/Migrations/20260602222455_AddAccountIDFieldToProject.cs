using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountIDFieldToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "account_id",
                schema: "projects",
                table: "project",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_project_account_id",
                schema: "projects",
                table: "project",
                column: "account_id");

            migrationBuilder.AddForeignKey(
                name: "fk_project_account_account_id",
                schema: "projects",
                table: "project",
                column: "account_id",
                principalSchema: "accounts",
                principalTable: "account",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_project_account_account_id",
                schema: "projects",
                table: "project");

            migrationBuilder.DropIndex(
                name: "ix_project_account_id",
                schema: "projects",
                table: "project");

            migrationBuilder.DropColumn(
                name: "account_id",
                schema: "projects",
                table: "project");
        }
    }
}
