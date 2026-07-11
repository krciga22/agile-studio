using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.WorkflowStates;
using AgileStudioServerTest.Features.Accounts.Workflows;
using AgileStudioServerTest.Features.Accounts.WorkflowStates;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.WorkflowStates
{
    public class WorkflowStateControllerTest : AbstractControllerTest
    {
        private readonly WorkflowStateController _Controller;

        private readonly WorkflowFixture _WorkflowFixture;

        private readonly WorkflowStateFixture _WorkflowStateFixture;

        public WorkflowStateControllerTest(
            DBContext dbContext,
            WorkflowStateController controller,
            WorkflowFixture workflowFixture,
            WorkflowStateFixture workflowStateFixture) : base(dbContext)
        {
            _Controller = controller;
            _WorkflowFixture = workflowFixture;
            _WorkflowStateFixture = workflowStateFixture;
        }

        // todo add tests
    }
}
