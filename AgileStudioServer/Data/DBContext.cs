using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.AccountTypes;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypes;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEntries;
using AgileStudioServer.Features.Accounts.Workflows;
using AgileStudioServer.Features.Accounts.WorkflowStates;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.RolePermissions;
using AgileStudioServer.Features.Auth.Roles;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Projects.BacklogItems;
using AgileStudioServer.Features.Projects.Projects;
using AgileStudioServer.Features.Projects.Releases;
using AgileStudioServer.Features.Projects.Sprints;
using AgileStudioServer.Features.Users.Users;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.Data
{
    public class DBContext : DbContext
    {
        public DbSet<Account> Account { get; set; }

        public DbSet<AccountType> AccountType { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<BacklogItem> BacklogItem { get; set; }

        public DbSet<BacklogItemType> BacklogItemType { get; set; }

        public DbSet<BacklogItemTypeSchema> BacklogItemTypeSchema { get; set; }

        public DbSet<BacklogItemLinkTypeSchemaEntry> BacklogItemLinkTypeSchemaEntry { get; set; }

        public DbSet<BacklogItemLinkType> BacklogItemLinkType { get; set; }

        public DbSet<BacklogItemTypeSchemaEntry> BacklogItemTypeSchemaEntry { get; set; }

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

            modelBuilder.Entity<Account>()
                .HasOne(e => e.AccountType)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_account_account_type_id");

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

            modelBuilder.Entity<BacklogItemTypeSchemaEntry>()
                .HasOne(e => e.ChildType)
                .WithMany()
                .HasConstraintName("fk_backlog_item_type_schema_entry_child_type");

            modelBuilder.Entity<BacklogItemTypeSchemaEntry>()
                .HasOne(e => e.ParentType)
                .WithMany()
                .HasConstraintName("fk_backlog_item_type_schema_entry_parent_type");

            modelBuilder.Entity<BacklogItemTypeSchemaEntry>()
                .HasOne(e => e.Schema)
                .WithMany()
                .HasConstraintName("fk_backlog_item_type_schema_entry_schema");

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
                .HasOne(e => e.Workflow)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_backlog_item_type_workflow_workflow_id");

            modelBuilder.Entity<RolePermission>()
                .Property(rp => rp.Scope)
                .HasDefaultValue(Scopes.GLOBAL);

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleKey, rp.PermissionKey, rp.Scope }) // todo add scope
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

            modelBuilder.Entity<RoleGrant>()
                .Property(rg => rg.Scope)
                .HasDefaultValue(Scopes.GLOBAL);

            modelBuilder.Entity<Role>()
                .HasKey(r => r.RoleKey);

            modelBuilder.Entity<Role>()
                .Property(r => r.Scope)
                .HasDefaultValue(Scopes.GLOBAL);

            modelBuilder.Entity<Permission>()
                .HasKey(r => r.PermissionKey);

            // todo move this to separate seeding class
            SeedStandardTypes(modelBuilder);

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

            modelBuilder.Entity<Account>().ToTable("account", "accounts");
            modelBuilder.Entity<AccountType>().ToTable("account_type", "accounts");
            modelBuilder.Entity<BacklogItemType>().ToTable("backlog_item_type", "accounts");
            modelBuilder.Entity<BacklogItemTypeSchema>().ToTable("backlog_item_type_schema", "accounts");
            modelBuilder.Entity<BacklogItemLinkTypeSchemaEntry>().ToTable("backlog_item_link_type_schema_entry", "accounts");
            modelBuilder.Entity<BacklogItemLinkType>().ToTable("backlog_item_link_type", "accounts");
            modelBuilder.Entity<BacklogItemTypeSchemaEntry>().ToTable("backlog_item_type_schema_entry", "accounts");
            modelBuilder.Entity<BacklogItemLinkTypeSchema>().ToTable("backlog_item_link_type_schema", "accounts");
            modelBuilder.Entity<Workflow>().ToTable("workflow", "accounts");
            modelBuilder.Entity<WorkflowState>().ToTable("workflow_state", "accounts");
        }

        private void SeedStandardTypes(ModelBuilder modelBuilder)
        {
            SeedStandardAccountTypes(modelBuilder);
        }

        private void SeedStandardAccountTypes(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<AccountType>().HasData(
                    new AccountType("Individual")
                    {
                        ID = AccountTypes.INDIVIDUAL,
                        CreatedOn = new DateTime(2026, 5, 29, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new AccountType("Organization")
                    {
                        ID = AccountTypes.ORGANIZATION,
                        CreatedOn = new DateTime(2026, 5, 29, 0, 0, 0, DateTimeKind.Utc),
                    },
                    new AccountType("Business")
                    {
                        ID = AccountTypes.BUSINESS,
                        CreatedOn = new DateTime(2026, 5, 29, 0, 0, 0, DateTimeKind.Utc),
                    }
                );
        }

        private void SeedStandardRolesAndPermissions(ModelBuilder modelBuilder)
        {
            SeedPermissions(modelBuilder);
            SeedAccountRoles(modelBuilder);
            SeedAccountOwnerRolePermissions(modelBuilder);
            SeedProjectRoles(modelBuilder);
            SeedProjectAdminRolePermissions(modelBuilder);
            SeedProjectManagerRolePermissions(modelBuilder);
            SeedProjectDeveloperRolePermissions(modelBuilder);
            SeedProjectTesterRolePermissions(modelBuilder);
            SeedProjectBusinessAnalystRolePermissions(modelBuilder);
        }

        private void SeedAccountRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role(RoleKeys.ACCOUNTS_ACCOUNT_OWNER, "Account Owner", Scopes.ACCOUNT)
                {
                    CreatedOn = new DateTime(2025, 5, 30, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRole = true
                }
            );
        }

        private void SeedAccountOwnerRolePermissions(ModelBuilder modelBuilder)
        {
            var roleKey = RoleKeys.ACCOUNTS_ACCOUNT_OWNER;
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.ACCOUNT)
                {
                    CreatedOn = new DateTime(2025, 5, 30, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.ACCOUNT)
                {
                    CreatedOn = new DateTime(2025, 5, 30, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.ACCOUNT)
                {
                    CreatedOn = new DateTime(2025, 5, 30, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2025, 6, 7, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2025, 6, 7, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.ACCOUNT_BACKLOG_ITEM_TYPE)
                {
                    CreatedOn = new DateTime(2025, 6, 13, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.ACCOUNT_BACKLOG_ITEM_TYPE)
                {
                    CreatedOn = new DateTime(2025, 6, 13, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.ACCOUNT_BACKLOG_ITEM_TYPE)
                {
                    CreatedOn = new DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.ACCOUNT_BACKLOG_ITEM_TYPE)
                {
                    CreatedOn = new DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.ACCOUNT_BACKLOG_ITEM_TYPE)
                {
                    CreatedOn = new DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                }
            );
        }

        private void SeedProjectRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role(RoleKeys.PROJECTS_PROJECT_ADMIN, "Project Admin", Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRole = true
                },
                new Role(RoleKeys.PROJECTS_PROJECT_MANAGER, "Project Manager", Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRole = true
                },
                new Role(RoleKeys.PROJECTS_PROJECT_DEVELOPER, "Developer", Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRole = true
                },
                new Role(RoleKeys.PROJECTS_PROJECT_TESTER, "Tester", Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRole = true
                },
                new Role(RoleKeys.PROJECTS_PROJECT_BUSINESS_ANALYST, "Business Analyst", Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRole = true
                }
            );
        }

        private void SeedPermissions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasData(
                new Permission(PermissionKeys.CREATE, "Create")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.LIST, "List")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.BULK_UPDATE, "Bulk Update")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.BULK_DELETE, "Bulk Delete")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.READ, "Read")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.UPDATE, "Update")
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemPermission = true
                },
                new Permission(PermissionKeys.DELETE, "Delete")
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
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_MEMBER_ROLE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_MEMBER_ROLE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_SPRINT)
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
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_MEMBER_ROLE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_MEMBER_ROLE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_SPRINT)
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
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_SPRINT)
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
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_SPRINT)
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
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_MEMBER)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_BACKLOG_ITEM)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_RELEASE)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.CREATE, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.LIST, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.READ, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.UPDATE, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                },
                new RolePermission(roleKey, PermissionKeys.DELETE, Scopes.PROJECT_SPRINT)
                {
                    CreatedOn = new DateTime(2024, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                    IsSystemRolePermission = true
                }
            );
        }
    }
}
