using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.Workflows.Workflows;
using AgileStudioServerTest.CoreFeatures.Workflows.Workflows;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.Workflows.Workflows
{
    public class WorkflowServiceTest : AbstractServiceTest
    {
        private readonly WorkflowService _workflowService;

        private readonly WorkflowFixture _WorkflowFixture;

        public WorkflowServiceTest(
            DBContext dbContext,
            WorkflowService workflowService,
            WorkflowFixture workflowFixture) : base(dbContext)
        {
            _workflowService = workflowService;
            _WorkflowFixture = workflowFixture;
        }

        [Fact]
        public void Create_ReturnsWorkflow()
        {
            WorkflowModel workflow = new("Test Workflow");

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
        public void GetAll_ReturnsAllWorkflows()
        {
            var workflows = new List<WorkflowModel>
            {
                _WorkflowFixture.Create("Test Workflow 1"),
                _WorkflowFixture.Create("Test Workflow 2")
            };

            List<WorkflowModel> returnedWorkflows = _workflowService.GetAll();

            Assert.Equal(workflows.Count, returnedWorkflows.Count);
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

            workflow = _workflowService.Get(workflow.ID);
            Assert.Null(workflow);
        }
    }
}
