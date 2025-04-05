using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.Workflows.Workflows;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.Workflows.Services
{
    public class WorkflowServiceTest : AbstractServiceTest
    {
        private readonly WorkflowService _workflowService;

        public WorkflowServiceTest(
            DBContext dbContext,
            ModelFixtures fixtures,
            WorkflowService workflowService) : base(dbContext, fixtures)
        {
            _workflowService = workflowService;
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
            var workflow = _Fixtures.CreateWorkflow();

            var returnedWorkflow = _workflowService.Get(workflow.ID);

            Assert.NotNull(returnedWorkflow);
            Assert.Equal(workflow.ID, returnedWorkflow.ID);
        }

        [Fact]
        public void GetAll_ReturnsAllWorkflows()
        {
            var workflows = new List<WorkflowModel>
            {
                _Fixtures.CreateWorkflow("Test Workflow 1"),
                _Fixtures.CreateWorkflow("Test Workflow 2")
            };

            List<WorkflowModel> returnedWorkflows = _workflowService.GetAll();

            Assert.Equal(workflows.Count, returnedWorkflows.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedWorkflow()
        {
            var workflow = _Fixtures.CreateWorkflow();
            var title = $"{workflow.Title} Updated";

            workflow.Title = title;
            workflow = _workflowService.Update(workflow);

            Assert.NotNull(workflow);
            Assert.Equal(title, workflow.Title);
        }

        [Fact]
        public void Delete_DeletesWorkflow()
        {
            var workflow = _Fixtures.CreateWorkflow();

            _workflowService.Delete(workflow);

            workflow = _workflowService.Get(workflow.ID);
            Assert.Null(workflow);
        }
    }
}
