using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AssignBacklogItemLinkTypeSchematoProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "backlog_item_link_type_schema_id",
                table: "project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_project_backlog_item_link_type_schema_id",
                table: "project",
                column: "backlog_item_link_type_schema_id");

            migrationBuilder.AddForeignKey(
                name: "fk_project_backlog_item_link_type_schema_id",
                table: "project",
                column: "backlog_item_link_type_schema_id",
                principalTable: "backlog_item_link_type_schema",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_project_backlog_item_link_type_schema_id",
                table: "project");

            migrationBuilder.DropIndex(
                name: "ix_project_backlog_item_link_type_schema_id",
                table: "project");

            migrationBuilder.DropColumn(
                name: "backlog_item_link_type_schema_id",
                table: "project");
        }
    }
}
