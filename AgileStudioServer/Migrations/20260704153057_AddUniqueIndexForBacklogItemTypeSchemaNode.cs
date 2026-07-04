using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexForBacklogItemTypeSchemaNode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_backlog_item_type_schema_node_schema_id",
                schema: "accounts",
                table: "backlog_item_type_schema_node");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_node_schema_type",
                schema: "accounts",
                table: "backlog_item_type_schema_node",
                columns: new[] { "schema_id", "backlog_item_type_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_backlog_item_type_schema_node_schema_type",
                schema: "accounts",
                table: "backlog_item_type_schema_node");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_node_schema_id",
                schema: "accounts",
                table: "backlog_item_type_schema_node",
                column: "schema_id");
        }
    }
}
