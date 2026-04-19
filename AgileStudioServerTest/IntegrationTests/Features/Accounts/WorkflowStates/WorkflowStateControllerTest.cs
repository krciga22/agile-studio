using Microsoft.AspNetCore.Mvc;
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

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var workflowState = _WorkflowStateFixture.Create();

            WorkflowStateDto? dto = null;
            IActionResult result = _Controller.Get(workflowState.ID);
            if (result is OkObjectResult okResult)
            {
                dto = okResult.Value as WorkflowStateDto;
            }

            Assert.IsType<WorkflowStateDto>(dto);
            Assert.Equal(workflowState.ID, dto.ID);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.Get(Constants.NonExistantId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public void Post_WithDto_ReturnsDto()
        {
            var workflow = _WorkflowFixture.Create();
            var workflowStatePostDto = new WorkflowStatePostDto("Test WorkflowState", workflow.ID);

            WorkflowStateDto? dto = null;
            IActionResult result = _Controller.Post(workflowStatePostDto);
            if (result is CreatedResult createdResult)
            {
                dto = createdResult.Value as WorkflowStateDto;
            }

            Assert.IsType<WorkflowStateDto>(dto);
            Assert.Equal(workflowStatePostDto.Title, dto.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var workflowState = _WorkflowStateFixture.Create();
            var title = $"{workflowState.Title} Updated";
            var workflowStatePatchDto = new WorkflowStatePatchDto(workflowState.ID, title);

            IActionResult result = _Controller.Patch(workflowState.ID, workflowStatePatchDto);
            WorkflowStateDto? dto = null;
            if (result is OkObjectResult okObjectResult)
            {
                dto = okObjectResult.Value as WorkflowStateDto;
            }

            Assert.IsType<WorkflowStateDto>(dto);
            Assert.Equal(workflowStatePatchDto.Title, dto.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var workflowState = _WorkflowStateFixture.Create();

            IActionResult result = _Controller.Delete(workflowState.ID);

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
