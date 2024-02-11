﻿// <auto-generated />
using System;
using AgileStudioServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgileStudioServer.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20240211070237_Assign-Release-to-Backlog-Item")]
    partial class AssignReleasetoBacklogItem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AgileStudioServer.Models.Entities.BacklogItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("BacklogItemTypeID")
                        .HasColumnType("int")
                        .HasColumnName("backlog_item_type_id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_on");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<int>("ProjectID")
                        .HasColumnType("int")
                        .HasColumnName("project_id");

                    b.Property<int?>("ReleaseID")
                        .HasColumnType("int")
                        .HasColumnName("release_id");

                    b.Property<int?>("SprintID")
                        .HasColumnType("int")
                        .HasColumnName("sprint_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("title");

                    b.HasKey("ID")
                        .HasName("pk_backlog_item");

                    b.HasIndex("BacklogItemTypeID")
                        .HasDatabaseName("ix_backlog_item_backlog_item_type_id");

                    b.HasIndex("ProjectID")
                        .HasDatabaseName("ix_backlog_item_project_id");

                    b.HasIndex("ReleaseID")
                        .HasDatabaseName("ix_backlog_item_release_id");

                    b.HasIndex("SprintID")
                        .HasDatabaseName("ix_backlog_item_sprint_id");

                    b.ToTable("backlog_item", (string)null);
                });

            modelBuilder.Entity("AgileStudioServer.Models.Entities.BacklogItemType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("BacklogItemTypeSchemaID")
                        .HasColumnType("int")
                        .HasColumnName("backlog_item_type_schema_id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_on");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("title");

                    b.HasKey("ID")
                        .HasName("pk_backlog_item_type");

                    b.HasIndex("BacklogItemTypeSchemaID")
                        .HasDatabaseName("ix_backlog_item_type_backlog_item_type_schema_id");

                    b.ToTable("backlog_item_type", (string)null);
                });

            modelBuilder.Entity("AgileStudioServer.Models.Entities.BacklogItemTypeSchema", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_on");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("title");

                    b.HasKey("ID")
                        .HasName("pk_backlog_item_type_schema");

                    b.ToTable("backlog_item_type_schema", (string)null);
                });

            modelBuilder.Entity("AgileStudioServer.Models.Entities.Project", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("BacklogItemTypeSchemaID")
                        .HasColumnType("int")
                        .HasColumnName("backlog_item_type_schema_id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_on");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("title");

                    b.HasKey("ID")
                        .HasName("pk_project");

                    b.HasIndex("BacklogItemTypeSchemaID")
                        .HasDatabaseName("ix_project_backlog_item_type_schema_id");

                    b.ToTable("project", (string)null);
                });

            modelBuilder.Entity("AgileStudioServer.Models.Entities.Release", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_on");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("end_date");

                    b.Property<int>("ProjectID")
                        .HasColumnType("int")
                        .HasColumnName("project_id");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("start_date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("title");

                    b.HasKey("ID")
                        .HasName("pk_release");

                    b.HasIndex("ProjectID")
                        .HasDatabaseName("ix_release_project_id");

                    b.ToTable("release", (string)null);
                });

            modelBuilder.Entity("AgileStudioServer.Models.Entities.Sprint", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_on");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("end_date");

                    b.Property<int>("ProjectID")
                        .HasColumnType("int")
                        .HasColumnName("project_id");

                    b.Property<int>("SprintNumber")
                        .HasColumnType("int")
                        .HasColumnName("sprint_number");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("start_date");

                    b.HasKey("ID")
                        .HasName("pk_sprint");

                    b.HasIndex("ProjectID")
                        .HasDatabaseName("ix_sprint_project_id");

                    b.ToTable("sprint", (string)null);
                });

            modelBuilder.Entity("AgileStudioServer.Models.Entities.BacklogItem", b =>
                {
                    b.HasOne("AgileStudioServer.Models.Entities.BacklogItemType", "BacklogItemType")
                        .WithMany()
                        .HasForeignKey("BacklogItemTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_backlog_item_backlog_item_type_id");

                    b.HasOne("AgileStudioServer.Models.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_backlog_item_project_project_id");

                    b.HasOne("AgileStudioServer.Models.Entities.Release", "Release")
                        .WithMany()
                        .HasForeignKey("ReleaseID")
                        .HasConstraintName("fk_backlog_item_release_release_id");

                    b.HasOne("AgileStudioServer.Models.Entities.Sprint", "Sprint")
                        .WithMany()
                        .HasForeignKey("SprintID")
                        .HasConstraintName("fk_backlog_item_sprint_sprint_id");

                    b.Navigation("BacklogItemType");

                    b.Navigation("Project");

                    b.Navigation("Release");

                    b.Navigation("Sprint");
                });

            modelBuilder.Entity("AgileStudioServer.Models.Entities.BacklogItemType", b =>
                {
                    b.HasOne("AgileStudioServer.Models.Entities.BacklogItemTypeSchema", "BacklogItemTypeSchema")
                        .WithMany()
                        .HasForeignKey("BacklogItemTypeSchemaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_backlog_item_type_backlog_item_type_schema_id");

                    b.Navigation("BacklogItemTypeSchema");
                });

            modelBuilder.Entity("AgileStudioServer.Models.Entities.Project", b =>
                {
                    b.HasOne("AgileStudioServer.Models.Entities.BacklogItemTypeSchema", "BacklogItemTypeSchema")
                        .WithMany()
                        .HasForeignKey("BacklogItemTypeSchemaID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_project_backlog_item_type_schema_id");

                    b.Navigation("BacklogItemTypeSchema");
                });

            modelBuilder.Entity("AgileStudioServer.Models.Entities.Release", b =>
                {
                    b.HasOne("AgileStudioServer.Models.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_release_project_project_id");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("AgileStudioServer.Models.Entities.Sprint", b =>
                {
                    b.HasOne("AgileStudioServer.Models.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sprint_project_project_id");

                    b.Navigation("Project");
                });
#pragma warning restore 612, 618
        }
    }
}
