using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddBacklogItemTypeSchemaNode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "backlog_item_type_schema_node",
                schema: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    schema_id = table.Column<int>(type: "integer", nullable: false),
                    backlog_item_type_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_backlog_item_type_schema_node", x => x.id);
                    table.ForeignKey(
                        name: "fk_backlog_item_type_schema_node_backlog_item_type",
                        column: x => x.backlog_item_type_id,
                        principalSchema: "accounts",
                        principalTable: "backlog_item_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backlog_item_type_schema_node_schema",
                        column: x => x.schema_id,
                        principalSchema: "accounts",
                        principalTable: "backlog_item_type_schema",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backlog_item_type_schema_node_user_created_by_id",
                        column: x => x.created_by_id,
                        principalSchema: "users",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_node_backlog_item_type_id",
                schema: "accounts",
                table: "backlog_item_type_schema_node",
                column: "backlog_item_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_node_created_by_id",
                schema: "accounts",
                table: "backlog_item_type_schema_node",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_node_schema_id",
                schema: "accounts",
                table: "backlog_item_type_schema_node",
                column: "schema_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "backlog_item_type_schema_node",
                schema: "accounts");
        }
    }
}
