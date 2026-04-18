using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypes;
using AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemLinkTypes;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemLinkTypeSchemas;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.Accounts.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemLinkTypeSchemaEntryService _backlogItemLinkTypeSchemaEntryService;

        private readonly BacklogItemLinkTypeFixture _BacklogItemLinkTypeFixture;

        private readonly BacklogItemLinkTypeSchemaFixture _BacklogItemLinkTypeSchemaFixture;

        private readonly BacklogItemLinkTypeSchemaEntryFixture _BacklogItemLinkTypeSchemaEntryFixture;

        public BacklogItemLinkTypeSchemaEntryServiceTest(
            DBContext dbContext,
            BacklogItemLinkTypeSchemaEntryService backlogItemLinkTypeSchemaEntryService,
            BacklogItemLinkTypeFixture backlogItemLinkTypeFixture,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture,
            BacklogItemLinkTypeSchemaEntryFixture backlogItemLinkTypeSchemaEntryFixture) : base(dbContext)
        {
            _backlogItemLinkTypeSchemaEntryService = backlogItemLinkTypeSchemaEntryService;
            _BacklogItemLinkTypeFixture = backlogItemLinkTypeFixture;
            _BacklogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
            _BacklogItemLinkTypeSchemaEntryFixture = backlogItemLinkTypeSchemaEntryFixture;
        }

        [Fact]
        public void Create_ReturnsBacklogItemLinkTypeSchemaEntry()
        {
            BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchemaModel = _BacklogItemLinkTypeSchemaFixture.Create();
            BacklogItemLinkTypeModel backlogItemLinkTypeModel = _BacklogItemLinkTypeFixture.Create();
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
            var backlogItemLinkTypeSchemaEntry = _BacklogItemLinkTypeSchemaEntryFixture.Create();
            var returnedBacklogItemLinkTypeSchemaEntry = _backlogItemLinkTypeSchemaEntryService.Get(backlogItemLinkTypeSchemaEntry.ID);

            Assert.NotNull(returnedBacklogItemLinkTypeSchemaEntry);
            Assert.Equal(backlogItemLinkTypeSchemaEntry.ID, returnedBacklogItemLinkTypeSchemaEntry.ID);
        }

        [Fact]
        public void Delete_DeletesBacklogItemLinkTypeSchemaEntry()
        {
            var backlogItemLinkTypeSchemaEntry = _BacklogItemLinkTypeSchemaEntryFixture.Create();

            _backlogItemLinkTypeSchemaEntryService.Delete(backlogItemLinkTypeSchemaEntry);

            backlogItemLinkTypeSchemaEntry = _backlogItemLinkTypeSchemaEntryService.Get(backlogItemLinkTypeSchemaEntry.ID);
            Assert.Null(backlogItemLinkTypeSchemaEntry);
        }
    }
}
