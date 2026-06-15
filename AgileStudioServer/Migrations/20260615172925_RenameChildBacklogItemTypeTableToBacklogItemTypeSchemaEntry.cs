using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class RenameChildBacklogItemTypeTableToBacklogItemTypeSchemaEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "child_backlog_item_type",
                schema: "accounts");

            migrationBuilder.CreateTable(
                name: "backlog_item_type_schema_entry",
                schema: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    child_type_id = table.Column<int>(type: "integer", nullable: false),
                    parent_type_id = table.Column<int>(type: "integer", nullable: false),
                    schema_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_backlog_item_type_schema_entry", x => x.id);
                    table.ForeignKey(
                        name: "fk_backlog_item_type_schema_entry_child_type",
                        column: x => x.child_type_id,
                        principalSchema: "accounts",
                        principalTable: "backlog_item_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backlog_item_type_schema_entry_parent_type",
                        column: x => x.parent_type_id,
                        principalSchema: "accounts",
                        principalTable: "backlog_item_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backlog_item_type_schema_entry_schema",
                        column: x => x.schema_id,
                        principalSchema: "accounts",
                        principalTable: "backlog_item_type_schema",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backlog_item_type_schema_entry_user_created_by_id",
                        column: x => x.created_by_id,
                        principalSchema: "users",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_entry_child_type_id",
                schema: "accounts",
                table: "backlog_item_type_schema_entry",
                column: "child_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_entry_created_by_id",
                schema: "accounts",
                table: "backlog_item_type_schema_entry",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_entry_parent_type_id",
                schema: "accounts",
                table: "backlog_item_type_schema_entry",
                column: "parent_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_entry_schema_id",
                schema: "accounts",
                table: "backlog_item_type_schema_entry",
                column: "schema_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "backlog_item_type_schema_entry",
                schema: "accounts");

            migrationBuilder.CreateTable(
                name: "child_backlog_item_type",
                schema: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    child_type_id = table.Column<int>(type: "integer", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true),
                    parent_type_id = table.Column<int>(type: "integer", nullable: false),
                    schema_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_child_backlog_item_type", x => x.id);
                    table.ForeignKey(
                        name: "fk_child_backlog_item_type_child_type_backlog_item_type_id",
                        column: x => x.child_type_id,
                        principalSchema: "accounts",
                        principalTable: "backlog_item_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_child_backlog_item_type_parent_type_backlog_item_type_id",
                        column: x => x.parent_type_id,
                        principalSchema: "accounts",
                        principalTable: "backlog_item_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_child_backlog_item_type_schema_backlog_item_type_schema_id",
                        column: x => x.schema_id,
                        principalSchema: "accounts",
                        principalTable: "backlog_item_type_schema",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_child_backlog_item_type_user_created_by_id",
                        column: x => x.created_by_id,
                        principalSchema: "users",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_child_backlog_item_type_child_type_id",
                schema: "accounts",
                table: "child_backlog_item_type",
                column: "child_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_child_backlog_item_type_created_by_id",
                schema: "accounts",
                table: "child_backlog_item_type",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_child_backlog_item_type_parent_type_id",
                schema: "accounts",
                table: "child_backlog_item_type",
                column: "parent_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_child_backlog_item_type_schema_id",
                schema: "accounts",
                table: "child_backlog_item_type",
                column: "schema_id");
        }
    }
}
