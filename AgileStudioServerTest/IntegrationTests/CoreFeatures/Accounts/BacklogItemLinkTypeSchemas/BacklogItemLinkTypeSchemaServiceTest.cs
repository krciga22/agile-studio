using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypeSchemas;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemLinkTypeSchemaService _backlogItemLinkTypeSchemaService;

        private readonly BacklogItemLinkTypeSchemaFixture _BacklogItemLinkTypeSchemaFixture;

        public BacklogItemLinkTypeSchemaServiceTest(
            DBContext dbContext,
            BacklogItemLinkTypeSchemaService backlogItemLinkTypeSchemaService,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture) : base(dbContext)
        {
            _backlogItemLinkTypeSchemaService = backlogItemLinkTypeSchemaService;
            _BacklogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
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
            var backlogItemLinkTypeSchema = _BacklogItemLinkTypeSchemaFixture.Create();

            var returnedBacklogItemLinkTypeSchema = _backlogItemLinkTypeSchemaService.Get(backlogItemLinkTypeSchema.ID);

            Assert.NotNull(returnedBacklogItemLinkTypeSchema);
            Assert.Equal(backlogItemLinkTypeSchema.ID, returnedBacklogItemLinkTypeSchema.ID);
        }

        [Fact]
        public void GetAll_ReturnsAllBacklogItemLinkTypeSchemas()
        {
            var backlogItemLinkTypeSchemas = new List<BacklogItemLinkTypeSchemaModel>
            {
                _BacklogItemLinkTypeSchemaFixture.Create("Test BacklogItemLinkTypeSchema 1"),
                _BacklogItemLinkTypeSchemaFixture.Create("Test BacklogItemLinkTypeSchema 2")
            };

            List<BacklogItemLinkTypeSchemaModel> returnedBacklogItemLinkTypeSchemas = _backlogItemLinkTypeSchemaService
                .GetAll();

            Assert.Equal(backlogItemLinkTypeSchemas.Count, returnedBacklogItemLinkTypeSchemas.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedBacklogItemLinkTypeSchema()
        {
            var backlogItemLinkTypeSchema = _BacklogItemLinkTypeSchemaFixture.Create();
            var title = $"{backlogItemLinkTypeSchema.Title} Updated";

            backlogItemLinkTypeSchema.Title = title;
            backlogItemLinkTypeSchema = _backlogItemLinkTypeSchemaService.Update(backlogItemLinkTypeSchema);

            Assert.NotNull(backlogItemLinkTypeSchema);
            Assert.Equal(title, backlogItemLinkTypeSchema.Title);
        }

        [Fact]
        public void Delete_DeletesBacklogItemLinkTypeSchema()
        {
            var backlogItemLinkTypeSchema = _BacklogItemLinkTypeSchemaFixture.Create();

            _backlogItemLinkTypeSchemaService.Delete(backlogItemLinkTypeSchema);

            backlogItemLinkTypeSchema = _backlogItemLinkTypeSchemaService.Get(backlogItemLinkTypeSchema.ID);
            Assert.Null(backlogItemLinkTypeSchema);
        }
    }
}
