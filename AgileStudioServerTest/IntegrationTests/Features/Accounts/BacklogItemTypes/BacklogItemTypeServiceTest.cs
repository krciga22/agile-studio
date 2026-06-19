using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Accounts.Workflows;
using AgileStudioServerTest.Features.Accounts.Accounts;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Accounts.Workflows;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.BacklogItemTypes
{
    public class BacklogItemTypeServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemTypeService _backlogItemTypeService;

        private readonly BacklogItemTypeFixture _BacklogItemTypeFixture;

        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;

        private readonly WorkflowFixture _WorkflowFixture;

        private readonly AccountFixture _AccountFixture;

        public BacklogItemTypeServiceTest(
            DBContext dbContext,
            BacklogItemTypeService backlogItemTypeService,
            BacklogItemTypeFixture backlogItemTypeFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            WorkflowFixture workflowFixture,
            AccountFixture accountFixture) : base(dbContext)
        {
            _backlogItemTypeService = backlogItemTypeService;
            _BacklogItemTypeFixture = backlogItemTypeFixture;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _WorkflowFixture = workflowFixture;
            _AccountFixture = accountFixture;
        }

        [Fact]
        public void Create_ReturnsBacklogItemType()
        {
            AccountModel account = _AccountFixture.Create();
            BacklogItemTypeSchemaModel schema = _BacklogItemTypeSchemaFixture.Create();
            WorkflowModel workflow = _WorkflowFixture.Create(); ;
            BacklogItemTypeModel backlogItemType = new(
                "Test BacklogItemType", account.ID, workflow.ID);

            backlogItemType = _backlogItemTypeService.Create(backlogItemType);

            Assert.NotNull(backlogItemType);
            Assert.True(backlogItemType.ID > 0);
        }

        [Fact]
        public void Get_ReturnsBacklogItemType()
        {
            var backlogItemType = _BacklogItemTypeFixture.Create();

            var returnedBacklogItemType = _backlogItemTypeService.Get(backlogItemType.ID);

            Assert.NotNull(returnedBacklogItemType);
            Assert.Equal(backlogItemType.ID, returnedBacklogItemType.ID);
        }

        [Fact]
        public void Update_ReturnsUpdatedBacklogItemType()
        {
            var backlogItemType = _BacklogItemTypeFixture.Create();
            var title = $"{backlogItemType.Title} Updated";

            backlogItemType.Title = title;
            backlogItemType = _backlogItemTypeService.Update(backlogItemType);

            Assert.NotNull(backlogItemType);
            Assert.Equal(title, backlogItemType.Title);
        }

        [Fact]
        public void Delete_DeletesBacklogItemType()
        {
            var backlogItemType = _BacklogItemTypeFixture.Create();

            _backlogItemTypeService.Delete(backlogItemType);

            Assert.Throws<ModelNotFoundException>(() => 
                _backlogItemTypeService.Get(backlogItemType.ID));
        }
    }
}
