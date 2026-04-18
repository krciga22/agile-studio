using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.Data;
using AgileStudioServerTest.CoreFeatures.Accounts.Workflows;
using AgileStudioServerTest.CoreFeatures.Accounts.WorkflowStates;
using AgileStudioServer.CoreFeatures.Accounts.Workflows;
using AgileStudioServer.CoreFeatures.Accounts.WorkflowStates;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.Accounts.Workflows
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
        public void Get_WithNoArguments_ReturnsDtos()
        {
            List<WorkflowModel> workflows = new() {
                _WorkflowFixture.Create("Test Workflow 1"),
                _WorkflowFixture.Create("Test Workflow 2")
            };

            List<WorkflowDto>? dtos = null;
            IActionResult result = _Controller.Get();
            if (result is OkObjectResult okResult)
            {
                dtos = okResult.Value as List<WorkflowDto>;
            }

            Assert.IsType<List<WorkflowDto>>(dtos);
            Assert.Equal(workflows.Count, dtos.Count);
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var workflow = _WorkflowFixture.Create();

            WorkflowDto? dto = null;
            IActionResult result = _Controller.Get(workflow.ID);
            if (result is OkObjectResult okResult)
            {
                dto = okResult.Value as WorkflowDto;
            }

            Assert.IsType<WorkflowDto>(dto);
            Assert.Equal(workflow.ID, dto.ID);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.Get(Constants.NonExistantId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
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

        [Fact]
        public void Post_WithDto_ReturnsDto()
        {
            var workflowPostDto = new WorkflowPostDto("Test Workflow");

            WorkflowDto? dto = null;
            IActionResult result = _Controller.Post(workflowPostDto);
            if (result is CreatedResult createdResult)
            {
                dto = createdResult.Value as WorkflowDto;
            }

            Assert.IsType<WorkflowDto>(dto);
            Assert.Equal(workflowPostDto.Title, dto.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var workflow = _WorkflowFixture.Create();
            var title = $"{workflow.Title} Updated";
            var workflowPatchDto = new WorkflowPatchDto(workflow.ID, title);

            IActionResult result = _Controller.Patch(workflow.ID, workflowPatchDto);
            WorkflowDto? dto = null;
            if (result is OkObjectResult okObjectResult)
            {
                dto = okObjectResult.Value as WorkflowDto;
            }

            Assert.IsType<WorkflowDto>(dto);
            Assert.Equal(workflowPatchDto.Title, dto.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var workflow = _WorkflowFixture.Create();

            IActionResult result = _Controller.Delete(workflow.ID);

            Assert.IsType<OkResult>(result as OkResult);
        }

        [Fact]
        public void Delete_WithInvalidId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.Delete(Constants.NonExistantId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }
    }
}
