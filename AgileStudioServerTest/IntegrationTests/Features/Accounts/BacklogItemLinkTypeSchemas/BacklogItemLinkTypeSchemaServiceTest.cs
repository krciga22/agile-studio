using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServerTest.Features.Accounts.Accounts;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypeSchemas;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemLinkTypeSchemaService _backlogItemLinkTypeSchemaService;

        private readonly BacklogItemLinkTypeSchemaFixture _BacklogItemLinkTypeSchemaFixture;

        private readonly AccountFixture _AccountFixture;

        public BacklogItemLinkTypeSchemaServiceTest(
            DBContext dbContext,
            BacklogItemLinkTypeSchemaService backlogItemLinkTypeSchemaService,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture,
            AccountFixture accountFixture) : base(dbContext)
        {
            _backlogItemLinkTypeSchemaService = backlogItemLinkTypeSchemaService;
            _BacklogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
            _AccountFixture = accountFixture;
        }

        [Fact]
        public void Create_ReturnsBacklogItemLinkTypeSchema()
        {
            AccountModel account = _AccountFixture.Create();
            BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchema = new("Test Schema", account.ID);

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

        // todo add test for GetAllByAccountId

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

            Assert.Throws<ModelNotFoundException>(() => 
                _backlogItemLinkTypeSchemaService.Get(backlogItemLinkTypeSchema.ID));
        }
    }
}
