
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItems;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.Projects.Projects;
using AgileStudioServer.CoreFeatures.Projects.Releases;
using AgileStudioServer.CoreFeatures.Sprints.Sprints;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.CoreFeatures.Workflows.WorkflowStates;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServerTest.CoreFeatures.Projects.Projects;
using AgileStudioServerTest.CoreFeatures.Users.Users;
using AgileStudioServerTest.CoreFeatures.Workflows.WorkflowStates;

namespace AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItems
{
    public class BacklogItemFixture : AbstractEntityFixture<BacklogItemRepository>
    {
        private readonly BacklogItemTypeSchemaRepository _backlogItemTypeSchemaRepository;
        private readonly UserFixture _userFixture;

        private readonly ProjectFixture _projectFixture;

        private readonly BacklogItemTypeFixture _backlogItemTypeFixture;

        private readonly WorkflowStateFixture _workflowStateFixture;

        public BacklogItemFixture(
            BacklogItemRepository backlogItemRepository, 
            BacklogItemTypeSchemaRepository backlogItemTypeSchemaRepository,
            UserFixture userFixture,
            ProjectFixture projectFixture,
            BacklogItemTypeFixture backlogItemTypeFixture,
            WorkflowStateFixture workflowStateFixture) : base(backlogItemRepository)
        {
            _backlogItemTypeSchemaRepository = backlogItemTypeSchemaRepository;
            _userFixture = userFixture;
            _projectFixture = projectFixture;
            _backlogItemTypeFixture = backlogItemTypeFixture;
            _workflowStateFixture = workflowStateFixture;
        }

        public BacklogItemModel Create(
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
            project ??= _projectFixture.Create();

            BacklogItemTypeSchemaModel? backlogItemTypeSchema = 
                _backlogItemTypeSchemaRepository.Get(project.BacklogItemTypeSchemaID);

            backlogItemType ??= _backlogItemTypeFixture.Create(
                    backlogItemTypeSchema: backlogItemTypeSchema);

            workflowState ??= _workflowStateFixture.Create();

            var backlogItem = new BacklogItemModel(
                title, project.ID, backlogItemType.ID, workflowState.ID);

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
            
            return _Repository.Create(backlogItem);
        }

        public BacklogItemModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
