
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.CoreFeatures.Workflows.Workflows;
using AgileStudioServer.CoreFeatures.Workflows.WorkflowStates;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Users.Users;
using AgileStudioServerTest.CoreFeatures.Workflows.Workflows;

namespace AgileStudioServerTest.CoreFeatures.Workflows.WorkflowStates
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
    }
}
