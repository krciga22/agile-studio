using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemLinkTypeSchemaController _Controller;

        public BacklogItemLinkTypeSchemaControllerTest(
            DBContext dbContext,
            EntityFixtures fixtures,
            BacklogItemLinkTypeSchemaController controller) : base(dbContext, fixtures)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var backlogItemLinkTypeSchema = _Fixtures.CreateBacklogItemLinkTypeSchema();

            BacklogItemLinkTypeSchemaDto? dto = null;
            IActionResult result = _Controller.Get(backlogItemLinkTypeSchema.ID);
            if (result is OkObjectResult okResult)
            {
                dto = okResult.Value as BacklogItemLinkTypeSchemaDto;
            }

            Assert.IsType<BacklogItemLinkTypeSchemaDto>(dto);
            Assert.Equal(backlogItemLinkTypeSchema.ID, dto.ID);
        }

        [Fact]
        public void Post_WithDto_ReturnsDto()
        {
            var postDto = new BacklogItemLinkTypeSchemaPostDto("Test Schema");

            BacklogItemLinkTypeSchemaDto? dto = null;
            IActionResult result = _Controller.Post(postDto);
            if (result is CreatedResult createdResult)
            {
                dto = createdResult.Value as BacklogItemLinkTypeSchemaDto;
            }

            Assert.IsType<BacklogItemLinkTypeSchemaDto>(dto);
            Assert.Equal(postDto.Title, dto.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var backlogItemLinkTypeSchema = _Fixtures.CreateBacklogItemLinkTypeSchema();
            var title = $"{backlogItemLinkTypeSchema.Title} Updated";
            var patchDto = new BacklogItemLinkTypeSchemaPatchDto(backlogItemLinkTypeSchema.ID, title);

            IActionResult result = _Controller.Patch(backlogItemLinkTypeSchema.ID, patchDto);
            BacklogItemLinkTypeSchemaDto? dto = null;
            if (result is OkObjectResult okObjectResult)
            {
                dto = okObjectResult.Value as BacklogItemLinkTypeSchemaDto;
            }

            Assert.IsType<BacklogItemLinkTypeSchemaDto>(dto);
            Assert.Equal(patchDto.Title, dto.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var backlogItemLinkTypeSchema = _Fixtures.CreateBacklogItemLinkTypeSchema();

            IActionResult result = _Controller.Delete(backlogItemLinkTypeSchema.ID);

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
