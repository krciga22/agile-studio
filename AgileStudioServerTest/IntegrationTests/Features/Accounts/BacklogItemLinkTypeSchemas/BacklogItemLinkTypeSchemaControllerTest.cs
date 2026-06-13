using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServerTest.Features.Accounts.Accounts;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypeSchemas;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemLinkTypeSchemaController _Controller;

        private readonly BacklogItemLinkTypeSchemaFixture _BacklogItemLinkTypeSchemaFixture;

        private readonly AccountFixture _AccountFixture;

        public BacklogItemLinkTypeSchemaControllerTest(
            DBContext dbContext,
            BacklogItemLinkTypeSchemaController controller,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture,
            AccountFixture accountFixture) : base(dbContext)
        {
            _Controller = controller;
            _BacklogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
            _AccountFixture = accountFixture;
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
            var account = _AccountFixture.Create();
            var postDto = new BacklogItemLinkTypeSchemaPostDto("Test Schema", account.ID);

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
