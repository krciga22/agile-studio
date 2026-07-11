using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.Workflows;
using AgileStudioServerTest.Features.Accounts.Accounts;
using AgileStudioServerTest.Features.Accounts.Workflows;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.Workflows
{
    public class WorkflowServiceTest : AbstractServiceTest
    {
        private readonly WorkflowService _workflowService;

        private readonly WorkflowFixture _WorkflowFixture;

        private readonly AccountFixture _AccountFixture;

        public WorkflowServiceTest(
            DBContext dbContext,
            WorkflowService workflowService,
            WorkflowFixture workflowFixture,
            AccountFixture accountFixture) : base(dbContext)
        {
            _workflowService = workflowService;
            _WorkflowFixture = workflowFixture;
            _AccountFixture = accountFixture;
        }

        [Fact]
        public void Create_ReturnsWorkflow()
        {
            var account = _AccountFixture.Create();
            WorkflowModel workflow = new("Test Workflow", account.ID);

            workflow = _workflowService.Create(workflow);

            Assert.NotNull(workflow);
            Assert.True(workflow.ID > 0);
        }

        [Fact]
        public void Get_ReturnsWorkflow()
        {
            var workflow = _WorkflowFixture.Create();

            var returnedWorkflow = _workflowService.Get(workflow.ID);

            Assert.NotNull(returnedWorkflow);
            Assert.Equal(workflow.ID, returnedWorkflow.ID);
        }

        [Fact]
        public void Update_ReturnsUpdatedWorkflow()
        {
            var workflow = _WorkflowFixture.Create();
            var title = $"{workflow.Title} Updated";

            workflow.Title = title;
            workflow = _workflowService.Update(workflow);

            Assert.NotNull(workflow);
            Assert.Equal(title, workflow.Title);
        }

        [Fact]
        public void Delete_DeletesWorkflow()
        {
            var workflow = _WorkflowFixture.Create();

            _workflowService.Delete(workflow);

            Assert.Throws<ModelNotFoundException>(() => 
                _workflowService.Get(workflow.ID));
        }
    }
}
