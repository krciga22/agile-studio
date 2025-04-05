using AgileStudioServer.Data;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.CoreFeatures.Projects.Services.Models;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItems;
using AgileStudioServer.CoreFeatures.Workflows.WorkflowStates;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.BacklogItems.BacklogItems
{
    public class BacklogItemServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemService _backlogItemService;

        public BacklogItemServiceTest(
            DBContext dbContext,
            ModelFixtures fixtures,
            BacklogItemService backlogItemService) : base(dbContext, fixtures)
        {
            _backlogItemService = backlogItemService;
        }

        [Fact]
        public void Create_ReturnsBacklogItem()
        {
            Project project = _Fixtures.CreateProject();
            BacklogItemTypeModel backlogItemType = _Fixtures.CreateBacklogItemType();
            WorkflowStateModel workflowState = _Fixtures.CreateWorkflowState();

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
            var backlogItem = _Fixtures.CreateBacklogItem();

            var returnedBacklogItem = _backlogItemService.Get(backlogItem.ID);

            Assert.NotNull(returnedBacklogItem);
            Assert.Equal(backlogItem.ID, returnedBacklogItem.ID);
        }

        [Fact]
        public void GetParentBacklogItem_ReturnsBacklogItem()
        {
            var project = _Fixtures.CreateProject();
            var parentBacklogItem = _Fixtures.CreateBacklogItem(
                "Parent Backlog Item",
                project: project
            );

            var childBacklogItem = _Fixtures.CreateBacklogItem(
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
            var project = _Fixtures.CreateProject();
            var backlogItems = new List<BacklogItemModel>
            {
                _Fixtures.CreateBacklogItem("Test BacklogItem 1", project: project),
                _Fixtures.CreateBacklogItem("Test BacklogItem 2", project: project)
            };

            List<BacklogItemModel> returnedBacklogItems = _backlogItemService
                .GetByProjectId(project.ID);

            Assert.Equal(backlogItems.Count, returnedBacklogItems.Count);
        }

        [Fact]
        public void GetByProjectIdAndBacklogItemTypeId_ReturnsBacklogItems()
        {
            var project = _Fixtures.CreateProject();
            var backlogItemType = _Fixtures.CreateBacklogItemType();
            var backlogItems = new List<BacklogItemModel>
            {
                _Fixtures.CreateBacklogItem("Test BacklogItem 1", project: project, backlogItemType: backlogItemType),
                _Fixtures.CreateBacklogItem("Test BacklogItem 2", project: project, backlogItemType: backlogItemType)
            };

            List<BacklogItemModel> returnedBacklogItems = _backlogItemService
                .GetByProjectIdAndBacklogItemTypeId(project.ID, backlogItemType.ID);

            Assert.Equal(backlogItems.Count, returnedBacklogItems.Count);
        }

        [Fact]
        public void GetChildBacklogItems_ReturnsBacklogItems()
        {
            var project = _Fixtures.CreateProject();
            var parentBacklogItem = _Fixtures.CreateBacklogItem(
                "Parent Backlog Item",
                project: project
            );
            var childBacklogItemType = _Fixtures.CreateBacklogItemType();
            var childBacklogItem1 = _Fixtures.CreateBacklogItem(
                "Child BacklogItem 1",
                project: project,
                backlogItemType: childBacklogItemType,
                parentBacklogItem: parentBacklogItem
            );
            var childBacklogItem2 = _Fixtures.CreateBacklogItem(
                "Child BacklogItem 2",
                project: project,
                backlogItemType: childBacklogItemType,
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
            var backlogItem = _Fixtures.CreateBacklogItem();
            var title = $"{backlogItem.Title} Updated";

            backlogItem.Title = title;
            backlogItem = _backlogItemService.Update(backlogItem);

            Assert.NotNull(backlogItem);
            Assert.Equal(title, backlogItem.Title);
        }

        [Fact]
        public void Delete_DeletesBacklogItem()
        {
            var backlogItem = _Fixtures.CreateBacklogItem();

            _backlogItemService.Delete(backlogItem);

            backlogItem = _backlogItemService.Get(backlogItem.ID);
            Assert.Null(backlogItem);
        }
    }
}
