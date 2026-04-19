using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateForPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    auth_server_user_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "backlog_item_link_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    title_opposite = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_backlog_item_link_type", x => x.id);
                    table.ForeignKey(
                        name: "fk_backlog_item_link_type_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "backlog_item_link_type_schema",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_backlog_item_link_type_schema", x => x.id);
                    table.ForeignKey(
                        name: "fk_backlog_item_link_type_schema_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "backlog_item_type_schema",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_backlog_item_type_schema", x => x.id);
                    table.ForeignKey(
                        name: "fk_backlog_item_type_schema_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "permission",
                columns: table => new
                {
                    permission_key = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    scope = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true),
                    is_system_permission = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission", x => x.permission_key);
                    table.ForeignKey(
                        name: "fk_permission_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    role_key = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    scope = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true),
                    is_system_role = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role", x => x.role_key);
                    table.ForeignKey(
                        name: "fk_role_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "workflow",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workflow", x => x.id);
                    table.ForeignKey(
                        name: "fk_workflow_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "backlog_item_link_type_schema_entry",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    backlog_item_link_type_schema_id = table.Column<int>(type: "integer", nullable: false),
                    backlog_item_link_type_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true),
                    backlog_item_type_schema_id = table.Column<int>(type: "integer", nullable: false),
                    backlog_item_link_type_schema_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project", x => x.id);
                    table.ForeignKey(
                        name: "fk_project_backlog_item_link_type_schema_id",
                        column: x => x.backlog_item_link_type_schema_id,
                        principalTable: "backlog_item_link_type_schema",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_project_backlog_item_type_schema_id",
                        column: x => x.backlog_item_type_schema_id,
                        principalTable: "backlog_item_type_schema",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_project_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "role_grant",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_key = table.Column<string>(type: "text", nullable: false),
                    subject_type = table.Column<string>(type: "text", nullable: false),
                    subject_id = table.Column<string>(type: "text", nullable: false),
                    scope = table.Column<string>(type: "text", nullable: true),
                    scope_id = table.Column<string>(type: "text", nullable: true),
                    hash = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_grant", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_grant_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_role_subject_role_key",
                        column: x => x.role_key,
                        principalTable: "role",
                        principalColumn: "role_key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permission",
                columns: table => new
                {
                    role_key = table.Column<string>(type: "text", nullable: false),
                    permission_key = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true),
                    is_system_role_permission = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permission", x => new { x.role_key, x.permission_key });
                    table.ForeignKey(
                        name: "fk_role_permission_permission_key",
                        column: x => x.permission_key,
                        principalTable: "permission",
                        principalColumn: "permission_key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_role_permission_role_key",
                        column: x => x.role_key,
                        principalTable: "role",
                        principalColumn: "role_key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_role_permission_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "backlog_item_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true),
                    backlog_item_type_schema_id = table.Column<int>(type: "integer", nullable: false),
                    workflow_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_backlog_item_type", x => x.id);
                    table.ForeignKey(
                        name: "fk_backlog_item_type_backlog_item_type_schema_id",
                        column: x => x.backlog_item_type_schema_id,
                        principalTable: "backlog_item_type_schema",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backlog_item_type_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_backlog_item_type_workflow_workflow_id",
                        column: x => x.workflow_id,
                        principalTable: "workflow",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "workflow_state",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    workflow_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workflow_state", x => x.id);
                    table.ForeignKey(
                        name: "fk_workflow_state_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_workflow_state_workflow_workflow_id",
                        column: x => x.workflow_id,
                        principalTable: "workflow",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "release",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    project_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_release", x => x.id);
                    table.ForeignKey(
                        name: "fk_release_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_release_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "sprint",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sprint_number = table.Column<int>(type: "integer", nullable: false),
                    project_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sprint", x => x.id);
                    table.ForeignKey(
                        name: "fk_sprint_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sprint_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "child_backlog_item_type",
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
                    table.PrimaryKey("pk_child_backlog_item_type", x => x.id);
                    table.ForeignKey(
                        name: "fk_child_backlog_item_type_child_type_backlog_item_type_id",
                        column: x => x.child_type_id,
                        principalTable: "backlog_item_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_child_backlog_item_type_parent_type_backlog_item_type_id",
                        column: x => x.parent_type_id,
                        principalTable: "backlog_item_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_child_backlog_item_type_schema_backlog_item_type_schema_id",
                        column: x => x.schema_id,
                        principalTable: "backlog_item_type_schema",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_child_backlog_item_type_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "backlog_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<int>(type: "integer", nullable: true),
                    project_id = table.Column<int>(type: "integer", nullable: false),
                    sprint_id = table.Column<int>(type: "integer", nullable: true),
                    release_id = table.Column<int>(type: "integer", nullable: true),
                    backlog_item_type_id = table.Column<int>(type: "integer", nullable: false),
                    workflow_state_id = table.Column<int>(type: "integer", nullable: false),
                    parent_backlog_item_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_backlog_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_backlog_item_backlog_item_type_id",
                        column: x => x.backlog_item_type_id,
                        principalTable: "backlog_item_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backlog_item_parent_backlog_item_id",
                        column: x => x.parent_backlog_item_id,
                        principalTable: "backlog_item",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_backlog_item_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backlog_item_release_release_id",
                        column: x => x.release_id,
                        principalTable: "release",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_backlog_item_sprint_sprint_id",
                        column: x => x.sprint_id,
                        principalTable: "sprint",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_backlog_item_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_backlog_item_workflow_state_id",
                        column: x => x.workflow_state_id,
                        principalTable: "workflow_state",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "permission_key", "created_by_id", "created_on", "description", "is_system_permission", "scope", "title" },
                values: new object[,]
                {
                    { "projects-backlog-item-delete", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Backlog Item Delete" },
                    { "projects-backlog-item-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Backlog Item Read" },
                    { "projects-backlog-item-update", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Backlog Item Update" },
                    { "projects-backlog-items-create", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Backlog Items Create" },
                    { "projects-backlog-items-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Backlog Items Read" },
                    { "projects-project-delete", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Project Delete" },
                    { "projects-project-member-grant-role", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Project Member Grant Role" },
                    { "projects-project-member-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Project Member Read" },
                    { "projects-project-member-remove", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Project Member Remove" },
                    { "projects-project-member-revoke-role", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Project Member Revoke Role" },
                    { "projects-project-members-add", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Project Members Add" },
                    { "projects-project-members-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Project Members Read" },
                    { "projects-project-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Project Read" },
                    { "projects-project-update", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Project Update" },
                    { "projects-projects-create", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Projects Create" },
                    { "projects-projects-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Projects Read" },
                    { "projects-release-delete", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Release Delete" },
                    { "projects-release-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Release Read" },
                    { "projects-release-update", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Release Update" },
                    { "projects-releases-create", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Releases Create" },
                    { "projects-releases-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Releases Read" },
                    { "projects-sprint-delete", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Sprint Delete" },
                    { "projects-sprint-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Sprint Read" },
                    { "projects-sprint-update", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Sprint Update" },
                    { "projects-sprints-create", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Sprints Create" },
                    { "projects-sprints-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Sprints Read" }
                });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "role_key", "created_by_id", "created_on", "description", "is_system_role", "scope", "title" },
                values: new object[,]
                {
                    { "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Project Admin" },
                    { "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Business Analyst" },
                    { "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Developer" },
                    { "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Project Manager" },
                    { "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "projects", "Tester" }
                });

            migrationBuilder.InsertData(
                table: "role_permission",
                columns: new[] { "permission_key", "role_key", "created_by_id", "created_on", "is_system_role_permission" },
                values: new object[,]
                {
                    { "projects-backlog-item-delete", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-update", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-items-create", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-items-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-delete", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-grant-role", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-remove", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-revoke-role", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-members-add", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-members-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-update", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-release-delete", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-release-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-release-update", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-releases-create", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-releases-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprint-delete", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprint-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprint-update", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprints-create", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprints-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-delete", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-read", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-update", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-items-read", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-read", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-members-read", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-read", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-delete", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-read", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-update", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-items-create", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-items-read", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-read", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-members-read", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-read", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-delete", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-update", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-items-create", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-items-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-grant-role", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-remove", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-revoke-role", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-members-add", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-members-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-update", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-release-delete", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-release-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-release-update", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-releases-create", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-releases-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprint-delete", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprint-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprint-update", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprints-create", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprints-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-delete", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-read", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-update", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-items-read", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-read", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-members-read", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-read", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true }
                });

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_backlog_item_type_id",
                table: "backlog_item",
                column: "backlog_item_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_created_by_id",
                table: "backlog_item",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_parent_backlog_item_id",
                table: "backlog_item",
                column: "parent_backlog_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_project_id",
                table: "backlog_item",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_release_id",
                table: "backlog_item",
                column: "release_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_sprint_id",
                table: "backlog_item",
                column: "sprint_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_workflow_state_id",
                table: "backlog_item",
                column: "workflow_state_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_link_type_created_by_id",
                table: "backlog_item_link_type",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_link_type_schema_created_by_id",
                table: "backlog_item_link_type_schema",
                column: "created_by_id");

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

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_backlog_item_type_schema_id",
                table: "backlog_item_type",
                column: "backlog_item_type_schema_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_created_by_id",
                table: "backlog_item_type",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_workflow_id",
                table: "backlog_item_type",
                column: "workflow_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_created_by_id",
                table: "backlog_item_type_schema",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_child_backlog_item_type_child_type_id",
                table: "child_backlog_item_type",
                column: "child_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_child_backlog_item_type_created_by_id",
                table: "child_backlog_item_type",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_child_backlog_item_type_parent_type_id",
                table: "child_backlog_item_type",
                column: "parent_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_child_backlog_item_type_schema_id",
                table: "child_backlog_item_type",
                column: "schema_id");

            migrationBuilder.CreateIndex(
                name: "ix_permission_created_by_id",
                table: "permission",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_project_backlog_item_link_type_schema_id",
                table: "project",
                column: "backlog_item_link_type_schema_id");

            migrationBuilder.CreateIndex(
                name: "ix_project_backlog_item_type_schema_id",
                table: "project",
                column: "backlog_item_type_schema_id");

            migrationBuilder.CreateIndex(
                name: "ix_project_created_by_id",
                table: "project",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_release_created_by_id",
                table: "release",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_release_project_id",
                table: "release",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_created_by_id",
                table: "role",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_grant_created_by_id",
                table: "role_grant",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_grant_hash",
                table: "role_grant",
                column: "hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_role_grant_role_key",
                table: "role_grant",
                column: "role_key");

            migrationBuilder.CreateIndex(
                name: "ix_role_permission_created_by_id",
                table: "role_permission",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_permission_permission_key",
                table: "role_permission",
                column: "permission_key");

            migrationBuilder.CreateIndex(
                name: "ix_role_permission_role_key",
                table: "role_permission",
                column: "role_key");

            migrationBuilder.CreateIndex(
                name: "ix_sprint_created_by_id",
                table: "sprint",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_sprint_project_id",
                table: "sprint",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_workflow_created_by_id",
                table: "workflow",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_workflow_state_created_by_id",
                table: "workflow_state",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_workflow_state_workflow_id",
                table: "workflow_state",
                column: "workflow_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "backlog_item");

            migrationBuilder.DropTable(
                name: "backlog_item_link_type_schema_entry");

            migrationBuilder.DropTable(
                name: "child_backlog_item_type");

            migrationBuilder.DropTable(
                name: "role_grant");

            migrationBuilder.DropTable(
                name: "role_permission");

            migrationBuilder.DropTable(
                name: "release");

            migrationBuilder.DropTable(
                name: "sprint");

            migrationBuilder.DropTable(
                name: "workflow_state");

            migrationBuilder.DropTable(
                name: "backlog_item_link_type");

            migrationBuilder.DropTable(
                name: "backlog_item_type");

            migrationBuilder.DropTable(
                name: "permission");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "project");

            migrationBuilder.DropTable(
                name: "workflow");

            migrationBuilder.DropTable(
                name: "backlog_item_link_type_schema");

            migrationBuilder.DropTable(
                name: "backlog_item_type_schema");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
