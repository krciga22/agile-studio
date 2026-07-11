using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.Workflows;
using AgileStudioServer.Features.Accounts.WorkflowStates;
using AgileStudioServerTest.Features.Accounts.Workflows;
using AgileStudioServerTest.Features.Accounts.WorkflowStates;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.Workflows
{
    public class WorkflowControllerTest : AbstractControllerTest
    {
        private readonly WorkflowController _Controller;

        private readonly WorkflowFixture _WorkflowFixture;

        private readonly WorkflowStateFixture _WorkflowStateFixture;

        public WorkflowControllerTest(
            DBContext dbContext,
            WorkflowController controller,
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
