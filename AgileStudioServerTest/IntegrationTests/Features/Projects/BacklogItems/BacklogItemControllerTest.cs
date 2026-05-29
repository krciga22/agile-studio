using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Auth.Roles;
using AgileStudioServer.Features.Projects.BacklogItems;
using AgileStudioServer.Features.Resources.Resource;
using AgileStudioServerTest.Features.Projects.BacklogItems;
using AgileStudioServerTest.Features.Projects.Projects;
using AgileStudioServerTest.Features.Users.Users;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AgileStudioServerTest.IntegrationTests.Features.Projects.BacklogItems
{
    public class BacklogItemControllerTest : ResourceControllerTest
    {
        private readonly BacklogItemController _Controller;

        private readonly BacklogItemFixture _BacklogItemFixture;

        private readonly ProjectFixture _ProjectFixture;

        private readonly UserFixture _UserFixture;

        public BacklogItemControllerTest(
            DBContext dbContext,
            ResourceController resourceController,
            ServiceContext serviceContext,
            BacklogItemController controller,
            BacklogItemFixture backlogItemFixture,
            ProjectFixture projectFixture,
            UserFixture userFixture,
            IUrlHelperFactory? iUrlHelperFactory = null) : 
            base(dbContext, resourceController, serviceContext, iUrlHelperFactory)
        {
            _Controller = controller;
            _BacklogItemFixture = backlogItemFixture;
            _ProjectFixture = projectFixture;
            _UserFixture = userFixture;
        }

        [Fact]
        public void GetChildBacklogItems_WithId_ReturnsDtos()
        {
            // todo
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var user = _UserFixture.Create();

            var backlogItem = _BacklogItemFixture.Create("Test BacklogItem", createdBy: user);
            
            _ProjectFixture.GrantAccess(backlogItem.ProjectID,
                user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            InitHttpAndServiceContextWithUser(user);
            object[] id = [backlogItem.ID];
            var result = _ResourceController.Get(
                _HttpContext, ResourceTypes.BacklogItemsBacklogItem, id);

            var objectResult =
                Assert.IsType<Ok<object>>(result);

            var backlogItemDtoResult =
                Assert.IsType<BacklogItemDto>(objectResult.Value);

            Assert.Equal(backlogItem.ID, backlogItemDtoResult.ID);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var user = _UserFixture.Create();

            var backlogItem = _BacklogItemFixture.Create(createdBy: user);
            
            _ProjectFixture.GrantAccess(backlogItem.ProjectID,
                user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            var backlogItemPatchDto = new BacklogItemPatchDto(
                backlogItem.ID, "Updated Title", backlogItem.WorkflowStateID);

            object data = IntegrationTestsUtil.ConvertDtoToObject(backlogItemPatchDto);

            InitHttpAndServiceContextWithUser(user);
            object[] id = [backlogItem.ID];
            var result = _ResourceController.Patch(
                _HttpContext, ResourceTypes.BacklogItemsBacklogItem, id, data);

            var objectResult =
                Assert.IsType<Ok<object>>(result);

            var backlogItemDtoResult =
                Assert.IsType<BacklogItemDto>(objectResult.Value);

            Assert.Equal(backlogItemPatchDto.Title, backlogItemDtoResult.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var user = _UserFixture.Create();

            var backlogItem = _BacklogItemFixture.Create(createdBy: user);
            
            _ProjectFixture.GrantAccess(backlogItem.ProjectID,
                user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            InitHttpAndServiceContextWithUser(user);
            object[] id = [backlogItem.ID];
            var result = _ResourceController.Delete(
                _HttpContext, ResourceTypes.BacklogItemsBacklogItem, id);

            Assert.IsType<Ok>(result);
        }
    }
}
