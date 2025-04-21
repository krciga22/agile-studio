using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemLinkTypes;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.BacklogItems.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemLinkTypeService _backlogItemLinkTypeService;

        private readonly BacklogItemLinkTypeFixture _BacklogItemLinkTypeFixture;

        public BacklogItemLinkTypeServiceTest(
            DBContext dbContext,
            BacklogItemLinkTypeService backlogItemLinkTypeService,
            BacklogItemLinkTypeFixture backlogItemLinkTypeFixture) : base(dbContext)
        {
            _backlogItemLinkTypeService = backlogItemLinkTypeService;
            _BacklogItemLinkTypeFixture = backlogItemLinkTypeFixture;
        }

        [Fact]
        public void Create_ReturnsBacklogItemLinkType()
        {
            BacklogItemLinkTypeModel backlogItemLinkType = new("blocks", "blocked-by");

            backlogItemLinkType = _backlogItemLinkTypeService.Create(backlogItemLinkType);

            Assert.NotNull(backlogItemLinkType);
            Assert.True(backlogItemLinkType.ID > 0);
        }

        [Fact]
        public void Get_ReturnsBacklogItemLinkType()
        {
            var backlogItemLinkType = _BacklogItemLinkTypeFixture.Create();

            var returnedBacklogItemLinkType = _backlogItemLinkTypeService.Get(backlogItemLinkType.ID);

            Assert.NotNull(returnedBacklogItemLinkType);
            Assert.Equal(backlogItemLinkType.ID, returnedBacklogItemLinkType.ID);
        }

        [Fact]
        public void Update_ReturnsUpdatedBacklogItemLinkType()
        {
            var backlogItemLinkType = _BacklogItemLinkTypeFixture.Create();
            var title = $"{backlogItemLinkType.Title} Updated";

            backlogItemLinkType.Title = title;
            backlogItemLinkType = _backlogItemLinkTypeService.Update(backlogItemLinkType);

            Assert.NotNull(backlogItemLinkType);
            Assert.Equal(title, backlogItemLinkType.Title);
        }

        [Fact]
        public void Delete_DeletesBacklogItemLinkType()
        {
            var backlogItemLinkType = _BacklogItemLinkTypeFixture.Create();

            _backlogItemLinkTypeService.Delete(backlogItemLinkType);

            backlogItemLinkType = _backlogItemLinkTypeService.Get(backlogItemLinkType.ID);
            Assert.Null(backlogItemLinkType);
        }
    }
}
