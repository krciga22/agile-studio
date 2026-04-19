using AgileStudioServer.Features.Accounts.Workflows;
using AgileStudioServer.Features.Accounts.WorkflowStates;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Accounts.Workflows;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Accounts.WorkflowStates
{
    public class WorkflowStateFixture : AbstractEntityFixture<WorkflowStateRepository>
    {
        private readonly WorkflowFixture _workflowFixture;
        private readonly UserFixture _userFixture;

        public WorkflowStateFixture(
            WorkflowStateRepository workflowStateRepository, 
            WorkflowFixture workflowFixture, 
            UserFixture userFixture) : base(workflowStateRepository)
        {
            _workflowFixture = workflowFixture;
            _userFixture = userFixture;
        }

        public WorkflowStateModel Create(
            string? title = null,
            WorkflowModel? workflow = null,
            UserModel? createdBy = null)
        {
            title ??= "Test Workflow";
            workflow ??= _workflowFixture.Create();
            createdBy ??= _userFixture.Create();

            var workflowState = new WorkflowStateModel(title, workflow.ID)
            {
                CreatedById = createdBy.ID
            };

            return _Repository.Create(workflowState);
        }

        public WorkflowStateModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
