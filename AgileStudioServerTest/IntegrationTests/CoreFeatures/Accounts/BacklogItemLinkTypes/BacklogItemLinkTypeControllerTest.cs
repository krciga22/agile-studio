using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypes;
using AgileStudioServer.Data;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemLinkTypes;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.Accounts.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemLinkTypeController _Controller;

        private readonly BacklogItemLinkTypeFixture _BacklogItemLinkTypeFixture;

        public BacklogItemLinkTypeControllerTest(
            DBContext dbContext,
            BacklogItemLinkTypeController controller,
            BacklogItemLinkTypeFixture backlogItemLinkTypeFixture) : base(dbContext)
        {
            _Controller = controller;
            _BacklogItemLinkTypeFixture = backlogItemLinkTypeFixture;
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var backlogItemLinkType = _BacklogItemLinkTypeFixture.Create();

            BacklogItemLinkTypeDto? dto = null;
            IActionResult result = _Controller.Get(backlogItemLinkType.ID);
            if (result is OkObjectResult okResult)
            {
                dto = okResult.Value as BacklogItemLinkTypeDto;
            }

            Assert.IsType<BacklogItemLinkTypeDto>(dto);
            Assert.Equal(backlogItemLinkType.ID, dto.ID);
        }

        [Fact]
        public void Post_WithDto_ReturnsDto()
        {
            var postDto = new BacklogItemLinkTypePostDto("blocks", "blocked-by");

            BacklogItemLinkTypeDto? dto = null;
            IActionResult result = _Controller.Post(postDto);
            if (result is CreatedResult createdResult)
            {
                dto = createdResult.Value as BacklogItemLinkTypeDto;
            }

            Assert.IsType<BacklogItemLinkTypeDto>(dto);
            Assert.Equal(postDto.Title, dto.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var backlogItemLinkType = _BacklogItemLinkTypeFixture.Create();
            var title = $"{backlogItemLinkType.Title} Updated";
            var patchDto = new BacklogItemLinkTypePatchDto(backlogItemLinkType.ID, title, backlogItemLinkType.TitleOpposite);

            IActionResult result = _Controller.Patch(backlogItemLinkType.ID, patchDto);
            BacklogItemLinkTypeDto? dto = null;
            if (result is OkObjectResult okObjectResult)
            {
                dto = okObjectResult.Value as BacklogItemLinkTypeDto;
            }

            Assert.IsType<BacklogItemLinkTypeDto>(dto);
            Assert.Equal(patchDto.Title, dto.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var backlogItemLinkType = _BacklogItemLinkTypeFixture.Create();

            IActionResult result = _Controller.Delete(backlogItemLinkType.ID);

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
