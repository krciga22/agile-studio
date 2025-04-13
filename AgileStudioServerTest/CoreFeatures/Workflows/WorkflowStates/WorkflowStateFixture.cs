
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.CoreFeatures.Workflows.Workflows;
using AgileStudioServer.CoreFeatures.Workflows.WorkflowStates;
using AgileStudioServer.Data;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Users.Users;
using AgileStudioServerTest.CoreFeatures.Workflows.Workflows;

namespace AgileStudioServerTest.CoreFeatures.Workflows.WorkflowStates
{
    public class WorkflowStateFixture : AbstractEntityFixture
    {
        private readonly WorkflowFixture _workflowFixture;
        private readonly UserFixture _userFixture;

        public WorkflowStateFixture(DBContext dbContext, WorkflowFixture workflowFixture, UserFixture userFixture) : base(dbContext)
        {
            _workflowFixture = workflowFixture;
            _userFixture = userFixture;
        }

        public WorkflowState Create(
            string? title = null,
            Workflow? workflow = null,
            User? createdBy = null)
        {
            title ??= "Test Workflow";
            workflow ??= _workflowFixture.Create();
            createdBy ??= _userFixture.Create();

            var workflowState = new WorkflowState(title, workflow.ID)
            {
                CreatedBy = createdBy
            };
            _DBContext.WorkflowState.Add(workflowState);
            _DBContext.SaveChanges();
            return workflowState;
        }
    }
}
