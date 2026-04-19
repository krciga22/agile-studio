using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Accounts.WorkflowStates;
using AgileStudioServer.Features.Projects.BacklogItems;
using AgileStudioServer.Features.Projects.Projects;
using AgileStudioServer.Features.Projects.Releases;
using AgileStudioServer.Features.Projects.Sprints;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.WorkflowStates;
using AgileStudioServerTest.Features.Projects.Projects;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Projects.BacklogItems
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
