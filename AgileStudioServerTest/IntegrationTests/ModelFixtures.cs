using AgileStudioServer.Data;
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
        private readonly BacklogItemLinkTypeSchemaEntryService _backlogItemLinkTypeSchemaEntryService;
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
            BacklogItemLinkTypeSchemaEntryService backlogItemLinkTypeSchemaEntryService,
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
            _backlogItemLinkTypeSchemaEntryService = backlogItemLinkTypeSchemaEntryService;
            _sprintService = sprintService;
            _releaseService = releaseService;
            _userService = userService;
            _workflowService = workflowService;
            _workflowStateService = workflowStateService;
        }

        public ProjectModel CreateProject(
            string? title = null, 
            BacklogItemTypeSchemaModel? backlogItemTypeSchema = null,
            BacklogItemLinkTypeSchemaModel? backlogItemLinkTypeSchema = null,
            UserModel? createdBy = null)
        {
            title ??= "Test Project";
            backlogItemTypeSchema ??= CreateBacklogItemTypeSchema();
            backlogItemLinkTypeSchema ??= CreateBacklogItemLinkTypeSchema();
            createdBy ??= CreateUser();

            var project = new ProjectModel(title, backlogItemTypeSchema.ID, backlogItemLinkTypeSchema.ID)
            {
                CreatedByID = createdBy.ID
            };
            project = _projectService.Create(project);
            return project;
        }

        public BacklogItemModel CreateBacklogItem(
            string? title = null,
            UserModel? createdBy = null, 
            ProjectModel? project = null, 
            BacklogItemTypeModel? backlogItemType = null,
            WorkflowStateModel? workflowState = null,
            SprintModel? sprint = null,
            ReleaseModel? release = null,
            BacklogItemModel? parentBacklogItem = null)
        {
            title ??= "Test BacklogItem";

            BacklogItemTypeSchemaModel schema = CreateBacklogItemTypeSchema();
            project ??= CreateProject(null, backlogItemTypeSchema: schema);
            backlogItemType ??= CreateBacklogItemType(backlogItemTypeSchema: schema);

            workflowState ??= CreateWorkflowState();

            var backlogItem = new BacklogItemModel(title, project.ID, backlogItemType.ID, workflowState.ID);

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
            UserModel? createdBy = null)
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
            UserModel? createdBy = null,
            BacklogItemTypeSchemaModel? backlogItemTypeSchema = null,
            WorkflowModel? workflow = null)
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

        public ChildBacklogItemTypeModel CreateChildBacklogItemType(
            BacklogItemTypeModel? parentType = null,
            BacklogItemTypeModel? childType = null,
            BacklogItemTypeSchemaModel? schema = null,
            UserModel? createdBy = null)
        {
            schema ??= CreateBacklogItemTypeSchema();
            parentType ??= CreateBacklogItemType("Story", backlogItemTypeSchema: schema);
            childType ??= CreateBacklogItemType("Task", backlogItemTypeSchema: schema);
            createdBy ??= CreateUser();

            var childBacklogItemType = new ChildBacklogItemTypeModel(childType.ID, parentType.ID, schema.ID)
            {
                CreatedByID = createdBy.ID
            };
            childBacklogItemType = _childBacklogItemTypeService.Create(childBacklogItemType);
            return childBacklogItemType;
        }

        public BacklogItemLinkTypeModel CreateBacklogItemLinkType(
            string? title = null,
            string? titleOpposite = null,
            UserModel? createdBy = null)
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
            UserModel? createdBy = null)
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

        public BacklogItemLinkTypeSchemaEntryModel CreateBacklogItemLinkTypeSchemaEntry(
            BacklogItemLinkTypeSchemaModel? backlogItemLinkTypeSchemaModel = null,
            BacklogItemLinkTypeModel? backlogItemLinkTypeModel = null,
            UserModel? createdBy = null)
        {
            backlogItemLinkTypeSchemaModel ??= CreateBacklogItemLinkTypeSchema();
            backlogItemLinkTypeModel ??= CreateBacklogItemLinkType();
            createdBy ??= CreateUser();

            var backlogItemLinkTypeSchemaEntry = new BacklogItemLinkTypeSchemaEntryModel(
                backlogItemLinkTypeSchemaModel.ID,
                backlogItemLinkTypeModel.ID)
            {
                CreatedByID = createdBy.ID,
            };
            backlogItemLinkTypeSchemaEntry = _backlogItemLinkTypeSchemaEntryService.Create(backlogItemLinkTypeSchemaEntry);
            return backlogItemLinkTypeSchemaEntry;
        }

        public SprintModel CreateSprint(
            int? sprintNumber = null,
            ProjectModel? project = null,
            UserModel? createdBy = null)
        {
            int nextSprintNumber = sprintNumber ?? 1;
            project ??= CreateProject();
            createdBy ??= CreateUser();

            var sprint = new SprintModel(nextSprintNumber, project.ID)
            {
                CreatedByID = createdBy.ID
            };
            sprint = _sprintService.Create(sprint);
            return sprint;
        }

        public ReleaseModel CreateRelease(
            string? title = null,
            ProjectModel? project = null,
            UserModel? createdBy = null)
        {
            title ??= "v1.0.0";
            project ??= CreateProject();
            createdBy ??= CreateUser();

            var release = new ReleaseModel(title, project.ID)
            {
                CreatedByID = createdBy.ID
            };
            release = _releaseService.Create(release);
            return release;
        }

        public WorkflowModel CreateWorkflow(
            string? title = null,
            UserModel? createdBy = null)
        {
            title ??= "Test Workflow";
            createdBy ??= CreateUser();

            var workflow = new WorkflowModel(title) 
            {
                CreatedById = createdBy.ID
            };
            workflow = _workflowService.Create(workflow);
            return workflow;
        }

        public WorkflowStateModel CreateWorkflowState(
            string? title = null,
            WorkflowModel? workflow = null,
            UserModel? createdBy = null)
        {
            title ??= "Test Workflow";
            workflow ??= CreateWorkflow();
            createdBy ??= CreateUser();

            var workflowState = new WorkflowStateModel(title, workflow.ID)
            {
                CreatedById = createdBy.ID
            };
            workflowState = _workflowStateService.Create(workflowState);
            return workflowState;
        }

        public UserModel CreateUser(
            string? email = null,
            string? firstName = null,
            string? lastName = null)
        {
            firstName ??= "Test";
            lastName ??= "User";
            email ??= "testuser@local.agilestudio.dev";

            var user = new UserModel(email, firstName, lastName);
            user = _userService.Create(user);
            return user;
        }
    }
}
