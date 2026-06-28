using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Accounts.Workflows;
using AgileStudioServerTest.Features.Accounts.Accounts;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemaEdges;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Accounts.Workflows;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.BacklogItemTypes
{
    public class BacklogItemTypeServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemTypeService _backlogItemTypeService;

        private readonly BacklogItemTypeFixture _BacklogItemTypeFixture;

        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;

        private readonly BacklogItemTypeSchemaEdgeFixture _BacklogItemTypeSchemaEdgeFixture;

        private readonly WorkflowFixture _WorkflowFixture;

        private readonly AccountFixture _AccountFixture;

        private readonly ServiceContext _ServiceContext;

        public BacklogItemTypeServiceTest(
            DBContext dbContext,
            BacklogItemTypeService backlogItemTypeService,
            BacklogItemTypeFixture backlogItemTypeFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemTypeSchemaEdgeFixture backlogItemTypeSchemaEdgeFixture,
            WorkflowFixture workflowFixture,
            AccountFixture accountFixture,
            ServiceContext serviceContext) : base(dbContext)
        {
            _backlogItemTypeService = backlogItemTypeService;
            _BacklogItemTypeFixture = backlogItemTypeFixture;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _BacklogItemTypeSchemaEdgeFixture = backlogItemTypeSchemaEdgeFixture;
            _WorkflowFixture = workflowFixture;
            _AccountFixture = accountFixture;
            _ServiceContext = serviceContext;
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
        public void GetForSchemaAndFromBacklogItemType_WithNullFromBacklogItemType_ReturnsBacklogItemTypesForSchema()
        {
            var account = _AccountFixture.Create();
            var schema = _BacklogItemTypeSchemaFixture.Create(account: account);
            var epicType = _BacklogItemTypeFixture.Create("Epic", account: account);
            var storyType = _BacklogItemTypeFixture.Create("Story", account: account);
            var defectType = _BacklogItemTypeFixture.Create("Defect", account: account);
            var taskType = _BacklogItemTypeFixture.Create("Task", account: account);
            var testType = _BacklogItemTypeFixture.Create("Test", account: account);

            var epicEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, null, epicType);
            var storyEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, null, storyType);
            var defectEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, null, defectType);
            var storyTaskEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, storyType, taskType);
            var storyTestEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, storyType, testType);
            var defectTaskEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, defectType, taskType);
            var defectTestEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, defectType, testType);

            _ServiceContext.Sort = "id:asc";
            var returnedBacklogItemTypes = _backlogItemTypeService.GetForSchemaAndFromBacklogItemType(schema.ID);

            Assert.Collection(returnedBacklogItemTypes,
                backlogItemType => Assert.Equal(epicType.ID, backlogItemType.ID),
                backlogItemType => Assert.Equal(storyType.ID, backlogItemType.ID),
                backlogItemType => Assert.Equal(defectType.ID, backlogItemType.ID));
        }

        [Fact]
        public void GetForSchemaAndFromBacklogItemType_WithFromBacklogItemType_ReturnsBacklogItemTypesForSchema()
        {
            var account = _AccountFixture.Create();
            var schema = _BacklogItemTypeSchemaFixture.Create(account: account);
            var epicType = _BacklogItemTypeFixture.Create("Epic", account: account);
            var storyType = _BacklogItemTypeFixture.Create("Story", account: account);
            var defectType = _BacklogItemTypeFixture.Create("Defect", account: account);
            var taskType = _BacklogItemTypeFixture.Create("Task", account: account);
            var testType = _BacklogItemTypeFixture.Create("Test", account: account);

            var epicEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, null, epicType);
            var storyEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, null, storyType);
            var defectEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, null, defectType);
            var storyTaskEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, storyType, taskType);
            var storyTestEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, storyType, testType);
            var defectTaskEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, defectType, taskType);
            var defectTestEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, defectType, testType);

            _ServiceContext.Sort = "id:asc";
            var returnedBacklogItemTypes = _backlogItemTypeService.GetForSchemaAndFromBacklogItemType(
                schema.ID, storyType.ID);

            Assert.Collection(returnedBacklogItemTypes,
                backlogItemType => Assert.Equal(taskType.ID, backlogItemType.ID),
                backlogItemType => Assert.Equal(testType.ID, backlogItemType.ID));
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
