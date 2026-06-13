using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountIDFieldToWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "account_id",
                schema: "accounts",
                table: "workflow",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_workflow_account_id",
                schema: "accounts",
                table: "workflow",
                column: "account_id");

            migrationBuilder.AddForeignKey(
                name: "fk_workflow_account_account_id",
                schema: "accounts",
                table: "workflow",
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
                name: "fk_workflow_account_account_id",
                schema: "accounts",
                table: "workflow");

            migrationBuilder.DropIndex(
                name: "ix_workflow_account_id",
                schema: "accounts",
                table: "workflow");

            migrationBuilder.DropColumn(
                name: "account_id",
                schema: "accounts",
                table: "workflow");
        }
    }
}
