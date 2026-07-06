using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissionsForProjectBacklogItemTypeSchemasNodesAndEdges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "security",
                table: "role_permission",
                columns: new[] { "permission_key", "role_key", "scope", "created_by_id", "created_on", "is_system_role_permission" },
                values: new object[,]
                {
                    { "list", "projects-project-admin", "project.backlogItemTypeSchema", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-admin", "project.backlogItemTypeSchemaEdge", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-admin", "project.backlogItemTypeSchemaNode", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-admin", "project.backlogItemTypeSchema", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-admin", "project.backlogItemTypeSchemaEdge", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-admin", "project.backlogItemTypeSchemaNode", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-business-analyst", "project.backlogItemTypeSchema", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-business-analyst", "project.backlogItemTypeSchemaEdge", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-business-analyst", "project.backlogItemTypeSchemaNode", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-business-analyst", "project.backlogItemTypeSchema", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-business-analyst", "project.backlogItemTypeSchemaEdge", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-business-analyst", "project.backlogItemTypeSchemaNode", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-developer", "project.backlogItemTypeSchema", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-developer", "project.backlogItemTypeSchemaEdge", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-developer", "project.backlogItemTypeSchemaNode", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-developer", "project.backlogItemTypeSchema", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-developer", "project.backlogItemTypeSchemaEdge", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-developer", "project.backlogItemTypeSchemaNode", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-manager", "project.backlogItemTypeSchema", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-manager", "project.backlogItemTypeSchemaEdge", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-manager", "project.backlogItemTypeSchemaNode", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-manager", "project.backlogItemTypeSchema", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-manager", "project.backlogItemTypeSchemaEdge", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-manager", "project.backlogItemTypeSchemaNode", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-tester", "project.backlogItemTypeSchema", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-tester", "project.backlogItemTypeSchemaEdge", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-tester", "project.backlogItemTypeSchemaNode", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-tester", "project.backlogItemTypeSchema", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-tester", "project.backlogItemTypeSchemaEdge", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-tester", "project.backlogItemTypeSchemaNode", null, new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-admin", "project.backlogItemTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-admin", "project.backlogItemTypeSchemaEdge" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-admin", "project.backlogItemTypeSchemaNode" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-admin", "project.backlogItemTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-admin", "project.backlogItemTypeSchemaEdge" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-admin", "project.backlogItemTypeSchemaNode" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-business-analyst", "project.backlogItemTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-business-analyst", "project.backlogItemTypeSchemaEdge" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-business-analyst", "project.backlogItemTypeSchemaNode" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-business-analyst", "project.backlogItemTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-business-analyst", "project.backlogItemTypeSchemaEdge" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-business-analyst", "project.backlogItemTypeSchemaNode" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-developer", "project.backlogItemTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-developer", "project.backlogItemTypeSchemaEdge" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-developer", "project.backlogItemTypeSchemaNode" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-developer", "project.backlogItemTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-developer", "project.backlogItemTypeSchemaEdge" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-developer", "project.backlogItemTypeSchemaNode" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-manager", "project.backlogItemTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-manager", "project.backlogItemTypeSchemaEdge" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-manager", "project.backlogItemTypeSchemaNode" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-manager", "project.backlogItemTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-manager", "project.backlogItemTypeSchemaEdge" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-manager", "project.backlogItemTypeSchemaNode" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-tester", "project.backlogItemTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-tester", "project.backlogItemTypeSchemaEdge" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-tester", "project.backlogItemTypeSchemaNode" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-tester", "project.backlogItemTypeSchema" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-tester", "project.backlogItemTypeSchemaEdge" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-tester", "project.backlogItemTypeSchemaNode" });
        }
    }
}
