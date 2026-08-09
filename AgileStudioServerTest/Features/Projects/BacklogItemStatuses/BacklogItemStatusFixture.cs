using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.Workflows;
using AgileStudioServer.Features.Accounts.WorkflowStates;
using AgileStudioServer.Features.Projects.BacklogItems;
using AgileStudioServer.Features.Projects.BacklogItemStatuses;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.Workflows;
using AgileStudioServerTest.Features.Accounts.WorkflowStates;
using AgileStudioServerTest.Features.Projects.BacklogItems;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Projects.BacklogItemStatuses
{
    public class BacklogItemStatusFixture : AbstractEntityFixture<BacklogItemStatusRepository>
    {
        private readonly UserFixture _userFixture;
        private readonly BacklogItemFixture _backlogItemFixture;
        private readonly BacklogItemTypeFixture _backlogItemTypeFixture;
        private readonly WorkflowFixture _workflowFixture;
        private readonly WorkflowStateFixture _workflowStateFixture;

        public BacklogItemStatusFixture(
            BacklogItemStatusRepository backlogItemStatusRepository,
            BacklogItemTypeFixture backlogItemTypeFixture,
            UserFixture userFixture,
            BacklogItemFixture backlogItemFixture,
            WorkflowFixture workflowFixture,
            WorkflowStateFixture workflowStateFixture) : base(backlogItemStatusRepository)
        {
            _userFixture = userFixture;
            _backlogItemFixture = backlogItemFixture;
            _backlogItemTypeFixture = backlogItemTypeFixture;
            _workflowFixture = workflowFixture;
            _workflowStateFixture = workflowStateFixture;
        }

        public BacklogItemStatusModel Create(
            BacklogItemModel? backlogItem = null,
            WorkflowStateModel? workflowState = null,
            UserModel? createdBy = null,
            string? comment = null)
        {
            backlogItem ??= _backlogItemFixture.Create();

            BacklogItemTypeModel backlogItemTypeModel = _backlogItemTypeFixture.Get(backlogItem.BacklogItemTypeID) ?? 
                throw new ModelNotFoundException(nameof(BacklogItemTypeModel), 
                    backlogItem.BacklogItemTypeID.ToString());

            WorkflowModel workflowModel = _workflowFixture.Get(backlogItemTypeModel.WorkflowID) ??
                throw new ModelNotFoundException(nameof(WorkflowModel),
                    backlogItemTypeModel.WorkflowID.ToString());

            workflowState ??= _workflowStateFixture.Create(workflow: workflowModel);

            createdBy ??= _userFixture.Create();

            var backlogItemStatus = new BacklogItemStatusModel(
                backlogItem.ID, workflowState.ID)
            {
                CreatedByID = createdBy.ID,
                Comment = comment
            };

            return _Repository.Create(backlogItemStatus);
        }

        public BacklogItemStatusModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}