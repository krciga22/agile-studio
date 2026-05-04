using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRolePermissionsTemporarily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-create", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-delete", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-grant-role", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-remove", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-revoke-role", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-add", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-update", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-delete", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-update", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-releases-create", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-releases-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-delete", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-update", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprints-create", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprints-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-business-analyst" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-business-analyst" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-business-analyst" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-business-analyst" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-business-analyst" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-business-analyst" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-read", "projects-project-business-analyst" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-developer" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-developer" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-developer" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-create", "projects-project-developer" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-developer" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-developer" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-developer" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-read", "projects-project-developer" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-create", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-grant-role", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-remove", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-revoke-role", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-add", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-update", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-delete", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-update", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-releases-create", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-releases-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-delete", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-update", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprints-create", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprints-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-tester" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-tester" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-tester" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-tester" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-tester" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-tester" });

            migrationBuilder.DeleteData(
                schema: "security",
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-read", "projects-project-tester" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "security",
                table: "role_permission",
                columns: new[] { "permission_key", "role_key", "created_by_id", "created_on", "is_system_role_permission", "scope" },
                values: new object[,]
                {
                    { "projects-backlog-item-delete", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-item-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-item-update", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-items-create", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-items-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-project-delete", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project" },
                    { "projects-project-member-grant-role", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-member-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-member-remove", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-member-revoke-role", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-members-add", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-members-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project" },
                    { "projects-project-update", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project" },
                    { "projects-release-delete", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.release" },
                    { "projects-release-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.release" },
                    { "projects-release-update", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.release" },
                    { "projects-releases-create", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.release" },
                    { "projects-releases-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.release" },
                    { "projects-sprint-delete", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.sprint" },
                    { "projects-sprint-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.sprint" },
                    { "projects-sprint-update", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.sprint" },
                    { "projects-sprints-create", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.sprint" },
                    { "projects-sprints-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.sprint" },
                    { "projects-backlog-item-delete", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-item-read", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-item-update", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-items-read", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-project-member-read", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-members-read", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-read", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project" },
                    { "projects-backlog-item-delete", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-item-read", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-item-update", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-items-create", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-items-read", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-project-member-read", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-members-read", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-read", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project" },
                    { "projects-backlog-item-delete", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-item-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-item-update", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-items-create", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-items-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-project-member-grant-role", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-member-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-member-remove", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-member-revoke-role", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-members-add", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-members-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project" },
                    { "projects-project-update", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project" },
                    { "projects-release-delete", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.release" },
                    { "projects-release-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.release" },
                    { "projects-release-update", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.release" },
                    { "projects-releases-create", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.release" },
                    { "projects-releases-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.release" },
                    { "projects-sprint-delete", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.sprint" },
                    { "projects-sprint-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.sprint" },
                    { "projects-sprint-update", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.sprint" },
                    { "projects-sprints-create", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.sprint" },
                    { "projects-sprints-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.sprint" },
                    { "projects-backlog-item-delete", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-item-read", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-item-update", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-backlog-items-read", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.backlogItem" },
                    { "projects-project-member-read", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-members-read", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project.member" },
                    { "projects-project-read", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Utc), true, "project" }
                });
        }
    }
}
