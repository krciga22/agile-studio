using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Accounts.Accounts;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemTypeSchemaService _backlogItemTypeSchemaService;

        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;

        private readonly AccountFixture _AccountFixture;

        public BacklogItemTypeSchemaServiceTest(
            DBContext dbContext,
            BacklogItemTypeSchemaService backlogItemTypeSchemaService,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            AccountFixture accountFixture) : base(dbContext)
        {
            _backlogItemTypeSchemaService = backlogItemTypeSchemaService;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _AccountFixture = accountFixture;
        }

        [Fact]
        public void Create_ReturnsBacklogItemTypeSchema()
        {
            var account = _AccountFixture.Create();
            BacklogItemTypeSchemaModel backlogItemTypeSchema = 
                new("Test BacklogItemTypeSchema", account.ID);

            backlogItemTypeSchema = _backlogItemTypeSchemaService.Create(backlogItemTypeSchema);

            Assert.NotNull(backlogItemTypeSchema);
            Assert.True(backlogItemTypeSchema.ID > 0);
        }

        [Fact]
        public void Get_ReturnsBacklogItemTypeSchema()
        {
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Create();

            var returnedBacklogItemTypeSchema = _backlogItemTypeSchemaService.Get(backlogItemTypeSchema.ID);

            Assert.NotNull(returnedBacklogItemTypeSchema);
            Assert.Equal(backlogItemTypeSchema.ID, returnedBacklogItemTypeSchema.ID);
        }

        [Fact]
        public void Update_ReturnsUpdatedBacklogItemTypeSchema()
        {
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Create();
            var title = $"{backlogItemTypeSchema.Title} Updated";

            backlogItemTypeSchema.Title = title;
            backlogItemTypeSchema = _backlogItemTypeSchemaService.Update(backlogItemTypeSchema);

            Assert.NotNull(backlogItemTypeSchema);
            Assert.Equal(title, backlogItemTypeSchema.Title);
        }

        [Fact]
        public void Delete_DeletesBacklogItemTypeSchema()
        {
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Create();

            _backlogItemTypeSchemaService.Delete(backlogItemTypeSchema);

            backlogItemTypeSchema = _backlogItemTypeSchemaService.Get(backlogItemTypeSchema.ID);
            Assert.Null(backlogItemTypeSchema);
        }
    }
}
