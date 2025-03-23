using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.BacklogItems.Services;
using AgileStudioServer.CoreFeatures.BacklogItems.Services.Models;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.BacklogItems.Services
{
    public class BacklogItemLinkTypeServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemLinkTypeService _backlogItemLinkTypeService;

        public BacklogItemLinkTypeServiceTest(
            DBContext dbContext,
            ModelFixtures fixtures,
            BacklogItemLinkTypeService backlogItemLinkTypeService) : base(dbContext, fixtures)
        {
            _backlogItemLinkTypeService = backlogItemLinkTypeService;
        }

        [Fact]
        public void Create_ReturnsBacklogItemLinkType()
        {
            BacklogItemLinkType backlogItemLinkType = new("blocks", "blocked-by");

            backlogItemLinkType = _backlogItemLinkTypeService.Create(backlogItemLinkType);

            Assert.NotNull(backlogItemLinkType);
            Assert.True(backlogItemLinkType.ID > 0);
        }

        [Fact]
        public void Get_ReturnsBacklogItemLinkType()
        {
            var backlogItemLinkType = _Fixtures.CreateBacklogItemLinkType();

            var returnedBacklogItemLinkType = _backlogItemLinkTypeService.Get(backlogItemLinkType.ID);

            Assert.NotNull(returnedBacklogItemLinkType);
            Assert.Equal(backlogItemLinkType.ID, returnedBacklogItemLinkType.ID);
        }

        [Fact]
        public void Update_ReturnsUpdatedBacklogItemLinkType()
        {
            var backlogItemLinkType = _Fixtures.CreateBacklogItemLinkType();
            var title = $"{backlogItemLinkType.Title} Updated";

            backlogItemLinkType.Title = title;
            backlogItemLinkType = _backlogItemLinkTypeService.Update(backlogItemLinkType);

            Assert.NotNull(backlogItemLinkType);
            Assert.Equal(title, backlogItemLinkType.Title);
        }

        [Fact]
        public void Delete_DeletesBacklogItemLinkType()
        {
            var backlogItemLinkType = _Fixtures.CreateBacklogItemLinkType();

            _backlogItemLinkTypeService.Delete(backlogItemLinkType);

            backlogItemLinkType = _backlogItemLinkTypeService.Get(backlogItemLinkType.ID);
            Assert.Null(backlogItemLinkType);
        }
    }
}
