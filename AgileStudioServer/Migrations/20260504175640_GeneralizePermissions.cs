using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class GeneralizePermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-projects-create");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-projects-read");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-admin", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-admin", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-admin", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-items-create", "projects-project-admin", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-admin", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-delete", "projects-project-admin", "project" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-member-grant-role", "projects-project-admin", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-admin", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-member-remove", "projects-project-admin", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-member-revoke-role", "projects-project-admin", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-members-add", "projects-project-admin", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-admin", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-read", "projects-project-admin", "project" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-update", "projects-project-admin", "project" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-release-delete", "projects-project-admin", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-release-read", "projects-project-admin", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-release-update", "projects-project-admin", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-releases-create", "projects-project-admin", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-releases-read", "projects-project-admin", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-sprint-delete", "projects-project-admin", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-sprint-read", "projects-project-admin", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-sprint-update", "projects-project-admin", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-sprints-create", "projects-project-admin", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-sprints-read", "projects-project-admin", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-business-analyst", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-business-analyst", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-business-analyst", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-business-analyst", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-business-analyst", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-business-analyst", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-read", "projects-project-business-analyst", "project" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-developer", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-developer", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-developer", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-items-create", "projects-project-developer", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-developer", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-developer", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-developer", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-read", "projects-project-developer", "project" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-manager", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-manager", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-manager", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-items-create", "projects-project-manager", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-manager", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-member-grant-role", "projects-project-manager", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-manager", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-member-remove", "projects-project-manager", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-member-revoke-role", "projects-project-manager", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-members-add", "projects-project-manager", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-manager", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-read", "projects-project-manager", "project" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-update", "projects-project-manager", "project" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-release-delete", "projects-project-manager", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-release-read", "projects-project-manager", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-release-update", "projects-project-manager", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-releases-create", "projects-project-manager", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-releases-read", "projects-project-manager", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-sprint-delete", "projects-project-manager", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-sprint-read", "projects-project-manager", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-sprint-update", "projects-project-manager", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-sprints-create", "projects-project-manager", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-sprints-read", "projects-project-manager", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-tester", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-tester", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-tester", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-tester", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-tester", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-tester", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "projects-project-read", "projects-project-tester", "project" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-item-delete");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-item-read");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-item-update");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-items-create");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-items-read");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-delete");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-grant-role");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-read");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-remove");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-revoke-role");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-members-add");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-members-read");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-read");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-update");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-release-delete");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-release-read");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-release-update");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-releases-create");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-releases-read");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprint-delete");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprint-read");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprint-update");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprints-create");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprints-read");

            migrationBuilder.InsertData(
                schema: "security",
                table: "permission",
                columns: new[] { "permission_key", "created_by_id", "created_on", "description", "is_system_permission", "title" },
                values: new object[,]
                {
                    { "bulk-delete", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Bulk Delete" },
                    { "bulk-update", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Bulk Update" },
                    { "create", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Create" },
                    { "delete", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Delete" },
                    { "list", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "List" },
                    { "read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Read" },
                    { "update", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Update" }
                });

            migrationBuilder.InsertData(
                schema: "security",
                table: "role_permission",
                columns: new[] { "permission_key", "role_key", "scope", "created_by_id", "created_on", "is_system_role_permission" },
                values: new object[,]
                {
                    { "create", "projects-project-admin", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-admin", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-admin", "project.memberRole", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-admin", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-admin", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-admin", "project", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-admin", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-admin", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-admin", "project.memberRole", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-admin", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-admin", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-admin", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-admin", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-admin", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-admin", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-admin", "project", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-admin", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-admin", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-admin", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-admin", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "projects-project-admin", "project", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "projects-project-admin", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "projects-project-admin", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "projects-project-admin", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-business-analyst", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-business-analyst", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-business-analyst", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-business-analyst", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-business-analyst", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-business-analyst", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-business-analyst", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-business-analyst", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-business-analyst", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-business-analyst", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-business-analyst", "project", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-business-analyst", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-business-analyst", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-business-analyst", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-business-analyst", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "projects-project-business-analyst", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "projects-project-business-analyst", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "projects-project-business-analyst", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-developer", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-developer", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-developer", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-developer", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-developer", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-developer", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-developer", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-developer", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-developer", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-developer", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-developer", "project", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-developer", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-developer", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-developer", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-developer", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "projects-project-developer", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "projects-project-developer", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "projects-project-developer", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-manager", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-manager", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-manager", "project.memberRole", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-manager", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-manager", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-manager", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-manager", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-manager", "project.memberRole", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-manager", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-manager", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-manager", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-manager", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-manager", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-manager", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-manager", "project", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-manager", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-manager", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-manager", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-manager", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "projects-project-manager", "project", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "projects-project-manager", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "projects-project-manager", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "projects-project-manager", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-tester", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-tester", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "create", "projects-project-tester", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-tester", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-tester", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "delete", "projects-project-tester", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-tester", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-tester", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-tester", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "list", "projects-project-tester", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-tester", "project", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-tester", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-tester", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-tester", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "read", "projects-project-tester", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "projects-project-tester", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "projects-project-tester", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "update", "projects-project-tester", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "bulk-delete");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "bulk-update");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-admin", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-admin", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-admin", "project.memberRole" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-admin", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-admin", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-admin", "project" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-admin", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-admin", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-admin", "project.memberRole" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-admin", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-admin", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-admin", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-admin", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-admin", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-admin", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-admin", "project" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-admin", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-admin", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-admin", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-admin", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "projects-project-admin", "project" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "projects-project-admin", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "projects-project-admin", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "projects-project-admin", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-business-analyst", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-business-analyst", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-business-analyst", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-business-analyst", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-business-analyst", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-business-analyst", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-business-analyst", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-business-analyst", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-business-analyst", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-business-analyst", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-business-analyst", "project" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-business-analyst", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-business-analyst", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-business-analyst", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-business-analyst", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "projects-project-business-analyst", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "projects-project-business-analyst", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "projects-project-business-analyst", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-developer", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-developer", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-developer", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-developer", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-developer", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-developer", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-developer", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-developer", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-developer", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-developer", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-developer", "project" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-developer", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-developer", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-developer", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-developer", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "projects-project-developer", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "projects-project-developer", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "projects-project-developer", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-manager", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-manager", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-manager", "project.memberRole" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-manager", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-manager", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-manager", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-manager", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-manager", "project.memberRole" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-manager", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-manager", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-manager", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-manager", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-manager", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-manager", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-manager", "project" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-manager", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-manager", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-manager", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-manager", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "projects-project-manager", "project" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "projects-project-manager", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "projects-project-manager", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "projects-project-manager", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-tester", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-tester", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "create", "projects-project-tester", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-tester", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-tester", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "delete", "projects-project-tester", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-tester", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-tester", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-tester", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "list", "projects-project-tester", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-tester", "project" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-tester", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-tester", "project.member" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-tester", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "read", "projects-project-tester", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "projects-project-tester", "project.backlogItem" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "projects-project-tester", "project.release" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key", "scope" },
                keyValues: new object[] { "update", "projects-project-tester", "project.sprint" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "create");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "delete");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "list");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "read");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "update");

            migrationBuilder.InsertData(
                schema: "security",
                table: "permission",
                columns: new[] { "permission_key", "created_by_id", "created_on", "description", "is_system_permission", "title" },
                values: new object[,]
                {
                    { "projects-backlog-item-delete", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Backlog Item Delete" },
                    { "projects-backlog-item-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Backlog Item Read" },
                    { "projects-backlog-item-update", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Backlog Item Update" },
                    { "projects-backlog-items-create", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Backlog Items Create" },
                    { "projects-backlog-items-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Backlog Items Read" },
                    { "projects-project-delete", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Project Delete" },
                    { "projects-project-member-grant-role", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Project Member Grant Role" },
                    { "projects-project-member-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Project Member Read" },
                    { "projects-project-member-remove", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Project Member Remove" },
                    { "projects-project-member-revoke-role", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Project Member Revoke Role" },
                    { "projects-project-members-add", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Project Members Add" },
                    { "projects-project-members-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Project Members Read" },
                    { "projects-project-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Project Read" },
                    { "projects-project-update", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Project Update" },
                    { "projects-projects-create", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Projects Create" },
                    { "projects-projects-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Projects Read" },
                    { "projects-release-delete", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Release Delete" },
                    { "projects-release-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Release Read" },
                    { "projects-release-update", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Release Update" },
                    { "projects-releases-create", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Releases Create" },
                    { "projects-releases-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Releases Read" },
                    { "projects-sprint-delete", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Sprint Delete" },
                    { "projects-sprint-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Sprint Read" },
                    { "projects-sprint-update", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Sprint Update" },
                    { "projects-sprints-create", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Sprints Create" },
                    { "projects-sprints-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, true, "Sprints Read" }
                });

            migrationBuilder.InsertData(
                schema: "security",
                table: "role_permission",
                columns: new[] { "permission_key", "role_key", "scope", "created_by_id", "created_on", "is_system_role_permission" },
                values: new object[,]
                {
                    { "projects-backlog-item-delete", "projects-project-admin", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-read", "projects-project-admin", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-update", "projects-project-admin", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-items-create", "projects-project-admin", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-items-read", "projects-project-admin", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-delete", "projects-project-admin", "project", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-grant-role", "projects-project-admin", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-read", "projects-project-admin", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-remove", "projects-project-admin", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-revoke-role", "projects-project-admin", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-members-add", "projects-project-admin", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-members-read", "projects-project-admin", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-read", "projects-project-admin", "project", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-update", "projects-project-admin", "project", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-release-delete", "projects-project-admin", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-release-read", "projects-project-admin", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-release-update", "projects-project-admin", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-releases-create", "projects-project-admin", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-releases-read", "projects-project-admin", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprint-delete", "projects-project-admin", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprint-read", "projects-project-admin", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprint-update", "projects-project-admin", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprints-create", "projects-project-admin", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprints-read", "projects-project-admin", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-delete", "projects-project-business-analyst", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-read", "projects-project-business-analyst", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-update", "projects-project-business-analyst", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-items-read", "projects-project-business-analyst", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-read", "projects-project-business-analyst", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-members-read", "projects-project-business-analyst", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-read", "projects-project-business-analyst", "project", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-delete", "projects-project-developer", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-read", "projects-project-developer", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-update", "projects-project-developer", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-items-create", "projects-project-developer", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-items-read", "projects-project-developer", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-read", "projects-project-developer", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-members-read", "projects-project-developer", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-read", "projects-project-developer", "project", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-delete", "projects-project-manager", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-read", "projects-project-manager", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-update", "projects-project-manager", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-items-create", "projects-project-manager", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-items-read", "projects-project-manager", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-grant-role", "projects-project-manager", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-read", "projects-project-manager", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-remove", "projects-project-manager", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-revoke-role", "projects-project-manager", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-members-add", "projects-project-manager", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-members-read", "projects-project-manager", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-read", "projects-project-manager", "project", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-update", "projects-project-manager", "project", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-release-delete", "projects-project-manager", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-release-read", "projects-project-manager", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-release-update", "projects-project-manager", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-releases-create", "projects-project-manager", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-releases-read", "projects-project-manager", "project.release", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprint-delete", "projects-project-manager", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprint-read", "projects-project-manager", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprint-update", "projects-project-manager", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprints-create", "projects-project-manager", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-sprints-read", "projects-project-manager", "project.sprint", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-delete", "projects-project-tester", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-read", "projects-project-tester", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-item-update", "projects-project-tester", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-backlog-items-read", "projects-project-tester", "project.backlogItem", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-member-read", "projects-project-tester", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-members-read", "projects-project-tester", "project.member", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true },
                    { "projects-project-read", "projects-project-tester", "project", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true }
                });
        }
    }
}
