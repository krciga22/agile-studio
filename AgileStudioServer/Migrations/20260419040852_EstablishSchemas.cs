using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class EstablishSchemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "projects");

            migrationBuilder.EnsureSchema(
                name: "accounts");

            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.RenameTable(
                name: "workflow_state",
                newName: "workflow_state",
                newSchema: "accounts");

            migrationBuilder.RenameTable(
                name: "workflow",
                newName: "workflow",
                newSchema: "accounts");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "user",
                newSchema: "users");

            migrationBuilder.RenameTable(
                name: "sprint",
                newName: "sprint",
                newSchema: "projects");

            migrationBuilder.RenameTable(
                name: "role_permission",
                newName: "role_permission",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "role_grant",
                newName: "role_grant",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "role",
                newName: "role",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "release",
                newName: "release",
                newSchema: "projects");

            migrationBuilder.RenameTable(
                name: "project",
                newName: "project",
                newSchema: "projects");

            migrationBuilder.RenameTable(
                name: "permission",
                newName: "permission",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "child_backlog_item_type",
                newName: "child_backlog_item_type",
                newSchema: "accounts");

            migrationBuilder.RenameTable(
                name: "backlog_item_type_schema",
                newName: "backlog_item_type_schema",
                newSchema: "accounts");

            migrationBuilder.RenameTable(
                name: "backlog_item_type",
                newName: "backlog_item_type",
                newSchema: "accounts");

            migrationBuilder.RenameTable(
                name: "backlog_item_link_type_schema_entry",
                newName: "backlog_item_link_type_schema_entry",
                newSchema: "accounts");

            migrationBuilder.RenameTable(
                name: "backlog_item_link_type_schema",
                newName: "backlog_item_link_type_schema",
                newSchema: "accounts");

            migrationBuilder.RenameTable(
                name: "backlog_item_link_type",
                newName: "backlog_item_link_type",
                newSchema: "accounts");

            migrationBuilder.RenameTable(
                name: "backlog_item",
                newName: "backlog_item",
                newSchema: "projects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "workflow_state",
                schema: "accounts",
                newName: "workflow_state");

            migrationBuilder.RenameTable(
                name: "workflow",
                schema: "accounts",
                newName: "workflow");

            migrationBuilder.RenameTable(
                name: "user",
                schema: "users",
                newName: "user");

            migrationBuilder.RenameTable(
                name: "sprint",
                schema: "projects",
                newName: "sprint");

            migrationBuilder.RenameTable(
                name: "role_permission",
                schema: "security",
                newName: "role_permission");

            migrationBuilder.RenameTable(
                name: "role_grant",
                schema: "security",
                newName: "role_grant");

            migrationBuilder.RenameTable(
                name: "role",
                schema: "security",
                newName: "role");

            migrationBuilder.RenameTable(
                name: "release",
                schema: "projects",
                newName: "release");

            migrationBuilder.RenameTable(
                name: "project",
                schema: "projects",
                newName: "project");

            migrationBuilder.RenameTable(
                name: "permission",
                schema: "security",
                newName: "permission");

            migrationBuilder.RenameTable(
                name: "child_backlog_item_type",
                schema: "accounts",
                newName: "child_backlog_item_type");

            migrationBuilder.RenameTable(
                name: "backlog_item_type_schema",
                schema: "accounts",
                newName: "backlog_item_type_schema");

            migrationBuilder.RenameTable(
                name: "backlog_item_type",
                schema: "accounts",
                newName: "backlog_item_type");

            migrationBuilder.RenameTable(
                name: "backlog_item_link_type_schema_entry",
                schema: "accounts",
                newName: "backlog_item_link_type_schema_entry");

            migrationBuilder.RenameTable(
                name: "backlog_item_link_type_schema",
                schema: "accounts",
                newName: "backlog_item_link_type_schema");

            migrationBuilder.RenameTable(
                name: "backlog_item_link_type",
                schema: "accounts",
                newName: "backlog_item_link_type");

            migrationBuilder.RenameTable(
                name: "backlog_item",
                schema: "projects",
                newName: "backlog_item");
        }
    }
}
