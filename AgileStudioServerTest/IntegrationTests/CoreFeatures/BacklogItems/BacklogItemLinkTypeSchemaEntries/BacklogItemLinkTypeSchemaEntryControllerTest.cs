using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemaEntries;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemaEntryEntries
{
    public class BacklogItemLinkTypeSchemaEntryControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemLinkTypeSchemaEntryController _Controller;

        public BacklogItemLinkTypeSchemaEntryControllerTest(
            DBContext dbContext,
            EntityFixtures fixtures,
            BacklogItemLinkTypeSchemaEntryController controller) : base(dbContext, fixtures)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var backlogItemLinkTypeSchemaEntry = _Fixtures.CreateBacklogItemLinkTypeSchemaEntry();

            BacklogItemLinkTypeSchemaEntryDto? dto = null;
            IActionResult result = _Controller.Get(backlogItemLinkTypeSchemaEntry.ID);
            if (result is OkObjectResult okResult)
            {
                dto = okResult.Value as BacklogItemLinkTypeSchemaEntryDto;
            }

            Assert.IsType<BacklogItemLinkTypeSchemaEntryDto>(dto);
            Assert.Equal(backlogItemLinkTypeSchemaEntry.ID, dto.ID);
        }

        [Fact]
        public void Post_WithDto_ReturnsDto()
        {
            var backlogItemLinkTypeSchema = _Fixtures.CreateBacklogItemLinkTypeSchema();
            var backlogItemLinkType = _Fixtures.CreateBacklogItemLinkType();
            var postDto = new BacklogItemLinkTypeSchemaEntryPostDto(
                backlogItemLinkTypeSchema.ID,
                backlogItemLinkType.ID);

            BacklogItemLinkTypeSchemaEntryDto ? dto = null;
            IActionResult result = _Controller.Post(postDto);
            if (result is CreatedResult createdResult)
            {
                dto = createdResult.Value as BacklogItemLinkTypeSchemaEntryDto;
            }

            Assert.IsType<BacklogItemLinkTypeSchemaEntryDto>(dto);
            Assert.Equal(postDto.BacklogItemLinkTypeSchemaID, dto.BacklogItemLinkTypeSchemaSummaryDto.ID);
            Assert.Equal(postDto.BacklogItemLinkTypeID, dto.BacklogItemLinkTypeSummaryDto.ID);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var backlogItemLinkTypeSchemaEntry = _Fixtures.CreateBacklogItemLinkTypeSchemaEntry();

            IActionResult result = _Controller.Delete(backlogItemLinkTypeSchemaEntry.ID);

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
