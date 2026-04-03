using Microsoft.EntityFrameworkCore;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.ChildBacklogItemTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItems;
using AgileStudioServer.CoreFeatures.Sprints.Sprints;
using AgileStudioServer.CoreFeatures.Releases.Releases;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.CoreFeatures.Workflows.WorkflowStates;
using AgileStudioServer.CoreFeatures.Workflows.Workflows;
using AgileStudioServer.CoreFeatures.Projects.Projects;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServer.CoreFeatures.Auth.Roles;
using AgileStudioServer.CoreFeatures.Auth.Permissions;
using AgileStudioServer.CoreFeatures.Auth.RolePermissions;

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
                .HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(rp => rp.RoleKey)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_role_permission_role_key");

            modelBuilder.Entity<RolePermission>()
                .HasOne(e => e.Permission)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_role_permission_permission_id");

            modelBuilder.Entity<Role>()
                .HasKey(r => r.RoleKey);

            modelBuilder.Entity<Role>()
                .HasIndex(r => new { r.RoleKey })
                .IsUnique()
                .HasDatabaseName("ix_role_rolekey_unique");

            modelBuilder.Entity<Permission>()
                .HasIndex(p => new { p.PermissionKey })
                .IsUnique()
                .HasDatabaseName("ix_permission_permissionkey_unique");
        }
    }
}
