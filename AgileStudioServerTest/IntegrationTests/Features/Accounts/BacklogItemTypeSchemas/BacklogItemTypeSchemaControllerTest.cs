using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Accounts.Accounts;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemTypeSchemaController _Controller;

        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;

        private readonly BacklogItemTypeFixture _BacklogItemTypeFixture;
        private readonly AccountFixture _AccountFixture;

        public BacklogItemTypeSchemaControllerTest(
            DBContext dbContext,
            BacklogItemTypeSchemaController controller,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemTypeFixture backlogItemTypeFixture,
            AccountFixture accountFixture) : base(dbContext)
        {
            _Controller = controller;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _BacklogItemTypeFixture = backlogItemTypeFixture;
            _AccountFixture = accountFixture;
        }

        [Fact]
        public void List_ReturnsDtos()
        {
            List<BacklogItemTypeSchemaModel> backlogItemTypeSchemas = new() {
                _BacklogItemTypeSchemaFixture.Create("Test Backlog Item Type Schema 1"),
                _BacklogItemTypeSchemaFixture.Create("Test Backlog Item Type Schema 2")
            };

            List<BacklogItemTypeSchemaDto>? dtos = null;
            IActionResult result = _Controller.List();
            if (result is OkObjectResult okResult)
            {
                dtos = okResult.Value as List<BacklogItemTypeSchemaDto>;
            }

            Assert.IsType<List<BacklogItemTypeSchemaDto>>(dtos);
            Assert.Equal(backlogItemTypeSchemas.Count, dtos.Count);
        }

        [Fact]
        public void ListBacklogItemTypes_WithId_ReturnsDtos()
        {
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Create();

            List<BacklogItemTypeModel> backlogItemTypes = new() {
                _BacklogItemTypeFixture.Create(
                    title: "Test Backlog Item Type Schema 1",
                    backlogItemTypeSchema: backlogItemTypeSchema),
                _BacklogItemTypeFixture.Create(
                    title: "Test Backlog Item Type Schema 2",
                    backlogItemTypeSchema: backlogItemTypeSchema)
            };

            List<BacklogItemTypeSummaryDto>? dtos = null;
            IActionResult result = _Controller.ListBacklogItemTypes(backlogItemTypeSchema.ID);
            if (result is OkObjectResult okResult)
            {
                dtos = okResult.Value as List<BacklogItemTypeSummaryDto>;
            }

            Assert.IsType<List<BacklogItemTypeSummaryDto>>(dtos);
            Assert.Equal(backlogItemTypes.Count, dtos.Count);
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Create();

            BacklogItemTypeSchemaDto? dto = null;
            IActionResult result = _Controller.Get(backlogItemTypeSchema.ID);
            if (result is OkObjectResult okResult)
            {
                dto = okResult.Value as BacklogItemTypeSchemaDto;
            }

            Assert.IsType<BacklogItemTypeSchemaDto>(dto);
            Assert.Equal(backlogItemTypeSchema.ID, dto.ID);
        }

        [Fact]
        public void Post_WithDto_ReturnsDto()
        {
            var account = _AccountFixture.Create();
            var postDto = new BacklogItemTypeSchemaPostDto(
                "Test Backlog Item Type Schema", account.ID);

            BacklogItemTypeSchemaDto? dto = null;
            IActionResult result = _Controller.Post(postDto);
            if (result is CreatedResult createdResult)
            {
                dto = createdResult.Value as BacklogItemTypeSchemaDto;
            }

            Assert.IsType<BacklogItemTypeSchemaDto>(dto);
            Assert.Equal(postDto.Title, dto.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Create();
            var title = $"{backlogItemTypeSchema.Title} Updated";
            var patchDto = new BacklogItemTypeSchemaPatchDto(backlogItemTypeSchema.ID, title);

            IActionResult result = _Controller.Patch(backlogItemTypeSchema.ID, patchDto);
            BacklogItemTypeSchemaDto? dto = null;
            if (result is OkObjectResult okObjectResult)
            {
                dto = okObjectResult.Value as BacklogItemTypeSchemaDto;
            }

            Assert.IsType<BacklogItemTypeSchemaDto>(dto);
            Assert.Equal(patchDto.Title, dto.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Create();

            IActionResult result = _Controller.Delete(backlogItemTypeSchema.ID);

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
