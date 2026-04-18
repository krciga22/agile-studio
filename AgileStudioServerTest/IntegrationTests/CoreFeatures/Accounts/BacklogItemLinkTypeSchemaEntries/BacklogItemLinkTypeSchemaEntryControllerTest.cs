using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemLinkTypes;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemLinkTypeSchemas;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.Accounts.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemLinkTypeSchemaEntryController _Controller;

        private readonly BacklogItemLinkTypeFixture _BacklogItemLinkTypeFixture;

        private readonly BacklogItemLinkTypeSchemaFixture _BacklogItemLinkTypeSchemaFixture;

        private readonly BacklogItemLinkTypeSchemaEntryFixture _BacklogItemLinkTypeSchemaEntryFixture;

        public BacklogItemLinkTypeSchemaEntryControllerTest(
            DBContext dbContext,
            BacklogItemLinkTypeSchemaEntryController controller,
            BacklogItemLinkTypeFixture backlogItemLinkTypeFixture,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture,
            BacklogItemLinkTypeSchemaEntryFixture backlogItemLinkTypeSchemaEntryFixture) : base(dbContext)
        {
            _Controller = controller;
            _BacklogItemLinkTypeFixture = backlogItemLinkTypeFixture;
            _BacklogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
            _BacklogItemLinkTypeSchemaEntryFixture = backlogItemLinkTypeSchemaEntryFixture;
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var backlogItemLinkTypeSchemaEntry = _BacklogItemLinkTypeSchemaEntryFixture.Create();

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
            var backlogItemLinkTypeSchema = _BacklogItemLinkTypeSchemaFixture.Create();
            var backlogItemLinkType = _BacklogItemLinkTypeFixture.Create();
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
            var backlogItemLinkTypeSchemaEntry = _BacklogItemLinkTypeSchemaEntryFixture.Create();

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
