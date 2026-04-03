using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AddStandardProjectRolesAndPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "permission_key", "created_by_id", "created_on", "description", "scope", "title" },
                values: new object[,]
                {
                    { "projects-backlog-item-delete", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Backlog Item Delete" },
                    { "projects-backlog-item-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Backlog Item Read" },
                    { "projects-backlog-item-update", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Backlog Item Update" },
                    { "projects-backlog-items-create", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Backlog Items Create" },
                    { "projects-backlog-items-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Backlog Items Read" },
                    { "projects-project-delete", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Project Delete" },
                    { "projects-project-member-grant-role", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Project Member Grant Role" },
                    { "projects-project-member-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Project Member Read" },
                    { "projects-project-member-remove", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Project Member Remove" },
                    { "projects-project-member-revoke-role", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Project Member Revoke Role" },
                    { "projects-project-members-add", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Project Members Add" },
                    { "projects-project-members-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Project Members Read" },
                    { "projects-project-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Project Read" },
                    { "projects-project-update", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Project Update" },
                    { "projects-projects-create", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Projects Create" },
                    { "projects-projects-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Projects Read" },
                    { "projects-release-delete", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Release Delete" },
                    { "projects-release-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Release Read" },
                    { "projects-release-update", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Release Update" },
                    { "projects-releases-create", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Releases Create" },
                    { "projects-releases-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Releases Read" },
                    { "projects-sprint-delete", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Sprint Delete" },
                    { "projects-sprint-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Sprint Read" },
                    { "projects-sprint-update", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Sprint Update" },
                    { "projects-sprints-create", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Sprints Create" },
                    { "projects-sprints-read", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", "Sprints Read" }
                });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "role_key", "created_by_id", "created_on", "description", "scope", "scope_id", "title" },
                values: new object[,]
                {
                    { "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", null, "Project Admin" },
                    { "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", null, "Business Analyst" },
                    { "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", null, "Developer" },
                    { "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", null, "Project Manager" },
                    { "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "projects", null, "Tester" }
                });

            migrationBuilder.InsertData(
                table: "role_permission",
                columns: new[] { "permission_key", "role_key", "created_by_id", "created_on" },
                values: new object[,]
                {
                    { "projects-backlog-item-delete", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-item-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-item-update", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-items-create", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-items-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-delete", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-member-grant-role", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-member-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-member-remove", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-member-revoke-role", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-members-add", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-members-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-update", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-release-delete", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-release-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-release-update", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-releases-create", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-releases-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-sprint-delete", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-sprint-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-sprint-update", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-sprints-create", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-sprints-read", "projects-project-admin", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-item-delete", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-item-read", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-item-update", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-items-read", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-member-read", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-members-read", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-read", "projects-project-business-analyst", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-item-delete", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-item-read", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-item-update", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-items-create", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-items-read", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-member-read", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-members-read", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-read", "projects-project-developer", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-item-delete", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-item-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-item-update", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-items-create", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-items-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-member-grant-role", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-member-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-member-remove", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-member-revoke-role", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-members-add", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-members-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-update", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-release-delete", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-release-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-release-update", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-releases-create", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-releases-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-sprint-delete", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-sprint-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-sprint-update", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-sprints-create", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-sprints-read", "projects-project-manager", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-item-delete", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-item-read", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-item-update", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-backlog-items-read", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-member-read", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-members-read", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "projects-project-read", "projects-project-tester", null, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-projects-create");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-projects-read");

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-create", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-delete", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-grant-role", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-remove", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-revoke-role", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-add", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-update", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-delete", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-update", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-releases-create", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-releases-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-delete", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-update", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprints-create", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprints-read", "projects-project-admin" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-business-analyst" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-business-analyst" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-business-analyst" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-business-analyst" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-business-analyst" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-business-analyst" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-read", "projects-project-business-analyst" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-developer" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-developer" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-developer" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-create", "projects-project-developer" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-developer" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-developer" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-developer" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-read", "projects-project-developer" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-create", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-grant-role", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-remove", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-revoke-role", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-add", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-update", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-delete", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-release-update", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-releases-create", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-releases-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-delete", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprint-update", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprints-create", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-sprints-read", "projects-project-manager" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-delete", "projects-project-tester" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-read", "projects-project-tester" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-item-update", "projects-project-tester" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-backlog-items-read", "projects-project-tester" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-member-read", "projects-project-tester" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-members-read", "projects-project-tester" });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "permission_key", "role_key" },
                keyValues: new object[] { "projects-project-read", "projects-project-tester" });

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-item-delete");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-item-read");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-item-update");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-items-create");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-backlog-items-read");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-delete");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-grant-role");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-read");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-remove");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-member-revoke-role");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-members-add");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-members-read");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-read");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-project-update");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-release-delete");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-release-read");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-release-update");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-releases-create");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-releases-read");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprint-delete");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprint-read");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprint-update");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprints-create");

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "permission_key",
                keyValue: "projects-sprints-read");

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-admin");

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-business-analyst");

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-developer");

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-manager");

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "role_key",
                keyValue: "projects-project-tester");
        }
    }
}
