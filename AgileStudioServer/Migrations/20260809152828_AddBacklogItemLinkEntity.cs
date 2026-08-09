using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddBacklogItemLinkEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "backlog_item_link",
                schema: "projects",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    source_backlog_item_id = table.Column<int>(type: "integer", nullable: false),
                    target_backlog_item_id = table.Column<int>(type: "integer", nullable: false),
                    backlog_item_link_type_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_backlog_item_link", x => x.id);
                    table.ForeignKey(
                        name: "fk_backlog_item_link_backlog_item_link_type_id",
                        column: x => x.backlog_item_link_type_id,
                        principalSchema: "accounts",
                        principalTable: "backlog_item_link_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backlog_item_link_source_backlog_item_id",
                        column: x => x.source_backlog_item_id,
                        principalSchema: "projects",
                        principalTable: "backlog_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backlog_item_link_target_backlog_item_id",
                        column: x => x.target_backlog_item_id,
                        principalSchema: "projects",
                        principalTable: "backlog_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backlog_item_link_user_created_by_id",
                        column: x => x.created_by_id,
                        principalSchema: "users",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_link_backlog_item_link_type_id",
                schema: "projects",
                table: "backlog_item_link",
                column: "backlog_item_link_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_link_created_by_id",
                schema: "projects",
                table: "backlog_item_link",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_link_source_backlog_item_id",
                schema: "projects",
                table: "backlog_item_link",
                column: "source_backlog_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_link_target_backlog_item_id",
                schema: "projects",
                table: "backlog_item_link",
                column: "target_backlog_item_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "backlog_item_link",
                schema: "projects");
        }
    }
}
