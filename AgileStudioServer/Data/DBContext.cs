using Microsoft.EntityFrameworkCore;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.ChildBacklogItemTypes;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.CoreFeatures.Workflows.WorkflowStates;
using AgileStudioServer.CoreFeatures.Workflows.Workflows;
using AgileStudioServer.CoreFeatures.Projects.Projects;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServer.CoreFeatures.Auth.Roles;
using AgileStudioServer.CoreFeatures.Auth.Permissions;
using AgileStudioServer.CoreFeatures.Auth.RolePermissions;
using AgileStudioServer.CoreFeatures.Auth.RoleGrants;
using AgileStudioServer.CoreFeatures.Projects.Releases;
using AgileStudioServer.CoreFeatures.Projects.Sprints;
using AgileStudioServer.CoreFeatures.Projects.BacklogItems;

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
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRole = true
                },
                new Role(RoleKeys.PROJECTS_PROJECT_MANAGER, "Project Manager")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRole = true
                },
                new Role(RoleKeys.PROJECTS_PROJECT_DEVELOPER, "Developer")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRole = true
                },
                new Role(RoleKeys.PROJECTS_PROJECT_TESTER, "Tester")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRole = true
                },
                new Role(RoleKeys.PROJECTS_PROJECT_BUSINESS_ANALYST, "Business Analyst")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRole = true
                }
            );
        }

        private void SeedProjectPermissions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasData(
                new Permission(PermissionKeys.PROJECTS_PROJECTS_CREATE, "Projects Create")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECTS_READ, "Projects Read")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_READ, "Project Read")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_UPDATE, "Project Update")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_DELETE, "Project Delete")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_MEMBERS_ADD, "Project Members Add")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_MEMBERS_READ, "Project Members Read")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_MEMBER_READ, "Project Member Read")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_MEMBER_REMOVE, "Project Member Remove")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_MEMBER_GRANT_ROLE, "Project Member Grant Role")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_PROJECT_MEMBER_REVOKE_ROLE, "Project Member Revoke Role")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_BACKLOG_ITEMS_CREATE, "Backlog Items Create")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_BACKLOG_ITEMS_READ, "Backlog Items Read")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_BACKLOG_ITEM_READ, "Backlog Item Read")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_BACKLOG_ITEM_UPDATE, "Backlog Item Update")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_BACKLOG_ITEM_DELETE, "Backlog Item Delete")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_RELEASES_CREATE, "Releases Create")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_RELEASES_READ, "Releases Read")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_RELEASE_READ, "Release Read")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_RELEASE_UPDATE, "Release Update")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_RELEASE_DELETE, "Release Delete")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_SPRINTS_CREATE, "Sprints Create")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_SPRINTS_READ, "Sprints Read")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_SPRINT_READ, "Sprint Read")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_SPRINT_UPDATE, "Sprint Update")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.PROJECTS_SPRINT_DELETE, "Sprint Delete")
                {
                    Scope = PermissionScopes.PROJECTS,
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemPermission = true
                }
            );
        }

        private void SeedProjectAdminRolePermissions(ModelBuilder modelBuilder)
        {
            var roleKey = RoleKeys.PROJECTS_PROJECT_ADMIN;
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_UPDATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_DELETE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBERS_ADD)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBERS_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_REMOVE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_GRANT_ROLE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_REVOKE_ROLE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEMS_CREATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEMS_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_UPDATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_DELETE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASES_CREATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASES_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASE_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASE_UPDATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASE_DELETE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINTS_CREATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINTS_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINT_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINT_UPDATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINT_DELETE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                }
            );
        }

        private void SeedProjectManagerRolePermissions(ModelBuilder modelBuilder)
        {
            var roleKey = RoleKeys.PROJECTS_PROJECT_MANAGER;
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_UPDATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBERS_ADD)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBERS_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_REMOVE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_GRANT_ROLE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_REVOKE_ROLE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEMS_CREATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEMS_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_UPDATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_DELETE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASES_CREATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASES_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASE_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASE_UPDATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_RELEASE_DELETE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINTS_CREATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINTS_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINT_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINT_UPDATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_SPRINT_DELETE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                }
            );
        }

        private void SeedProjectDeveloperRolePermissions(ModelBuilder modelBuilder)
        {
            var roleKey = RoleKeys.PROJECTS_PROJECT_DEVELOPER;
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBERS_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEMS_CREATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEMS_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_UPDATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_DELETE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                }
            );
        }

        private void SeedProjectTesterRolePermissions(ModelBuilder modelBuilder)
        {
            var roleKey = RoleKeys.PROJECTS_PROJECT_TESTER;
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBERS_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEMS_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_UPDATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_DELETE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                }
            );
        }

        private void SeedProjectBusinessAnalystRolePermissions(ModelBuilder modelBuilder)
        {
            var roleKey = RoleKeys.PROJECTS_PROJECT_BUSINESS_ANALYST;
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBERS_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_PROJECT_MEMBER_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEMS_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_READ)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_UPDATE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.PROJECTS_BACKLOG_ITEM_DELETE)
                {
                    CreatedOn = new DateTime(2024, 4, 3),
                    IsSystemRolePermission = true
                }
            );
        }
    }
}
