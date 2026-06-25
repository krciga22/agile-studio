using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class MakeFromTypeIDOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_type_schema_edge_from_type",
                schema: "accounts",
                table: "backlog_item_type_schema_edge");

            migrationBuilder.AlterColumn<int>(
                name: "from_type_id",
                schema: "accounts",
                table: "backlog_item_type_schema_edge",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_type_schema_edge_from_type",
                schema: "accounts",
                table: "backlog_item_type_schema_edge",
                column: "from_type_id",
                principalSchema: "accounts",
                principalTable: "backlog_item_type",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_type_schema_edge_from_type",
                schema: "accounts",
                table: "backlog_item_type_schema_edge");

            migrationBuilder.AlterColumn<int>(
                name: "from_type_id",
                schema: "accounts",
                table: "backlog_item_type_schema_edge",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_type_schema_edge_from_type",
                schema: "accounts",
                table: "backlog_item_type_schema_edge",
                column: "from_type_id",
                principalSchema: "accounts",
                principalTable: "backlog_item_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
