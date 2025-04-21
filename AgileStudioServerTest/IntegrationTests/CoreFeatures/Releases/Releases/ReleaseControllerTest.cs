using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.Releases.Releases;
using AgileStudioServerTest.CoreFeatures.Releases.Releases;
using AgileStudioServerTest.CoreFeatures.Projects.Projects;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.Releases.Releases
{
    public class ReleaseControllerTest : AbstractControllerTest
    {
        private readonly ReleaseController _Controller;

        private readonly ReleaseFixture _ReleaseFixture;

        private readonly ProjectFixture _ProjectFixture;

        public ReleaseControllerTest(
            DBContext dbContext,
            ReleaseController controller,
            ReleaseFixture releaseFixture,
            ProjectFixture projectFixture) : base(dbContext)
        {
            _Controller = controller;
            _ReleaseFixture = releaseFixture;
            _ProjectFixture = projectFixture;
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var release = _ReleaseFixture.Create();

            ReleaseDto? releaseDto = null;
            IActionResult result = _Controller.Get(release.ID);
            if (result is OkObjectResult okResult)
            {
                releaseDto = okResult.Value as ReleaseDto;
            }

            Assert.IsType<ReleaseDto>(releaseDto);
            Assert.Equal(release.ID, releaseDto.ID);
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
            var releasePostDto = new ReleasePostDto("v1.0.0", project.ID);

            ReleaseDto? releaseDto = null;
            IActionResult result = _Controller.Post(releasePostDto);
            if (result is CreatedResult createdResult)
            {
                releaseDto = createdResult.Value as ReleaseDto;
            }

            Assert.IsType<ReleaseDto>(releaseDto);
            Assert.Equal(releasePostDto.Title, releaseDto.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var release = _ReleaseFixture.Create("v1.0.0");
            var releasePatchDto = new ReleasePatchDto(release.ID, "v1.0.1");

            IActionResult result = _Controller.Patch(release.ID, releasePatchDto);
            ReleaseDto? releaseDto = null;
            if (result is OkObjectResult okObjectResult)
            {
                releaseDto = okObjectResult.Value as ReleaseDto;
            }

            Assert.IsType<ReleaseDto>(releaseDto);
            Assert.Equal(releasePatchDto.Title, releaseDto.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var release = _ReleaseFixture.Create();

            IActionResult result = _Controller.Delete(release.ID);

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
