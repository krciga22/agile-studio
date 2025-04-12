using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class CreateBacklogItemLinkTypeSchemaEntryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "backlog_item_link_type_schema_entry",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    backlog_item_link_type_schema_id = table.Column<int>(type: "int", nullable: false),
                    backlog_item_link_type_id = table.Column<int>(type: "int", nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_backlog_item_link_type_schema_entry", x => x.id);
                    table.ForeignKey(
                        name: "fk_backlog_item_link_type_schema_entry_link_type_id",
                        column: x => x.backlog_item_link_type_id,
                        principalTable: "backlog_item_link_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backlog_item_link_type_schema_entry_link_type_schema_id",
                        column: x => x.backlog_item_link_type_schema_id,
                        principalTable: "backlog_item_link_type_schema",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backlog_item_link_type_schema_entry_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_link_type_schema_entry_created_by_id",
                table: "backlog_item_link_type_schema_entry",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_link_type_schema_entry_link_type_id",
                table: "backlog_item_link_type_schema_entry",
                column: "backlog_item_link_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_link_type_schema_entry_link_type_schema_id",
                table: "backlog_item_link_type_schema_entry",
                column: "backlog_item_link_type_schema_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_link_type_schema_entry_unique",
                table: "backlog_item_link_type_schema_entry",
                columns: new[] { "backlog_item_link_type_schema_id", "backlog_item_link_type_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "backlog_item_link_type_schema_entry");
        }
    }
}
