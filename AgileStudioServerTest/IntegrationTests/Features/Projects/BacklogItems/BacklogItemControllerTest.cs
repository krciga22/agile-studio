using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.Core.APIs.DTOs;
using AgileStudioServer.Features.Projects.BacklogItems;
using AgileStudioServer.Data;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Accounts.WorkflowStates;
using AgileStudioServerTest.Features.Projects.BacklogItems;
using AgileStudioServerTest.Features.Projects.Projects;

namespace AgileStudioServerTest.IntegrationTests.Features.Projects.BacklogItems
{
    public class BacklogItemControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemController _Controller;

        private readonly BacklogItemFixture _BacklogItemFixture;

        private readonly BacklogItemTypeFixture _BacklogItemTypeFixture;

        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;

        private readonly ProjectFixture _ProjectFixture;

        private readonly WorkflowStateFixture _WorkflowStateFixture;

        public BacklogItemControllerTest(
            DBContext dbContext,
            BacklogItemController controller,
            BacklogItemFixture backlogItemFixture,
            BacklogItemTypeFixture backlogItemTypeFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            ProjectFixture projectFixture,
            WorkflowStateFixture workflowStateFixture) : base(dbContext)
        {
            _Controller = controller;
            _BacklogItemFixture = backlogItemFixture;
            _BacklogItemTypeFixture = backlogItemTypeFixture;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _ProjectFixture = projectFixture;
            _WorkflowStateFixture = workflowStateFixture;
        }

        [Fact]
        public void GetChildBacklogItems_WithId_ReturnsDtos()
        {
            var project = _ProjectFixture.Create();
            var parentBacklogItem = _BacklogItemFixture.Create(
                "Parent Backlog Item",
                project: project
            );
            var childBacklogItemType = _BacklogItemTypeFixture.Create();
            var childBacklogItem1 = _BacklogItemFixture.Create(
                "Child BacklogItem 1",
                project: project,
                backlogItemType: childBacklogItemType,
                parentBacklogItem: parentBacklogItem
            );
            var childBacklogItem2 = _BacklogItemFixture.Create(
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

            PaginatedResultsDto<BacklogItemDto, BacklogItemModel>? results = null;
            IActionResult result = _Controller.GetChildBacklogItems(parentBacklogItem.ID);
            if (result is OkObjectResult okResult)
            {
                results = okResult.Value as PaginatedResultsDto<BacklogItemDto, BacklogItemModel>;
            }

            Assert.IsType<PaginatedResultsDto<BacklogItemDto, BacklogItemModel>>(results);
            Assert.Equal(childBacklogItems.Count, results.Items.Count);

            foreach (var dto in results.Items)
            {
                bool isChildBacklogItem = false;
                foreach (var childBacklogItem in childBacklogItems)
                {
                    if (childBacklogItem.ID == dto.ID)
                    {
                        isChildBacklogItem = true;
                        break;
                    }
                }

                Assert.True(isChildBacklogItem);
            }
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var backlogItem = _BacklogItemFixture.Create();

            BacklogItemDto? dto = null;
            IActionResult result = _Controller.Get(backlogItem.ID);
            if (result is OkObjectResult okResult)
            {
                dto = okResult.Value as BacklogItemDto;
            }

            Assert.IsType<BacklogItemDto>(dto);
            Assert.Equal(backlogItem.ID, dto.ID);
        }

        [Fact]
        public void GetParentBacklogItem_WithId_ReturnsDto()
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

            BacklogItemDto? dto = null;
            IActionResult result = _Controller.GetParentBacklogItem(childBacklogItem.ID);
            if (result is OkObjectResult okResult)
            {
                dto = okResult.Value as BacklogItemDto;
            }

            Assert.IsType<BacklogItemDto>(dto);
            Assert.Equal(parentBacklogItem.ID, dto.ID);
        }

        [Fact]
        public void GetParentBacklogItem_WithInvalidId_ReturnsNotFoundResult()
        {
            var project = _ProjectFixture.Create();
            var backlogItem = _BacklogItemFixture.Create(
                "Test BacklogItem",
                project: project
            );

            IActionResult result = _Controller.GetParentBacklogItem(backlogItem.ID);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public void GetParentBacklogItem_WithNonExistantId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.GetParentBacklogItem(Constants.NonExistantId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public void Post_WithDto_ReturnsDto()
        {
            var project = _ProjectFixture.Create();
            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Get(
                project.BacklogItemTypeSchemaID);
            var backlogItemType = _BacklogItemTypeFixture.Create(
                    backlogItemTypeSchema: backlogItemTypeSchema);
            var workflowState = _WorkflowStateFixture.Create();
            var postDto = new BacklogItemPostDto("Test Backlog Item Type Schema", project.ID, backlogItemType.ID, workflowState.ID);

            BacklogItemDto? dto = null;
            IActionResult result = _Controller.Post(postDto);
            if (result is CreatedResult createdResult)
            {
                dto = createdResult.Value as BacklogItemDto;
            }

            Assert.IsType<BacklogItemDto>(dto);
            Assert.Equal(postDto.Title, dto.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var backlogItem = _BacklogItemFixture.Create();
            var title = $"{backlogItem.Title} Updated";
            var patchDto = new BacklogItemPatchDto(backlogItem.ID, title, backlogItem.WorkflowStateID);

            IActionResult result = _Controller.Patch(backlogItem.ID, patchDto);
            BacklogItemDto? dto = null;
            if (result is OkObjectResult okObjectResult)
            {
                dto = okObjectResult.Value as BacklogItemDto;
            }

            Assert.IsType<BacklogItemDto>(dto);
            Assert.Equal(patchDto.Title, dto.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var backlogItem = _BacklogItemFixture.Create();

            IActionResult result = _Controller.Delete(backlogItem.ID);

            Assert.IsType<OkResult>(result as OkResult);
        }

        [Fact]
        public void Delete_WithInvalidId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.Delete(Constants.NonExistantId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }
    }
}
