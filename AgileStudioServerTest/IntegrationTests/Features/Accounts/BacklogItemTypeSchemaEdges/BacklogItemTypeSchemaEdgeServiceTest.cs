using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges;
using AgileStudioServerTest.Features.Accounts.Accounts;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemaEdges;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.BacklogItemTypeSchemaEdges
{
    public class BacklogItemTypeSchemaEdgeServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemTypeSchemaEdgeService _backlogItemTypeSchemaEdgeService;

        private readonly BacklogItemTypeSchemaEdgeFixture _BacklogItemTypeSchemaEdgeFixture;

        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;

        private readonly BacklogItemTypeFixture _BacklogItemTypeFixture;

        private readonly AccountFixture _AccountFixture;

        private readonly ServiceContext _ServiceContext;

        public BacklogItemTypeSchemaEdgeServiceTest(
            DBContext dbContext,
            BacklogItemTypeSchemaEdgeService backlogItemTypeSchemaEdgeService,
            BacklogItemTypeSchemaEdgeFixture backlogItemTypeSchemaEdgeFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemTypeFixture backlogItemTypeFixture,
            AccountFixture accountFixture,
            ServiceContext serviceContext) : base(dbContext)
        {
            _backlogItemTypeSchemaEdgeService = backlogItemTypeSchemaEdgeService;
            _BacklogItemTypeSchemaEdgeFixture = backlogItemTypeSchemaEdgeFixture;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _BacklogItemTypeFixture = backlogItemTypeFixture;
            _AccountFixture = accountFixture;
            _ServiceContext = serviceContext;
        }

        [Fact]
        public void Get_ReturnsBacklogItemTypeSchemaEdge()
        {
            var edge = _BacklogItemTypeSchemaEdgeFixture.Create();

            var returnedEdge = _backlogItemTypeSchemaEdgeService.Get(edge.ID);

            Assert.NotNull(returnedEdge);
            Assert.Equal(edge.ID, returnedEdge.ID);
        }

        [Fact]
        public void Get_WithFromTypeToTypeAndSchema_ReturnsBacklogItemTypeSchemaEdge()
        {
            var edge = _BacklogItemTypeSchemaEdgeFixture.Create();

            var returnedEdge = _backlogItemTypeSchemaEdgeService.Get(
                edge.FromTypeID, edge.ToTypeID, edge.SchemaID);

            Assert.NotNull(returnedEdge);
            Assert.Equal(edge.ID, returnedEdge.ID);
        }

        [Fact]
        public void GetByFromTypeId_WithFromTypeAndSchema_ReturnsBacklogItemTypeSchemaEdges()
        {
            var account = _AccountFixture.Create();
            var schema = _BacklogItemTypeSchemaFixture.Create(account: account);
            var storyType = _BacklogItemTypeFixture.Create("Story", account: account);
            var defectType = _BacklogItemTypeFixture.Create("Defect", account: account);
            var taskType = _BacklogItemTypeFixture.Create("Task", account: account);
            var testType = _BacklogItemTypeFixture.Create("Test", account: account);
            var storyTaskEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, storyType, taskType);
            var storyTestEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, storyType, testType);
            var defectTaskEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, defectType, taskType);
            var defectTestEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, defectType, testType);

            _ServiceContext.Sort = "id:asc";
            var returnedEdges = _backlogItemTypeSchemaEdgeService.GetByFromTypeId(
                storyType.ID, schema.ID);

            Assert.Collection(returnedEdges,
                edge => Assert.Equal(storyTaskEdge.ID, edge.ID),
                edge => Assert.Equal(storyTestEdge.ID, edge.ID));
        }

        [Fact]
        public void GetByFromTypeId_WithNullFromTypeAndSchema_ReturnsBacklogItemTypeSchemaEdges()
        {
            var account = _AccountFixture.Create();
            var schema = _BacklogItemTypeSchemaFixture.Create(account: account);
            var storyType = _BacklogItemTypeFixture.Create("Story", account: account);
            var defectType = _BacklogItemTypeFixture.Create("Defect", account: account);
            var taskType = _BacklogItemTypeFixture.Create("Task", account: account);
            var testType = _BacklogItemTypeFixture.Create("Test", account: account);

            var storyEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, null, storyType);
            var defectEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, null, defectType);
            var storyTaskEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, storyType, taskType);
            var storyTestEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, storyType, testType);
            var defectTaskEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, defectType, taskType);
            var defectTestEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, defectType, testType);

            _ServiceContext.Sort = "id:asc";
            var returnedEdges = _backlogItemTypeSchemaEdgeService.GetByFromTypeId(
                null, schema.ID);

            Assert.Collection(returnedEdges,
                edge => Assert.Equal(storyEdge.ID, edge.ID),
                edge => Assert.Equal(defectEdge.ID, edge.ID));
        }

        [Fact]
        public void GetByToTypeId_WithFromTypeAndSchema_ReturnsBacklogItemTypeSchemaEdges()
        {
            var account = _AccountFixture.Create();
            var schema = _BacklogItemTypeSchemaFixture.Create(account: account);
            var storyType = _BacklogItemTypeFixture.Create("Story", account: account);
            var defectType = _BacklogItemTypeFixture.Create("Defect", account: account);
            var taskType = _BacklogItemTypeFixture.Create("Task", account: account);
            var testType = _BacklogItemTypeFixture.Create("Test", account: account);
            var storyTaskEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, storyType, taskType);
            var storyTestEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, storyType, testType);
            var defectTaskEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, defectType, taskType);
            var defectTestEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, defectType, testType);

            _ServiceContext.Sort = "id:asc";
            var returnedEdges = _backlogItemTypeSchemaEdgeService.GetByToTypeId(
                taskType.ID, schema.ID);

            Assert.Collection(returnedEdges,
                edge => Assert.Equal(storyTaskEdge.ID, edge.ID),
                edge => Assert.Equal(defectTaskEdge.ID, edge.ID));
        }

        [Fact]
        public void Create_WithValidData_ReturnsBacklogItemTypeSchemaEdge()
        {
            var account = _AccountFixture.Create();
            var schema = _BacklogItemTypeSchemaFixture.Create(account: account);
            var fromType = _BacklogItemTypeFixture.Create("Story", account: account);
            var toType = _BacklogItemTypeFixture.Create("Defect", account: account);

            var backlogItemTypeSchemaEdge =
                new BacklogItemTypeSchemaEdgeModel(schema.ID, fromType.ID, toType.ID);

            backlogItemTypeSchemaEdge = _backlogItemTypeSchemaEdgeService.Create(backlogItemTypeSchemaEdge);

            Assert.NotNull(backlogItemTypeSchemaEdge);
            Assert.True(backlogItemTypeSchemaEdge.ID > 0);
        }

        [Fact]
        public void Create_WithValidDataDeep_ReturnsBacklogItemTypeSchemaEdge()
        {
            var account = _AccountFixture.Create();
            var schema = _BacklogItemTypeSchemaFixture.Create(account: account);
            var epicType = _BacklogItemTypeFixture.Create("Epic", account: account);
            var storyType = _BacklogItemTypeFixture.Create("Story", account: account);
            var defectType = _BacklogItemTypeFixture.Create("Defect", account: account);
            var taskType = _BacklogItemTypeFixture.Create("Task", account: account);
            var testType = _BacklogItemTypeFixture.Create("Test", account: account);
            var subTaskType = _BacklogItemTypeFixture.Create("Sub-Task", account: account);

            List<BacklogItemTypeSchemaEdgeModel> edges = [
                new BacklogItemTypeSchemaEdgeModel(
                    schema.ID, epicType.ID, storyType.ID),
                new BacklogItemTypeSchemaEdgeModel(
                    schema.ID, epicType.ID, defectType.ID),
                new BacklogItemTypeSchemaEdgeModel(
                    schema.ID, storyType.ID, taskType.ID),
                new BacklogItemTypeSchemaEdgeModel(
                    schema.ID, storyType.ID, testType.ID),
                new BacklogItemTypeSchemaEdgeModel(
                    schema.ID, defectType.ID, taskType.ID),
                new BacklogItemTypeSchemaEdgeModel(
                    schema.ID, defectType.ID, testType.ID),
                new BacklogItemTypeSchemaEdgeModel(
                    schema.ID, taskType.ID, subTaskType.ID),
                new BacklogItemTypeSchemaEdgeModel(
                    schema.ID, testType.ID, subTaskType.ID)
            ];

            List<BacklogItemTypeSchemaEdgeModel> createdEdges = [];
            edges.ForEach(e => createdEdges.Add(
                _backlogItemTypeSchemaEdgeService.Create(e)));

            for (int i = 0; i < edges.Count; i++) {
                BacklogItemTypeSchemaEdgeModel edge = edges[i];
                BacklogItemTypeSchemaEdgeModel createdEdge = createdEdges[i];
                Assert.Equal(edge.SchemaID, createdEdge.SchemaID);
                Assert.Equal(edge.FromTypeID, createdEdge.FromTypeID);
                Assert.Equal(edge.ToTypeID, createdEdge.ToTypeID);
            }
        }

        [Fact]
        public void Create_WithSchemaFromAnotherAccount_ThrowsException()
        {
            var account1 = _AccountFixture.Create();
            var account2 = _AccountFixture.Create();
            var schema = _BacklogItemTypeSchemaFixture.Create(account: account2);
            var fromType = _BacklogItemTypeFixture.Create("Story", account: account1);
            var toType = _BacklogItemTypeFixture.Create("Defect", account: account1);

            var backlogItemTypeSchemaEdge =
                new BacklogItemTypeSchemaEdgeModel(schema.ID, fromType.ID, toType.ID);

            Assert.Throws<InvalidOperationException>(() =>
                _backlogItemTypeSchemaEdgeService.Create(backlogItemTypeSchemaEdge));
        }

        [Fact]
        public void Create_WithFromTypeFromAnotherAccount_ThrowsException()
        {
            var account1 = _AccountFixture.Create();
            var account2 = _AccountFixture.Create();
            var schema = _BacklogItemTypeSchemaFixture.Create(account: account1);
            var fromType = _BacklogItemTypeFixture.Create("Story", account: account2);
            var toType = _BacklogItemTypeFixture.Create("Defect", account: account1);

            var backlogItemTypeSchemaEdge =
                new BacklogItemTypeSchemaEdgeModel(schema.ID, fromType.ID, toType.ID);

            Assert.Throws<InvalidOperationException>(() =>
                _backlogItemTypeSchemaEdgeService.Create(backlogItemTypeSchemaEdge));
        }

        [Fact]
        public void Create_WithToTypeFromAnotherAccount_ThrowsException()
        {
            var account1 = _AccountFixture.Create();
            var account2 = _AccountFixture.Create();
            var schema = _BacklogItemTypeSchemaFixture.Create(account: account1);
            var fromType = _BacklogItemTypeFixture.Create("Story", account: account1);
            var toType = _BacklogItemTypeFixture.Create("Defect", account: account2);

            var backlogItemTypeSchemaEdge =
                new BacklogItemTypeSchemaEdgeModel(schema.ID, fromType.ID, toType.ID);

            Assert.Throws<InvalidOperationException>(() =>
                _backlogItemTypeSchemaEdgeService.Create(backlogItemTypeSchemaEdge));
        }

        [Fact]
        public void Create_WithSameFromTypeAndToType_ThrowsException()
        {
            var account = _AccountFixture.Create();
            var schema = _BacklogItemTypeSchemaFixture.Create(account: account);
            var fromType = _BacklogItemTypeFixture.Create("Story", account: account);

            var backlogItemTypeSchemaEdge =
                new BacklogItemTypeSchemaEdgeModel(schema.ID, fromType.ID, fromType.ID);

            Assert.Throws<InvalidOperationException>(() =>
                _backlogItemTypeSchemaEdgeService.Create(backlogItemTypeSchemaEdge));
        }

        [Fact]
        public void Create_WithCycle_ThrowsException()
        {
            var account = _AccountFixture.Create();
            var schema = _BacklogItemTypeSchemaFixture.Create(account: account);
            var storyType = _BacklogItemTypeFixture.Create("Story", account: account);
            var taskType = _BacklogItemTypeFixture.Create("Task", account: account);
            var existingEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, storyType, taskType);

            var newEdge =
                new BacklogItemTypeSchemaEdgeModel(schema.ID, taskType.ID, storyType.ID);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                _backlogItemTypeSchemaEdgeService.Create(newEdge));

            Assert.Contains(
                "Cycle detected in backlog item type schema graph.", ex.Message);
        }

        [Fact]
        public void Create_WithDeepCycle_ThrowsException()
        {
            var account = _AccountFixture.Create();
            var schema = _BacklogItemTypeSchemaFixture.Create(account: account);
            var storyType = _BacklogItemTypeFixture.Create("Story", account: account);
            var taskType = _BacklogItemTypeFixture.Create("Task", account: account);
            var subTaskType = _BacklogItemTypeFixture.Create("Sub-Task", account: account);
            var storyTaskEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, storyType, taskType);
            var taskSubTaskEdge = _BacklogItemTypeSchemaEdgeFixture.Create(schema, taskType, subTaskType);

            var subTaskStoryEdge =
                new BacklogItemTypeSchemaEdgeModel(schema.ID, subTaskType.ID, storyType.ID);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                _backlogItemTypeSchemaEdgeService.Create(subTaskStoryEdge));

            Assert.Contains(
                "Cycle detected in backlog item type schema graph.", ex.Message);
        }

        [Fact]
        public void Delete_DeletesBacklogItemTypeSchemaEdge()
        {
            var edge = _BacklogItemTypeSchemaEdgeFixture.Create();

            _backlogItemTypeSchemaEdgeService.Delete(edge);

            Assert.Throws<ModelNotFoundException>(() =>
                _backlogItemTypeSchemaEdgeService.Get(edge.ID));
        }
    }
}
