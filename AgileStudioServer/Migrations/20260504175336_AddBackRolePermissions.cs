using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddBackRolePermissions : Migration
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
