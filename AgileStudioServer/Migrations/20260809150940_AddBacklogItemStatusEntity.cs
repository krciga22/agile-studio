using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddBacklogItemStatusEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "backlog_item_status",
                schema: "projects",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    backlog_item_id = table.Column<int>(type: "integer", nullable: false),
                    workflow_state_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true),
                    comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_backlog_item_status", x => x.id);
                    table.ForeignKey(
                        name: "fk_backlog_item_status_backlog_item_id",
                        column: x => x.backlog_item_id,
                        principalSchema: "projects",
                        principalTable: "backlog_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backlog_item_status_user_created_by_id",
                        column: x => x.created_by_id,
                        principalSchema: "users",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_backlog_item_status_workflow_state_id",
                        column: x => x.workflow_state_id,
                        principalSchema: "accounts",
                        principalTable: "workflow_state",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_status_backlog_item_id",
                schema: "projects",
                table: "backlog_item_status",
                column: "backlog_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_status_created_by_id",
                schema: "projects",
                table: "backlog_item_status",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_status_workflow_state_id",
                schema: "projects",
                table: "backlog_item_status",
                column: "workflow_state_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "backlog_item_status",
                schema: "projects");
        }
    }
}
