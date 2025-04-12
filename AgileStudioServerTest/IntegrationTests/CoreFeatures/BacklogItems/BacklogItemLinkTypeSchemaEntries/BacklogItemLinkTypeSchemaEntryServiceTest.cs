using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemLinkTypeSchemaEntryService _backlogItemLinkTypeSchemaEntryService;

        public BacklogItemLinkTypeSchemaEntryServiceTest(
            DBContext dbContext,
            ModelFixtures fixtures,
            BacklogItemLinkTypeSchemaEntryService backlogItemLinkTypeSchemaEntryService) : base(dbContext, fixtures)
        {
            _backlogItemLinkTypeSchemaEntryService = backlogItemLinkTypeSchemaEntryService;
        }

        [Fact]
        public void Create_ReturnsBacklogItemLinkTypeSchemaEntry()
        {
            BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchemaModel = _Fixtures.CreateBacklogItemLinkTypeSchema();
            BacklogItemLinkTypeModel backlogItemLinkTypeModel = _Fixtures.CreateBacklogItemLinkType();
            BacklogItemLinkTypeSchemaEntryModel backlogItemLinkTypeSchemaEntry = new(
                backlogItemLinkTypeSchemaModel.ID,
                backlogItemLinkTypeModel.ID);

            backlogItemLinkTypeSchemaEntry = _backlogItemLinkTypeSchemaEntryService.Create(backlogItemLinkTypeSchemaEntry);

            Assert.NotNull(backlogItemLinkTypeSchemaEntry);
            Assert.True(backlogItemLinkTypeSchemaEntry.ID > 0);
        }

        [Fact]
        public void Get_ReturnsBacklogItemLinkTypeSchemaEntry()
        {
            var backlogItemLinkTypeSchemaEntry = _Fixtures.CreateBacklogItemLinkTypeSchemaEntry();
            var returnedBacklogItemLinkTypeSchemaEntry = _backlogItemLinkTypeSchemaEntryService.Get(backlogItemLinkTypeSchemaEntry.ID);

            Assert.NotNull(returnedBacklogItemLinkTypeSchemaEntry);
            Assert.Equal(backlogItemLinkTypeSchemaEntry.ID, returnedBacklogItemLinkTypeSchemaEntry.ID);
        }

        [Fact]
        public void Delete_DeletesBacklogItemLinkTypeSchemaEntry()
        {
            var backlogItemLinkTypeSchemaEntry = _Fixtures.CreateBacklogItemLinkTypeSchemaEntry();

            _backlogItemLinkTypeSchemaEntryService.Delete(backlogItemLinkTypeSchemaEntry);

            backlogItemLinkTypeSchemaEntry = _backlogItemLinkTypeSchemaEntryService.Get(backlogItemLinkTypeSchemaEntry.ID);
            Assert.Null(backlogItemLinkTypeSchemaEntry);
        }
    }
}
