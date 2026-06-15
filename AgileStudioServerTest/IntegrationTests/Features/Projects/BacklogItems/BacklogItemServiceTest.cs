using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.WorkflowStates;
using AgileStudioServer.Features.Projects.BacklogItems;
using AgileStudioServer.Features.Projects.Projects;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.WorkflowStates;
using AgileStudioServerTest.Features.Projects.BacklogItems;
using AgileStudioServerTest.Features.Projects.Projects;

namespace AgileStudioServerTest.IntegrationTests.Features.Projects.BacklogItems
{
    public class BacklogItemServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemService _backlogItemService;

        private readonly BacklogItemFixture _BacklogItemFixture;

        private readonly BacklogItemTypeFixture _BacklogItemTypeFixture;

        private readonly ProjectFixture _ProjectFixture;

        private readonly WorkflowStateFixture _WorkflowStateFixture;

        public BacklogItemServiceTest(
            DBContext dbContext,
            BacklogItemService backlogItemService,
            BacklogItemFixture backlogItemFixture,
            BacklogItemTypeFixture backlogItemTypeFixture,
            ProjectFixture projectFixture,
            WorkflowStateFixture workflowStateFixture) : base(dbContext)
        {
            _backlogItemService = backlogItemService;
            _BacklogItemFixture = backlogItemFixture;
            _BacklogItemTypeFixture = backlogItemTypeFixture;
            _ProjectFixture = projectFixture;
            _WorkflowStateFixture = workflowStateFixture;
        }

        [Fact]
        public void Create_ReturnsBacklogItem()
        {
            ProjectModel project = _ProjectFixture.Create();
            BacklogItemTypeModel backlogItemType = _BacklogItemTypeFixture.Create();
            WorkflowStateModel workflowState = _WorkflowStateFixture.Create();

            BacklogItemModel backlogItem = new(
                "Test BacklogItem",
                projectId: project.ID,
                backlogItemTypeId: backlogItemType.ID,
                workflowStateId: workflowState.ID
            );

            backlogItem = _backlogItemService.Create(backlogItem);

            Assert.NotNull(backlogItem);
            Assert.True(backlogItem.ID > 0);
        }

        [Fact]
        public void Get_ReturnsBacklogItem()
        {
            var backlogItem = _BacklogItemFixture.Create();

            var returnedBacklogItem = _backlogItemService.Get(backlogItem.ID);

            Assert.NotNull(returnedBacklogItem);
            Assert.Equal(backlogItem.ID, returnedBacklogItem.ID);
        }

        [Fact]
        public void GetParentBacklogItem_ReturnsBacklogItem()
        {
            var project = _ProjectFixture.Create();
            var parentBacklogItem = _BacklogItemFixture.Create(
                "Parent Backlog Item",
                project: project
            );

            var childBacklogItem = _BacklogItemFixture.Create(
                "Child BacklogItem",
                project: project,
                parentBacklogItem: parentBacklogItem
            );

            BacklogItemModel? returnedBacklogItem = _backlogItemService
                .GetParentBacklogItem(childBacklogItem.ID);

            Assert.NotNull(returnedBacklogItem);
            Assert.Equal(parentBacklogItem.ID, returnedBacklogItem.ID);
        }

        [Fact]
        public void GetAll_ReturnsAllBacklogItems()
        {
            var project = _ProjectFixture.Create();
            var backlogItems = new List<BacklogItemModel>
            {
                _BacklogItemFixture.Create("Test BacklogItem 1", project: project),
                _BacklogItemFixture.Create("Test BacklogItem 2", project: project)
            };

            PaginationResults<BacklogItemModel> returnedBacklogItems = _backlogItemService
                .GetByProjectId(project.ID);

            Assert.Equal(backlogItems.Count, returnedBacklogItems.Items.Count);
        }

        [Fact]
        public void GetByProjectIdAndBacklogItemTypeId_ReturnsBacklogItems()
        {
            var project = _ProjectFixture.Create();
            var backlogItemType = _BacklogItemTypeFixture.Create();
            var backlogItems = new List<BacklogItemModel>
            {
                _BacklogItemFixture.Create("Test BacklogItem 1", project: project, backlogItemType: backlogItemType),
                _BacklogItemFixture.Create("Test BacklogItem 2", project: project, backlogItemType: backlogItemType)
            };

            List<BacklogItemModel> returnedBacklogItems = _backlogItemService
                .GetByProjectIdAndBacklogItemTypeId(project.ID, backlogItemType.ID);

            Assert.Equal(backlogItems.Count, returnedBacklogItems.Count);
        }

        [Fact]
        public void GetChildBacklogItems_ReturnsBacklogItems()
        {
            var project = _ProjectFixture.Create();
            var parentBacklogItem = _BacklogItemFixture.Create(
                "Parent Backlog Item",
                project: project
            );
            var backlogItemTypeSchemaEntry = _BacklogItemTypeFixture.Create();
            var childBacklogItem1 = _BacklogItemFixture.Create(
                "Child BacklogItem 1",
                project: project,
                backlogItemType: backlogItemTypeSchemaEntry,
                parentBacklogItem: parentBacklogItem
            );
            var childBacklogItem2 = _BacklogItemFixture.Create(
                "Child BacklogItem 2",
                project: project,
                backlogItemType: backlogItemTypeSchemaEntry,
                parentBacklogItem: parentBacklogItem
            );

            var childBacklogItems = new List<BacklogItemModel>
            {
                childBacklogItem1,
                childBacklogItem2
            };

            PaginationResults<BacklogItemModel> results = _backlogItemService
                .GetChildBacklogItems(parentBacklogItem.ID);

            Assert.Equal(childBacklogItems.Count, results.Items.Count);

            foreach (var returnedBacklogItem in results.Items)
            {
                bool isChildBacklogItem = false;
                foreach (var childBacklogItem in childBacklogItems)
                {
                    if (childBacklogItem.ID == returnedBacklogItem.ID)
                    {
                        isChildBacklogItem = true;
                        break;
                    }
                }

                Assert.True(isChildBacklogItem);
            }
        }

        [Fact]
        public void Update_ReturnsUpdatedBacklogItem()
        {
            var backlogItem = _BacklogItemFixture.Create();
            var title = $"{backlogItem.Title} Updated";

            backlogItem.Title = title;
            backlogItem = _backlogItemService.Update(backlogItem);

            Assert.NotNull(backlogItem);
            Assert.Equal(title, backlogItem.Title);
        }

        [Fact]
        public void Delete_DeletesBacklogItem()
        {
            var backlogItem = _BacklogItemFixture.Create();

            _backlogItemService.Delete(backlogItem);

            Assert.Throws<ModelNotFoundException>(() => 
                _backlogItemService.Get(backlogItem.ID));
        }
    }
}
