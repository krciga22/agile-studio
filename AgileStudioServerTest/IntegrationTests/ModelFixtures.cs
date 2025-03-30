using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.Sprints.Services;
using AgileStudioServer.CoreFeatures.Sprints.Services.Models;
using AgileStudioServer.CoreFeatures.Releases.Services;
using AgileStudioServer.CoreFeatures.Releases.Services.Models;
using AgileStudioServer.CoreFeatures.Workflows.Services;
using AgileStudioServer.CoreFeatures.Workflows.Services.Models;
using AgileStudioServer.CoreFeatures.Projects.Services;
using AgileStudioServer.CoreFeatures.Projects.Services.Models;
using AgileStudioServer.CoreFeatures.Users.Services;
using AgileStudioServer.CoreFeatures.Users.Services.Models;
using AgileStudioServer.CoreFeatures.BacklogItems.Services;
using AgileStudioServer.CoreFeatures.BacklogItems.Services.Models;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypes;

namespace AgileStudioServerTest.IntegrationTests
{
    /// <summary>
    /// Service to help create test data.
    /// </summary>
    public class ModelFixtures
    {
        private readonly ProjectService _projectService;
        private readonly BacklogItemService _backlogItemService;
        private readonly BacklogItemTypeService _backlogItemTypeService;
        private readonly BacklogItemTypeSchemaService _backlogItemTypeSchemaService;
        private readonly ChildBacklogItemTypeService _childBacklogItemTypeService;
        private readonly BacklogItemLinkTypeService _backlogItemLinkTypeService;
        private readonly BacklogItemLinkTypeSchemaService _backlogItemLinkTypeSchemaService;
        private readonly SprintService _sprintService;
        private readonly ReleaseService _releaseService;
        private readonly UserService _userService;
        private readonly WorkflowService _workflowService;
        private readonly WorkflowStateService _workflowStateService;

        public ModelFixtures(
            ProjectService projectService,
            BacklogItemService backlogItemService,
            BacklogItemTypeService backlogItemTypeService,
            BacklogItemTypeSchemaService backlogItemTypeSchemaService,
            ChildBacklogItemTypeService childBacklogItemTypeService,
            BacklogItemLinkTypeService backlogItemLinkTypeService,
            BacklogItemLinkTypeSchemaService backlogItemLinkTypeSchemaService,
            SprintService sprintService,
            ReleaseService releaseService,
            UserService userService,
            WorkflowService workflowService,
            WorkflowStateService workflowStateService)
        {
            _projectService = projectService;
            _backlogItemService = backlogItemService;
            _backlogItemTypeService = backlogItemTypeService;
            _backlogItemTypeSchemaService = backlogItemTypeSchemaService;
            _childBacklogItemTypeService = childBacklogItemTypeService;
            _backlogItemLinkTypeService = backlogItemLinkTypeService;
            _backlogItemLinkTypeSchemaService = backlogItemLinkTypeSchemaService;
            _sprintService = sprintService;
            _releaseService = releaseService;
            _userService = userService;
            _workflowService = workflowService;
            _workflowStateService = workflowStateService;
        }

        public Project CreateProject(
            string? title = null, 
            BacklogItemTypeSchemaModel? backlogItemTypeSchema = null,
            User? createdBy = null)
        {
            title ??= "Test Project";
            backlogItemTypeSchema ??= CreateBacklogItemTypeSchema();
            createdBy ??= CreateUser();

            var project = new Project(title, backlogItemTypeSchema.ID)
            {
                CreatedByID = createdBy.ID
            };
            project = _projectService.Create(project);
            return project;
        }

        public BacklogItem CreateBacklogItem(
            string? title = null,
            User? createdBy = null, 
            Project? project = null, 
            BacklogItemTypeModel? backlogItemType = null,
            WorkflowState? workflowState = null,
            Sprint? sprint = null,
            Release? release = null,
            BacklogItem? parentBacklogItem = null)
        {
            title ??= "Test BacklogItem";

            BacklogItemTypeSchemaModel schema = CreateBacklogItemTypeSchema();
            project ??= CreateProject(null, backlogItemTypeSchema: schema);
            backlogItemType ??= CreateBacklogItemType(backlogItemTypeSchema: schema);

            workflowState ??= CreateWorkflowState();

            var backlogItem = new BacklogItem(title, project.ID, backlogItemType.ID, workflowState.ID);

            if (createdBy != null)
            {
                backlogItem.CreatedByID = createdBy.ID;
            }

            if (sprint != null)
            {
                backlogItem.SprintID = sprint.ID;
            }

            if (release != null)
            {
                backlogItem.ReleaseID = release.ID;
            }

            if (parentBacklogItem != null)
            {
                backlogItem.ParentBacklogItemId = parentBacklogItem.ID;
            }

            backlogItem = _backlogItemService.Create(backlogItem);
            return backlogItem;
        }

        public BacklogItemTypeSchemaModel CreateBacklogItemTypeSchema(
            string? title = null,
            User? createdBy = null)
        {
            title ??= "Test BacklogItemTypeSchema";
            createdBy ??= CreateUser();

            var backlogItemTypeSchema = new BacklogItemTypeSchemaModel(title)
            {
                CreatedById = createdBy.ID
            };
            backlogItemTypeSchema = _backlogItemTypeSchemaService.Create(backlogItemTypeSchema);
            return backlogItemTypeSchema;
        }

        public BacklogItemTypeModel CreateBacklogItemType(
            string? title = null,
            User? createdBy = null,
            BacklogItemTypeSchemaModel? backlogItemTypeSchema = null,
            Workflow? workflow = null)
        {
            title ??= "Test BacklogItemType";
            createdBy ??= CreateUser();
            backlogItemTypeSchema ??= CreateBacklogItemTypeSchema();
            workflow ??= CreateWorkflow();

            var backlogItemType = new BacklogItemTypeModel(title, backlogItemTypeSchema.ID, workflow.ID)
            {
                CreatedByID = createdBy.ID,
            };
            backlogItemType = _backlogItemTypeService.Create(backlogItemType);
            return backlogItemType;
        }

        public ChildBacklogItemType CreateChildBacklogItemType(
            BacklogItemTypeModel? parentType = null,
            BacklogItemTypeModel? childType = null,
            BacklogItemTypeSchemaModel? schema = null,
            User? createdBy = null)
        {
            schema ??= CreateBacklogItemTypeSchema();
            parentType ??= CreateBacklogItemType("Story", backlogItemTypeSchema: schema);
            childType ??= CreateBacklogItemType("Task", backlogItemTypeSchema: schema);
            createdBy ??= CreateUser();

            var childBacklogItemType = new ChildBacklogItemType(childType.ID, parentType.ID, schema.ID)
            {
                CreatedByID = createdBy.ID
            };
            childBacklogItemType = _childBacklogItemTypeService.Create(childBacklogItemType);
            return childBacklogItemType;
        }

        public BacklogItemLinkTypeModel CreateBacklogItemLinkType(
            string? title = null,
            string? titleOpposite = null,
            User? createdBy = null)
        {
            title ??= "Test BacklogItemLinkType";
            titleOpposite ??= "Test BacklogItemLinkTypeOpposite";
            createdBy ??= CreateUser();

            var backlogItemLinkType = new BacklogItemLinkTypeModel(title, titleOpposite)
            {
                CreatedByID = createdBy.ID,
            };
            backlogItemLinkType = _backlogItemLinkTypeService.Create(backlogItemLinkType);
            return backlogItemLinkType;
        }

        public BacklogItemLinkTypeSchemaModel CreateBacklogItemLinkTypeSchema(
            string? title = null,
            User? createdBy = null)
        {
            title ??= "Test BacklogItemLinkTypeSchema";
            createdBy ??= CreateUser();

            var backlogItemLinkTypeSchema = new BacklogItemLinkTypeSchemaModel(title)
            {
                CreatedByID = createdBy.ID,
            };
            backlogItemLinkTypeSchema = _backlogItemLinkTypeSchemaService.Create(backlogItemLinkTypeSchema);
            return backlogItemLinkTypeSchema;
        }

        public Sprint CreateSprint(
            int? sprintNumber = null,
            Project? project = null,
            User? createdBy = null)
        {
            int nextSprintNumber = sprintNumber ?? 1;
            project ??= CreateProject();
            createdBy ??= CreateUser();

            var sprint = new Sprint(nextSprintNumber, project.ID)
            {
                CreatedByID = createdBy.ID
            };
            sprint = _sprintService.Create(sprint);
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

            var release = new Release(title, project.ID)
            {
                CreatedByID = createdBy.ID
            };
            release = _releaseService.Create(release);
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
                CreatedById = createdBy.ID
            };
            workflow = _workflowService.Create(workflow);
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

            var workflowState = new WorkflowState(title, workflow.ID)
            {
                CreatedById = createdBy.ID
            };
            workflowState = _workflowStateService.Create(workflowState);
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
            user = _userService.Create(user);
            return user;
        }
    }
}
