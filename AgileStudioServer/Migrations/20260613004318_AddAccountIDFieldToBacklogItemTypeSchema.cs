using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountIDFieldToBacklogItemTypeSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "account_id",
                schema: "accounts",
                table: "backlog_item_type_schema",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_account_id",
                schema: "accounts",
                table: "backlog_item_type_schema",
                column: "account_id");

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_type_schema_account_account_id",
                schema: "accounts",
                table: "backlog_item_type_schema",
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
                name: "fk_backlog_item_type_schema_account_account_id",
                schema: "accounts",
                table: "backlog_item_type_schema");

            migrationBuilder.DropIndex(
                name: "ix_backlog_item_type_schema_account_id",
                schema: "accounts",
                table: "backlog_item_type_schema");

            migrationBuilder.DropColumn(
                name: "account_id",
                schema: "accounts",
                table: "backlog_item_type_schema");
        }
    }
}
