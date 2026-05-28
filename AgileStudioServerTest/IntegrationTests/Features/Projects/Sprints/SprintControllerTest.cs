using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Auth.Roles;
using AgileStudioServer.Features.Projects.Sprints;
using AgileStudioServer.Features.Resources.Resource;
using AgileStudioServerTest.Features.Projects.Projects;
using AgileStudioServerTest.Features.Projects.Sprints;
using AgileStudioServerTest.Features.Users.Users;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AgileStudioServerTest.IntegrationTests.Features.Projects.Sprints
{
    public class SprintControllerTest : ResourceControllerTest
    {
        private readonly ResourceController _ResourceController;

        private readonly SprintController _Controller;

        private readonly SprintFixture _SprintFixture;

        private readonly ProjectFixture _ProjectFixture;

        private readonly UserFixture _UserFixture;

        public SprintControllerTest(
            DBContext dbContext,
            ResourceController resourceController, // TODO move to controller test base class
            SprintController controller,
            SprintFixture sprintFixture,
            ProjectFixture projectFixture,
            UserFixture userFixture,
            ServiceContext serviceContext,
            IUrlHelperFactory? iUrlHelperFactory = null) : 
            base(dbContext, serviceContext, iUrlHelperFactory)
        {
            _ResourceController = resourceController;
            _Controller = controller;
            _SprintFixture = sprintFixture;
            _ProjectFixture = projectFixture;
            _UserFixture = userFixture;
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var user = _UserFixture.Create();

            var sprint = _SprintFixture.Create(createdBy: user);
            _ProjectFixture.GrantAccess(sprint.ProjectID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            InitHttpAndServiceContextWithUser(user);
            object[] id = [sprint.ID];
            var result = _ResourceController.Get(
                _HttpContext, ResourceTypes.SprintsSprint, id);

            var objectResult =
                Assert.IsType<Ok<object>>(result);

            var sprintDtoResult =
                Assert.IsType<SprintDto>(objectResult.Value);

            Assert.Equal(sprint.ID, sprintDtoResult.ID);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var user = _UserFixture.Create();

            var sprint = _SprintFixture.Create(createdBy: user);

            _ProjectFixture.GrantAccess(
                sprint.ProjectID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            var sprintPatchDto = new SprintPatchDto(sprint.ID)
            {
                Description = "Updated Description"
            };
            object data = IntegrationTestsUtil.ConvertDtoToObject(sprintPatchDto);

            InitHttpAndServiceContextWithUser(user);
            object[] id = [sprint.ID];
            var result = _ResourceController.Patch(
                _HttpContext, ResourceTypes.SprintsSprint, id, data);

            var objectResult =
                Assert.IsType<Ok<object>>(result);

            var sprintDtoResult =
                Assert.IsType<SprintDto>(objectResult.Value);

            Assert.Equal(sprintPatchDto.Description, sprintDtoResult.Description);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var user = _UserFixture.Create();

            var sprint = _SprintFixture.Create(createdBy: user);

            _ProjectFixture.GrantAccess(
                sprint.ProjectID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            InitHttpAndServiceContextWithUser(user);
            object[] id = [sprint.ID];
            var result = _ResourceController.Delete(
                _HttpContext, ResourceTypes.SprintsSprint, id);

            Assert.IsType<Ok>(result);
        }
    }
}
