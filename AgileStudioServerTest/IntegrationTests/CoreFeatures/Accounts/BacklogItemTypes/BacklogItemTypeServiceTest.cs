using AgileStudioServer.CoreFeatures.Workflows.Workflows;
using AgileStudioServerTest.CoreFeatures.Workflows.Workflows;
using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.Accounts.BacklogItemTypes;
using AgileStudioServer.CoreFeatures.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemTypes;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.Accounts.BacklogItemTypes
{
    public class BacklogItemTypeServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemTypeService _backlogItemTypeService;

        private readonly BacklogItemTypeFixture _BacklogItemTypeFixture;

        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;

        private readonly WorkflowFixture _WorkflowFixture;

        public BacklogItemTypeServiceTest(
            DBContext dbContext,
            BacklogItemTypeService backlogItemTypeService,
            BacklogItemTypeFixture backlogItemTypeFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            WorkflowFixture workflowFixture) : base(dbContext)
        {
            _backlogItemTypeService = backlogItemTypeService;
            _BacklogItemTypeFixture = backlogItemTypeFixture;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _WorkflowFixture = workflowFixture;
        }

        [Fact]
        public void Create_ReturnsBacklogItemType()
        {
            BacklogItemTypeSchemaModel schema = _BacklogItemTypeSchemaFixture.Create();
            WorkflowModel workflow = _WorkflowFixture.Create(); ;
            BacklogItemTypeModel backlogItemType = new("Test BacklogItemType", schema.ID, workflow.ID);

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
        public void GetByBacklogItemTypeSchemaId_ReturnsBacklogItemTypes()
        {
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Create();
            var backlogItemTypes = new List<BacklogItemTypeModel>
            {
                _BacklogItemTypeFixture.Create(
                    "Test BacklogItemType 1",
                    backlogItemTypeSchema: backlogItemTypeSchema
                ),
                _BacklogItemTypeFixture.Create(
                    "Test BacklogItemType 2",
                    backlogItemTypeSchema: backlogItemTypeSchema
                )
            };

            List<BacklogItemTypeModel> returnedBacklogItemTypes = _backlogItemTypeService
                .GetByBacklogItemTypeSchemaId(backlogItemTypeSchema.ID);

            Assert.Equal(backlogItemTypes.Count, returnedBacklogItemTypes.Count);
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

            backlogItemType = _backlogItemTypeService.Get(backlogItemType.ID);
            Assert.Null(backlogItemType);
        }
    }
}
