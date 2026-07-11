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

        [Fact]
        public void GetWorkflowStatesForWorkflow_WithId_ReturnsDtos()
        {
            var workflow = _WorkflowFixture.Create();

            List<WorkflowStateModel> workflowStates = new() {
                _WorkflowStateFixture.Create(
                    title: "Test Workflow State 1",
                    workflow: workflow),
                _WorkflowStateFixture.Create(
                    title: "Test Workflow State 2",
                    workflow: workflow)
            };

            List<WorkflowStateDto>? dtos = null;
            IActionResult result = _Controller.GetWorkflowStatesForWorkflow(workflow.ID);
            if (result is OkObjectResult okResult)
            {
                dtos = okResult.Value as List<WorkflowStateDto>;
            }

            Assert.IsType<List<WorkflowStateDto>>(dtos);
            Assert.Equal(workflowStates.Count, dtos.Count);
        }
    }
}
