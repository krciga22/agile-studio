using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.CoreFeatures.Projects.Sprints;
using AgileStudioServerTest.CoreFeatures.Projects.Projects;
using AgileStudioServer.Data;
using AgileStudioServerTest.CoreFeatures.Projects.Sprints;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.Projects.Sprints
{
    public class SprintControllerTest : AbstractControllerTest
    {
        private readonly SprintController _Controller;

        private readonly SprintFixture _SprintFixture;

        private readonly ProjectFixture _ProjectFixture;

        public SprintControllerTest(
            DBContext dbContext,
            SprintController controller,
            SprintFixture sprintFixture,
            ProjectFixture projectFixture) : base(dbContext)
        {
            _Controller = controller;
            _SprintFixture = sprintFixture;
            _ProjectFixture = projectFixture;
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var sprint = _SprintFixture.Create();

            SprintDto? sprintDto = null;
            IActionResult result = _Controller.Get(sprint.ID);
            if (result is OkObjectResult okResult)
            {
                sprintDto = okResult.Value as SprintDto;
            }

            Assert.IsType<SprintDto>(sprintDto);
            Assert.Equal(sprint.ID, sprintDto.ID);
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
            var project = _ProjectFixture.Create();
            var sprintPostDto = new SprintPostDto(project.ID);
            sprintPostDto.Description = "Test Sprint";

            SprintDto? sprintDto = null;
            IActionResult result = _Controller.Post(sprintPostDto);
            if (result is CreatedResult createdResult)
            {
                sprintDto = createdResult.Value as SprintDto;
            }

            Assert.IsType<SprintDto>(sprintDto);
            Assert.Equal(sprintPostDto.Description, sprintDto.Description);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var sprint = _SprintFixture.Create();
            var description = $"Test Sprint {sprint.ID} Updated";
            var sprintPatchDto = new SprintPatchDto(sprint.ID)
            {
                Description = description
            };

            IActionResult result = _Controller.Patch(sprint.ID, sprintPatchDto);
            SprintDto? sprintDto = null;
            if (result is OkObjectResult okObjectResult)
            {
                sprintDto = okObjectResult.Value as SprintDto;
            }

            Assert.IsType<SprintDto>(sprintDto);
            Assert.Equal(sprintPatchDto.Description, sprintDto.Description);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var sprint = _SprintFixture.Create();

            IActionResult result = _Controller.Delete(sprint.ID);

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
