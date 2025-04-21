using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.Workflows.WorkflowStates;
using AgileStudioServer.CoreFeatures.Workflows.Workflows;
using AgileStudioServerTest.CoreFeatures.Workflows.WorkflowStates;
using AgileStudioServerTest.CoreFeatures.Workflows.Workflows;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.Workflows.WorkflowStates
{
    public class WorkflowStateServiceTest : AbstractServiceTest
    {
        private readonly WorkflowStateService _workflowStateService;

        private readonly WorkflowStateFixture _WorkflowStateFixture;

        private readonly WorkflowFixture _WorkflowFixture;

        public WorkflowStateServiceTest(
            DBContext dbContext,
            WorkflowStateService workflowStateService,
            WorkflowStateFixture workflowStateFixture,
            WorkflowFixture workflowFixture) : base(dbContext)
        {
            _workflowStateService = workflowStateService;
            _WorkflowStateFixture = workflowStateFixture;
            _WorkflowFixture = workflowFixture;
        }

        [Fact]
        public void Create_ReturnsWorkflowState()
        {
            WorkflowModel workflow = _WorkflowFixture.Create();
            WorkflowStateModel workflowState = new("Test WorkflowState", workflow.ID);

            workflowState = _workflowStateService.Create(workflowState);

            Assert.NotNull(workflowState);
            Assert.True(workflowState.ID > 0);
        }

        [Fact]
        public void Get_ReturnsWorkflowState()
        {
            var workflowState = _WorkflowStateFixture.Create();

            var returnedWorkflowState = _workflowStateService.Get(workflowState.ID);

            Assert.NotNull(returnedWorkflowState);
            Assert.Equal(workflowState.ID, returnedWorkflowState.ID);
        }

        [Fact]
        public void GetByWorkflowId_ReturnsWorkflowStates()
        {
            var workflow = _WorkflowFixture.Create();
            var workflowStates = new List<WorkflowStateModel>
            {
                _WorkflowStateFixture.Create("Test WorkflowState 1", workflow: workflow),
                _WorkflowStateFixture.Create("Test WorkflowState 2", workflow: workflow)
            };

            List<WorkflowStateModel> returnedWorkflowStates = _workflowStateService
                .GetByWorkflowId(workflow.ID);

            Assert.Equal(workflowStates.Count, returnedWorkflowStates.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedWorkflowState()
        {
            var workflowState = _WorkflowStateFixture.Create();
            var title = $"{workflowState.Title} Updated";

            workflowState.Title = title;
            workflowState = _workflowStateService.Update(workflowState);

            Assert.NotNull(workflowState);
            Assert.Equal(title, workflowState.Title);
        }

        [Fact]
        public void Delete_DeletesWorkflowState()
        {
            var workflowState = _WorkflowStateFixture.Create();

            _workflowStateService.Delete(workflowState);

            workflowState = _workflowStateService.Get(workflowState.ID);
            Assert.Null(workflowState);
        }
    }
}
