using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemLinkTypeSchemaService _backlogItemLinkTypeSchemaService;

        public BacklogItemLinkTypeSchemaServiceTest(
            DBContext dbContext,
            ModelFixtures fixtures,
            BacklogItemLinkTypeSchemaService backlogItemLinkTypeSchemaService) : base(dbContext, fixtures)
        {
            _backlogItemLinkTypeSchemaService = backlogItemLinkTypeSchemaService;
        }

        [Fact]
        public void Create_ReturnsBacklogItemLinkTypeSchema()
        {
            BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchema = new("Test Schema");

            backlogItemLinkTypeSchema = _backlogItemLinkTypeSchemaService.Create(backlogItemLinkTypeSchema);

            Assert.NotNull(backlogItemLinkTypeSchema);
            Assert.True(backlogItemLinkTypeSchema.ID > 0);
        }

        [Fact]
        public void Get_ReturnsBacklogItemLinkTypeSchema()
        {
            var backlogItemLinkTypeSchema = _Fixtures.CreateBacklogItemLinkTypeSchema();

            var returnedBacklogItemLinkTypeSchema = _backlogItemLinkTypeSchemaService.Get(backlogItemLinkTypeSchema.ID);

            Assert.NotNull(returnedBacklogItemLinkTypeSchema);
            Assert.Equal(backlogItemLinkTypeSchema.ID, returnedBacklogItemLinkTypeSchema.ID);
        }

        [Fact]
        public void Update_ReturnsUpdatedBacklogItemLinkTypeSchema()
        {
            var backlogItemLinkTypeSchema = _Fixtures.CreateBacklogItemLinkTypeSchema();
            var title = $"{backlogItemLinkTypeSchema.Title} Updated";

            backlogItemLinkTypeSchema.Title = title;
            backlogItemLinkTypeSchema = _backlogItemLinkTypeSchemaService.Update(backlogItemLinkTypeSchema);

            Assert.NotNull(backlogItemLinkTypeSchema);
            Assert.Equal(title, backlogItemLinkTypeSchema.Title);
        }

        [Fact]
        public void Delete_DeletesBacklogItemLinkTypeSchema()
        {
            var backlogItemLinkTypeSchema = _Fixtures.CreateBacklogItemLinkTypeSchema();

            _backlogItemLinkTypeSchemaService.Delete(backlogItemLinkTypeSchema);

            backlogItemLinkTypeSchema = _backlogItemLinkTypeSchemaService.Get(backlogItemLinkTypeSchema.ID);
            Assert.Null(backlogItemLinkTypeSchema);
        }
    }
}
