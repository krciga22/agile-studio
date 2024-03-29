﻿using AgileStudioServer;
using AgileStudioServer.Models.Entities;

namespace AgileStudioServerTest.IntegrationTests
{
    /// <summary>
    /// Service to help create test data.
    /// </summary>
    public class Fixtures
    {
        private readonly DBContext _DBContext;

        public Fixtures(DBContext _dbContext)
        {
            _DBContext = _dbContext;
        }

        public Project CreateProject(
            string? title = null, 
            BacklogItemTypeSchema? backlogItemTypeSchema = null,
            User? createdBy = null)
        {
            title ??= "Test Project";
            backlogItemTypeSchema ??= CreateBacklogItemTypeSchema();
            createdBy ??= CreateUser();

            var project = new Project(title)
            {
                BacklogItemTypeSchema = backlogItemTypeSchema,
                CreatedBy = createdBy
            };
            _DBContext.Project.Add(project);
            _DBContext.SaveChanges();
            return project;
        }

        public BacklogItem CreateBacklogItem(
            string? title = null,
            User? createdBy = null, 
            Project? project = null, 
            BacklogItemType? backlogItemType = null,
            WorkflowState? workflowState = null,
            Sprint? sprint = null,
            Release? release = null)
        {
            title ??= "Test BacklogItem";
            createdBy ??= CreateUser();
            project ??= CreateProject();
            backlogItemType ??= CreateBacklogItemType(
                    backlogItemTypeSchema: project.BacklogItemTypeSchema);
            workflowState ??= CreateWorkflowState();
            sprint ??= CreateSprint(project: project);
            release ??= CreateRelease(project: project);

            var backlogItem = new BacklogItem(title)
            {
                CreatedBy = createdBy,
                Project = project,
                BacklogItemType = backlogItemType,
                WorkflowState = workflowState,
                Sprint = sprint,
                Release = release
            };
            _DBContext.BacklogItem.Add(backlogItem);
            _DBContext.SaveChanges();
            return backlogItem;
        }

        public BacklogItemTypeSchema CreateBacklogItemTypeSchema(
            string? title = null,
            User? createdBy = null)
        {
            title ??= "Test BacklogItemTypeSchema";
            createdBy ??= CreateUser();

            var backlogItemTypeSchema = new BacklogItemTypeSchema(title)
            {
                CreatedBy = createdBy
            };
            _DBContext.BacklogItemTypeSchema.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();
            return backlogItemTypeSchema;
        }

        public BacklogItemType CreateBacklogItemType(
            string? title = null,
            User? createdBy = null,
            BacklogItemTypeSchema? backlogItemTypeSchema = null,
            Workflow? workflow = null)
        {
            title ??= "Test BacklogItemType";
            createdBy ??= CreateUser();
            backlogItemTypeSchema ??= CreateBacklogItemTypeSchema();
            workflow ??= CreateWorkflow();

            var backlogItemType = new BacklogItemType(title)
            {
                CreatedBy = createdBy,
                BacklogItemTypeSchema = backlogItemTypeSchema,
                Workflow = workflow
            };
            _DBContext.BacklogItemType.Add(backlogItemType);
            _DBContext.SaveChanges();
            return backlogItemType;
        }

        public Sprint CreateSprint(
            int? sprintNumber = null,
            Project? project = null,
            User? createdBy = null)
        {
            int nextSprintNumber = sprintNumber ?? 1;
            project ??= CreateProject();
            createdBy ??= CreateUser();

            var sprint = new Sprint(nextSprintNumber)
            {
                Project = project,
                CreatedBy = createdBy
            };
            _DBContext.Sprint.Add(sprint);
            _DBContext.SaveChanges();
            return sprint;
        }

        public Release CreateRelease(
            string? title = null,
            Project? project = null,
            User? createdBy = null)
        {
            title ??= "v1.0.0";
            project ??= CreateProject();
            createdBy ??= CreateUser();

            var release = new Release(title)
            {
                Project = project,
                CreatedBy = createdBy
            };
            _DBContext.Release.Add(release);
            _DBContext.SaveChanges();
            return release;
        }

        public Workflow CreateWorkflow(
            string? title = null,
            User? createdBy = null)
        {
            title ??= "Test Workflow";
            createdBy ??= CreateUser();

            var workflow = new Workflow(title) 
            {
                CreatedBy = createdBy
            };
            _DBContext.Workflow.Add(workflow);
            _DBContext.SaveChanges();
            return workflow;
        }

        public WorkflowState CreateWorkflowState(
            string? title = null,
            Workflow? workflow = null,
            User? createdBy = null)
        {
            title ??= "Test Workflow";
            workflow ??= CreateWorkflow();
            createdBy ??= CreateUser();

            var workflowState = new WorkflowState(title)
            {
                Workflow = workflow,
                CreatedBy = createdBy
            };
            _DBContext.WorkflowState.Add(workflowState);
            _DBContext.SaveChanges();
            return workflowState;
        }

        public User CreateUser(
            string? email = null,
            string? firstName = null,
            string? lastName = null)
        {
            firstName ??= "Test";
            lastName ??= "User";
            email ??= "testuser@local.agilestudio.dev";

            var user = new User(email, firstName, lastName);
            _DBContext.User.Add(user);
            _DBContext.SaveChanges();
            return user;
        }

        /// <summary>
        /// Save any changes made to entity fixtures to the database.
        /// </summary>
        public void SaveChanges()
        {
            _DBContext.SaveChanges();
        }
    }
}
