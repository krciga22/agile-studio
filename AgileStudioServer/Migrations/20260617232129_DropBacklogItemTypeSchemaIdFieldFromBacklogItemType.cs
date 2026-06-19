using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class DropBacklogItemTypeSchemaIdFieldFromBacklogItemType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_type_backlog_item_type_schema_id",
                schema: "accounts",
                table: "backlog_item_type");

            migrationBuilder.DropIndex(
                name: "ix_backlog_item_type_backlog_item_type_schema_id",
                schema: "accounts",
                table: "backlog_item_type");

            migrationBuilder.DropColumn(
                name: "backlog_item_type_schema_id",
                schema: "accounts",
                table: "backlog_item_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "backlog_item_type_schema_id",
                schema: "accounts",
                table: "backlog_item_type",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_backlog_item_type_schema_id",
                schema: "accounts",
                table: "backlog_item_type",
                column: "backlog_item_type_schema_id");

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_type_backlog_item_type_schema_id",
                schema: "accounts",
                table: "backlog_item_type",
                column: "backlog_item_type_schema_id",
                principalSchema: "accounts",
                principalTable: "backlog_item_type_schema",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
