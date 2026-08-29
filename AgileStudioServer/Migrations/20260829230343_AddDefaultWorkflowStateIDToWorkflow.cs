using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultWorkflowStateIDToWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_workflow_state_workflow_workflow_id",
                schema: "accounts",
                table: "workflow_state");

            migrationBuilder.AddColumn<int>(
                name: "default_workflow_state_id",
                schema: "accounts",
                table: "workflow",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_workflow_default_workflow_state_id",
                schema: "accounts",
                table: "workflow",
                column: "default_workflow_state_id");

            migrationBuilder.AddForeignKey(
                name: "fk_workflow_default_workflow_state_id",
                schema: "accounts",
                table: "workflow",
                column: "default_workflow_state_id",
                principalSchema: "accounts",
                principalTable: "workflow_state",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_workflow_state_workflow_id",
                schema: "accounts",
                table: "workflow_state",
                column: "workflow_id",
                principalSchema: "accounts",
                principalTable: "workflow",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_workflow_default_workflow_state_id",
                schema: "accounts",
                table: "workflow");

            migrationBuilder.DropForeignKey(
                name: "fk_workflow_state_workflow_id",
                schema: "accounts",
                table: "workflow_state");

            migrationBuilder.DropIndex(
                name: "ix_workflow_default_workflow_state_id",
                schema: "accounts",
                table: "workflow");

            migrationBuilder.DropColumn(
                name: "default_workflow_state_id",
                schema: "accounts",
                table: "workflow");

            migrationBuilder.AddForeignKey(
                name: "fk_workflow_state_workflow_workflow_id",
                schema: "accounts",
                table: "workflow_state",
                column: "workflow_id",
                principalSchema: "accounts",
                principalTable: "workflow",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
