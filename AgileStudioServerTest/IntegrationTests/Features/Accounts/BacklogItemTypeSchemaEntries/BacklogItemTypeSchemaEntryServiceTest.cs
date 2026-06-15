using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEntries;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemaEntries;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.BacklogItemTypeSchemaEntries
{
    public class BacklogItemTypeSchemaEntryServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemTypeSchemaEntryService _backlogItemTypeSchemaEntryService;

        private readonly BacklogItemTypeSchemaEntryFixture _BacklogItemTypeSchemaEntryFixture;

        private readonly BacklogItemTypeFixture _BacklogItemTypeFixture;

        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;

        public BacklogItemTypeSchemaEntryServiceTest(
            DBContext dbContext,
            BacklogItemTypeSchemaEntryService backlogItemTypeSchemaEntryService,
            BacklogItemTypeSchemaEntryFixture backlogItemTypeSchemaEntryFixture,
            BacklogItemTypeFixture backlogItemTypeFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture) : base(dbContext)
        {
            _backlogItemTypeSchemaEntryService = backlogItemTypeSchemaEntryService;
            _BacklogItemTypeSchemaEntryFixture = backlogItemTypeSchemaEntryFixture;
            _BacklogItemTypeFixture = backlogItemTypeFixture;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
        }

        [Fact]
        public void Create_ReturnsBacklogItemTypeSchemaEntry()
        {
            BacklogItemTypeModel backlogItemTypeStory = _BacklogItemTypeFixture.Create("Story");
            BacklogItemTypeModel backlogItemTypeTask = _BacklogItemTypeFixture.Create("Task");
            BacklogItemTypeSchemaModel schema = _BacklogItemTypeSchemaFixture.Create();
            BacklogItemTypeSchemaEntryModel backlogItemTypeSchemaEntry = new(
                backlogItemTypeTask.ID, backlogItemTypeStory.ID, schema.ID);

            backlogItemTypeSchemaEntry = _backlogItemTypeSchemaEntryService.Create(backlogItemTypeSchemaEntry);

            Assert.NotNull(backlogItemTypeSchemaEntry);
            Assert.True(backlogItemTypeSchemaEntry.ID > 0);
        }

        [Fact]
        public void Get_ReturnsBacklogItemTypeSchemaEntry()
        {
            var backlogItemTypeSchemaEntry = _BacklogItemTypeSchemaEntryFixture.Create();

            var returnedBacklogItemTypeSchemaEntry = _backlogItemTypeSchemaEntryService.Get(backlogItemTypeSchemaEntry.ID);

            Assert.NotNull(returnedBacklogItemTypeSchemaEntry);
            Assert.Equal(backlogItemTypeSchemaEntry.ID, returnedBacklogItemTypeSchemaEntry.ID);
        }

        [Fact]
        public void GetByParentTypeId_ReturnsBacklogItemTypeSchemaEntries()
        {
            var parentBacklogItemType = _BacklogItemTypeFixture.Create("Parent Type");

            var backlogItemTypeSchemaEntries = new List<BacklogItemTypeSchemaEntryModel>
            {
                _BacklogItemTypeSchemaEntryFixture.Create(
                    parentType: parentBacklogItemType
                ),
                _BacklogItemTypeSchemaEntryFixture.Create(
                    parentType: parentBacklogItemType
                )
            };

            List<BacklogItemTypeSchemaEntryModel> returnedBacklogItemTypeSchemaEntries = _backlogItemTypeSchemaEntryService
                .GetByParentTypeId(parentBacklogItemType.ID);

            Assert.Equal(backlogItemTypeSchemaEntries.Count, returnedBacklogItemTypeSchemaEntries.Count);
        }

        [Fact]
        public void GetByChildTypeId_ReturnsBacklogItemTypeSchemaEntries()
        {
            var backlogItemTypeSchemaEntry = _BacklogItemTypeFixture.Create("Child Type");

            var backlogItemTypeSchemaEntries = new List<BacklogItemTypeSchemaEntryModel>
            {
                _BacklogItemTypeSchemaEntryFixture.Create(
                    childType: backlogItemTypeSchemaEntry
                ),
                _BacklogItemTypeSchemaEntryFixture.Create(
                    childType: backlogItemTypeSchemaEntry
                )
            };

            List<BacklogItemTypeSchemaEntryModel> returnedBacklogItemTypeSchemaEntries = _backlogItemTypeSchemaEntryService
                .GetByChildTypeId(backlogItemTypeSchemaEntry.ID);

            Assert.Equal(backlogItemTypeSchemaEntries.Count, returnedBacklogItemTypeSchemaEntries.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedBacklogItemTypeSchemaEntry()
        {
            var backlogItemTypeSchemaEntry = _BacklogItemTypeSchemaEntryFixture.Create();
            var backlogItemType = _BacklogItemTypeFixture.Create("Updated Type");

            backlogItemTypeSchemaEntry.ChildTypeID = backlogItemType.ID;
            backlogItemTypeSchemaEntry = _backlogItemTypeSchemaEntryService.Update(backlogItemTypeSchemaEntry);

            Assert.NotNull(backlogItemTypeSchemaEntry);
            Assert.Equal(backlogItemType.ID, backlogItemTypeSchemaEntry.ChildTypeID);
        }

        [Fact]
        public void Delete_DeletesBacklogItemTypeSchemaEntry()
        {
            var backlogItemTypeSchemaEntry = _BacklogItemTypeSchemaEntryFixture.Create();

            _backlogItemTypeSchemaEntryService.Delete(backlogItemTypeSchemaEntry);

            backlogItemTypeSchemaEntry = _backlogItemTypeSchemaEntryService.Get(backlogItemTypeSchemaEntry.ID);
            Assert.Null(backlogItemTypeSchemaEntry);
        }
    }
}
