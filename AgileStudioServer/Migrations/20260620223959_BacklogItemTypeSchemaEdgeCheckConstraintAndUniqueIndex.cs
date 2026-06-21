using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class BacklogItemTypeSchemaEdgeCheckConstraintAndUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_backlog_item_type_schema_edge_schema_id",
                schema: "accounts",
                table: "backlog_item_type_schema_edge");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_edge_schema_from_to",
                schema: "accounts",
                table: "backlog_item_type_schema_edge",
                columns: new[] { "schema_id", "from_type_id", "to_type_id" },
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "ck_backlog_item_type_schema_edge_types_differ",
                schema: "accounts",
                table: "backlog_item_type_schema_edge",
                sql: "\"from_type_id\" <> \"to_type_id\"");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_backlog_item_type_schema_edge_schema_from_to",
                schema: "accounts",
                table: "backlog_item_type_schema_edge");

            migrationBuilder.DropCheckConstraint(
                name: "ck_backlog_item_type_schema_edge_types_differ",
                schema: "accounts",
                table: "backlog_item_type_schema_edge");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_edge_schema_id",
                schema: "accounts",
                table: "backlog_item_type_schema_edge",
                column: "schema_id");
        }
    }
}
