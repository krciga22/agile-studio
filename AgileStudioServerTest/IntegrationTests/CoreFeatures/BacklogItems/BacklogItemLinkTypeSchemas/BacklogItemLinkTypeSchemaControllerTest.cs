using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Data;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemLinkTypeSchemaController _Controller;

        private readonly BacklogItemLinkTypeSchemaFixture _BacklogItemLinkTypeSchemaFixture;

        public BacklogItemLinkTypeSchemaControllerTest(
            DBContext dbContext,
            BacklogItemLinkTypeSchemaController controller,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture) : base(dbContext)
        {
            _Controller = controller;
            _BacklogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
        }

        [Fact]
        public void List_ReturnsDtos()
        {
            List<BacklogItemLinkTypeSchemaModel> backlogItemLinkTypeSchemas = new() {
                _BacklogItemLinkTypeSchemaFixture.Create("Test Backlog Item Link Type Schema 1"),
                _BacklogItemLinkTypeSchemaFixture.Create("Test Backlog Item Link Type Schema 2")
            };

            List<BacklogItemLinkTypeSchemaDto>? dtos = null;
            IActionResult result = _Controller.List();
            if (result is OkObjectResult okResult)
            {
                dtos = okResult.Value as List<BacklogItemLinkTypeSchemaDto>;
            }

            Assert.IsType<List<BacklogItemLinkTypeSchemaDto>>(dtos);
            Assert.Equal(backlogItemLinkTypeSchemas.Count, dtos.Count);
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var backlogItemLinkTypeSchema = _BacklogItemLinkTypeSchemaFixture.Create();

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
            var backlogItemLinkTypeSchema = _BacklogItemLinkTypeSchemaFixture.Create();
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
            var backlogItemLinkTypeSchema = _BacklogItemLinkTypeSchemaFixture.Create();

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
