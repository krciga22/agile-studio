using Microsoft.EntityFrameworkCore;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypes;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Accounts.ChildBacklogItemTypes;
using AgileStudioServer.Features.Accounts.Workflows;
using AgileStudioServer.Features.Accounts.WorkflowStates;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.RolePermissions;
using AgileStudioServer.Features.Auth.Roles;
using AgileStudioServer.Features.Projects.BacklogItems;
using AgileStudioServer.Features.Projects.Projects;
using AgileStudioServer.Features.Projects.Releases;
using AgileStudioServer.Features.Projects.Sprints;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServer.Data
{
    public class DBContext : DbContext
    {
        public DbSet<Project> Project { get; set; }

        public DbSet<BacklogItem> BacklogItem { get; set; }

        public DbSet<BacklogItemType> BacklogItemType { get; set; }

        public DbSet<BacklogItemTypeSchema> BacklogItemTypeSchema { get; set; }

        public DbSet<BacklogItemLinkTypeSchemaEntry> BacklogItemLinkTypeSchemaEntry { get; set; }

        public DbSet<BacklogItemLinkType> BacklogItemLinkType { get; set; }

        public DbSet<ChildBacklogItemType> ChildBacklogItemType { get; set; }

        public DbSet<BacklogItemLinkTypeSchema> BacklogItemLinkTypeSchema { get; set; }

        public DbSet<Sprint> Sprint { get; set; }

        public DbSet<Release> Release { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<Permission> Permission { get; set; }

        public DbSet<RolePermission> RolePermission { get; set; }

        public DbSet<RoleGrant> RoleGrant { get; set; }

        public DbSet<Workflow> Workflow { get; set; }

        public DbSet<WorkflowState> WorkflowState { get; set; }

        public DBContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureTableNamesAndSchemas(modelBuilder);

            modelBuilder.Entity<Project>()
                .HasOne(e => e.BacklogItemTypeSchema)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_project_backlog_item_type_schema_id");

            modelBuilder.Entity<Project>()
                .HasOne(e => e.BacklogItemLinkTypeSchema)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_project_backlog_item_link_type_schema_id");

            modelBuilder.Entity<BacklogItemLinkTypeSchemaEntry>()
                .HasOne(e => e.BacklogItemLinkTypeSchema)
                .WithMany()
                .HasConstraintName("fk_backlog_item_link_type_schema_entry_link_type_schema_id");

            modelBuilder.Entity<BacklogItemLinkTypeSchemaEntry>()
                .HasIndex(p => p.BacklogItemLinkTypeSchemaID)
                .HasDatabaseName("ix_backlog_item_link_type_schema_entry_link_type_schema_id");

            modelBuilder.Entity<BacklogItemLinkTypeSchemaEntry>()
                .HasOne(e => e.BacklogItemLinkType)
                .WithMany()
                .HasConstraintName("fk_backlog_item_link_type_schema_entry_link_type_id");

            modelBuilder.Entity<BacklogItemLinkTypeSchemaEntry>()
                .HasIndex(p => p.BacklogItemLinkTypeID)
                .HasDatabaseName("ix_backlog_item_link_type_schema_entry_link_type_id");

            modelBuilder.Entity<BacklogItemLinkTypeSchemaEntry>()
                .HasIndex(p => new { p.BacklogItemLinkTypeSchemaID, p.BacklogItemLinkTypeID })
                .IsUnique()
                .HasDatabaseName("ix_backlog_item_link_type_schema_entry_unique");

            modelBuilder.Entity<ChildBacklogItemType>()
                .HasOne(e => e.ChildType)
                .WithMany()
                .HasConstraintName("fk_child_backlog_item_type_child_type_backlog_item_type_id");

            modelBuilder.Entity<ChildBacklogItemType>()
                .HasOne(e => e.ParentType)
                .WithMany()
                .HasConstraintName("fk_child_backlog_item_type_parent_type_backlog_item_type_id");

            modelBuilder.Entity<ChildBacklogItemType>()
                .HasOne(e => e.Schema)
                .WithMany()
                .HasConstraintName("fk_child_backlog_item_type_schema_backlog_item_type_schema_id");

            modelBuilder.Entity<BacklogItem>()
                .HasOne(e => e.BacklogItemType)
                .WithMany()
                .HasConstraintName("fk_backlog_item_backlog_item_type_id");

            modelBuilder.Entity<BacklogItem>()
                .HasOne(e => e.WorkflowState)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_backlog_item_workflow_state_id");

            modelBuilder.Entity<BacklogItem>()
                .HasOne(e => e.ParentBacklogItem)
                .WithMany()
                .HasConstraintName("fk_backlog_item_parent_backlog_item_id");

            modelBuilder.Entity<BacklogItemType>()
                .HasOne(e => e.BacklogItemTypeSchema)
                .WithMany()
                .HasConstraintName("fk_backlog_item_type_backlog_item_type_schema_id");

            modelBuilder.Entity<BacklogItemType>()
                .HasOne(e => e.Workflow)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_backlog_item_type_workflow_workflow_id");

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleKey, rp.PermissionKey })
                .HasName("pk_role_permission");

            modelBuilder.Entity<RolePermission>()
                .HasOne(e => e.Role)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_role_permission_role_key");

            modelBuilder.Entity<RolePermission>()
                .HasIndex(r => new { r.RoleKey })
                .HasDatabaseName("ix_role_permission_role_key");

            modelBuilder.Entity<RolePermission>()
                .HasOne(e => e.Permission)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_role_permission_permission_key");

            modelBuilder.Entity<RolePermission>()
                .HasIndex(r => new { r.PermissionKey })
                .HasDatabaseName("ix_role_permission_permission_key");

            modelBuilder.Entity<RoleGrant>()
                .HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey("RoleKey")
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_role_subject_role_key");

            modelBuilder.Entity<RoleGrant>()
                .HasIndex(r => new { r.Hash })
                .IsUnique()
                .HasDatabaseName("ix_role_grant_hash");

            modelBuilder.Entity<Role>()
                .HasKey(r => r.RoleKey);

            modelBuilder.Entity<Permission>()
                .HasKey(r => r.PermissionKey);

            // todo move this to separate seeding class
            SeedStandardRolesAndPermissions(modelBuilder);
        }

        private void ConfigureTableNamesAndSchemas(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("user", "users");

            modelBuilder.Entity<Role>().ToTable("role", "security");
            modelBuilder.Entity<Permission>().ToTable("permission", "security");
            modelBuilder.Entity<RolePermission>().ToTable("role_permission", "security");
            modelBuilder.Entity<RoleGrant>().ToTable("role_grant", "security");

            modelBuilder.Entity<Project>().ToTable("project", "projects");
            modelBuilder.Entity<BacklogItem>().ToTable("backlog_item", "projects");
            modelBuilder.Entity<Sprint>().ToTable("sprint", "projects");
            modelBuilder.Entity<Release>().ToTable("release", "projects");

            modelBuilder.Entity<BacklogItemType>().ToTable("backlog_item_type", "accounts");
            modelBuilder.Entity<BacklogItemTypeSchema>().ToTable("backlog_item_type_schema", "accounts");
            modelBuilder.Entity<BacklogItemLinkTypeSchemaEntry>().ToTable("backlog_item_link_type_schema_entry", "accounts");
            modelBuilder.Entity<BacklogItemLinkType>().ToTable("backlog_item_link_type", "accounts");
            modelBuilder.Entity<ChildBacklogItemType>().ToTable("child_backlog_item_type", "accounts");
            modelBuilder.Entity<BacklogItemLinkTypeSchema>().ToTable("backlog_item_link_type_schema", "accounts");
            modelBuilder.Entity<Workflow>().ToTable("workflow", "accounts");
            modelBuilder.Entity<WorkflowState>().ToTable("workflow_state", "accounts");
        }

        private void SeedStandardRolesAndPermissions(ModelBuilder modelBuilder)
        {
            SeedProjectRoles(modelBuilder);
            SeedProjectPermissions(modelBuilder);
            SeedProjectAdminRolePermissions(modelBuilder);
            SeedProjectManagerRolePermissions(modelBuilder);
            SeedProjectDeveloperRolePermissions(modelBuilder);
            SeedProjectTesterRolePermissions(modelBuilder);
            SeedProjectBusinessAnalystRolePermissions(modelBuilder);
        }

        private void SeedProjectRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role(RoleKeys.PROJECTS_PROJECT_ADMIN, "Project Admin")
                {
                    Scope = Scopes.PROJECT,
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRole = true
                },
                new Role(RoleKeys.PROJECTS_PROJECT_MANAGER, "Project Manager")
                {
                    Scope = Scopes.PROJECT,
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRole = true
                },
                new Role(RoleKeys.PROJECTS_PROJECT_DEVELOPER, "Developer")
                {
                    Scope = Scopes.PROJECT,
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRole = true
                },
                new Role(RoleKeys.PROJECTS_PROJECT_TESTER, "Tester")
                {
                    Scope = Scopes.PROJECT,
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRole = true
                },
                new Role(RoleKeys.PROJECTS_PROJECT_BUSINESS_ANALYST, "Business Analyst")
                {
                    Scope = Scopes.PROJECT,
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRole = true
                }
            );
        }

        private void SeedProjectPermissions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasData(
                new Permission(PermissionKeys.PROJECTS_PROJECTS_CREATE, "Projects Create")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECTS_READ, "Projects Read")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_READ, "Project Read")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_UPDATE, "Project Update")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_DELETE, "Project Delete")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_MEMBERS_ADD, "Project Members Add")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_MEMBERS_READ, "Project Members Read")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_MEMBER_READ, "Project Member Read")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_MEMBER_REMOVE, "Project Member Remove")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_MEMBER_GRANT_ROLE, "Project Member Grant Role")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_MEMBER_REVOKE_ROLE, "Project Member Revoke Role")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_BACKLOG_ITEMS_CREATE, "Backlog Items Create")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_BACKLOG_ITEMS_READ, "Backlog Items Read")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_BACKLOG_ITEM_READ, "Backlog Item Read")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_BACKLOG_ITEM_UPDATE, "Backlog Item Update")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_BACKLOG_ITEM_DELETE, "Backlog Item Delete")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_RELEASES_CREATE, "Releases Create")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_RELEASES_READ, "Releases Read")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_RELEASE_READ, "Release Read")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_RELEASE_UPDATE, "Release Update")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_RELEASE_DELETE, "Release Delete")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_SPRINTS_CREATE, "Sprints Create")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_SPRINTS_READ, "Sprints Read")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_SPRINT_READ, "Sprint Read")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_SPRINT_UPDATE, "Sprint Update")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_SPRINT_DELETE, "Sprint Delete")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                }
            );
        }

        private void SeedProjectAdminRolePermissions(ModelBuilder modelBuilder)
        {
            var roleKey = RoleKeys.PROJECTS_PROJECT_ADMIN;
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_READ, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_UPDATE, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_DELETE, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBERS_ADD, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBERS_READ, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_READ, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_REMOVE, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_GRANT_ROLE, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_REVOKE_ROLE, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEMS_CREATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEMS_READ, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_READ, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_UPDATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_DELETE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASES_CREATE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASES_READ, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASE_READ, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASE_UPDATE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASE_DELETE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINTS_CREATE, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINTS_READ, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINT_READ, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINT_UPDATE, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINT_DELETE, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                }
            );
        }

        private void SeedProjectManagerRolePermissions(ModelBuilder modelBuilder)
        {
            var roleKey = RoleKeys.PROJECTS_PROJECT_MANAGER;
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_READ, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_UPDATE, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBERS_ADD, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBERS_READ, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_READ, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_REMOVE, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_GRANT_ROLE, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_REVOKE_ROLE, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEMS_CREATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEMS_READ, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_READ, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_UPDATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_DELETE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASES_CREATE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASES_READ, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASE_READ, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASE_UPDATE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASE_DELETE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINTS_CREATE, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINTS_READ, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINT_READ, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINT_UPDATE, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINT_DELETE, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                }
            );
        }

        private void SeedProjectDeveloperRolePermissions(ModelBuilder modelBuilder)
        {
            var roleKey = RoleKeys.PROJECTS_PROJECT_DEVELOPER;
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_READ, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBERS_READ, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_READ, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEMS_CREATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEMS_READ, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_READ, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_UPDATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_DELETE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                }
            );
        }

        private void SeedProjectTesterRolePermissions(ModelBuilder modelBuilder)
        {
            var roleKey = RoleKeys.PROJECTS_PROJECT_TESTER;
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_READ, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBERS_READ, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_READ, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEMS_READ, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_READ, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_UPDATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_DELETE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                }
            );
        }

        private void SeedProjectBusinessAnalystRolePermissions(ModelBuilder modelBuilder)
        {
            var roleKey = RoleKeys.PROJECTS_PROJECT_BUSINESS_ANALYST;
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_READ, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBERS_READ, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_READ, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEMS_READ, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_READ, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_UPDATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_DELETE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                }
            );
        }
    }
}
