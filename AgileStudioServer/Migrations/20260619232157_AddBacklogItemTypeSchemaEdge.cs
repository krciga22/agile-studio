using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddBacklogItemTypeSchemaEdge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "backlog_item_type_schema_edge",
                schema: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    schema_id = table.Column<int>(type: "integer", nullable: false),
                    from_type_id = table.Column<int>(type: "integer", nullable: false),
                    to_type_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_backlog_item_type_schema_edge", x => x.id);
                    table.ForeignKey(
                        name: "fk_backlog_item_type_schema_edge_from_type",
                        column: x => x.from_type_id,
                        principalSchema: "accounts",
                        principalTable: "backlog_item_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backlog_item_type_schema_edge_schema",
                        column: x => x.schema_id,
                        principalSchema: "accounts",
                        principalTable: "backlog_item_type_schema",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backlog_item_type_schema_edge_to_type",
                        column: x => x.to_type_id,
                        principalSchema: "accounts",
                        principalTable: "backlog_item_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backlog_item_type_schema_edge_user_created_by_id",
                        column: x => x.created_by_id,
                        principalSchema: "users",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_edge_created_by_id",
                schema: "accounts",
                table: "backlog_item_type_schema_edge",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_edge_from_type_id",
                schema: "accounts",
                table: "backlog_item_type_schema_edge",
                column: "from_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_edge_schema_id",
                schema: "accounts",
                table: "backlog_item_type_schema_edge",
                column: "schema_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_edge_to_type_id",
                schema: "accounts",
                table: "backlog_item_type_schema_edge",
                column: "to_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "backlog_item_type_schema_edge",
                schema: "accounts");
        }
    }
}
