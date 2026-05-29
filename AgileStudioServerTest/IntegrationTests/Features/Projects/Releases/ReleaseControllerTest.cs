using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Auth.Roles;
using AgileStudioServer.Features.Projects.Releases;
using AgileStudioServer.Features.Resources.Resource;
using AgileStudioServerTest.Features.Projects.Projects;
using AgileStudioServerTest.Features.Projects.Releases;
using AgileStudioServerTest.Features.Users.Users;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AgileStudioServerTest.IntegrationTests.Features.Projects.Releases
{
    public class ReleaseControllerTest : ResourceControllerTest
    {
        private readonly ReleaseController _Controller;

        private readonly ReleaseFixture _ReleaseFixture;

        private readonly ProjectFixture _ProjectFixture;

        private readonly UserFixture _UserFixture;

        public ReleaseControllerTest(
            DBContext dbContext,
            ResourceController resourceController,
            ServiceContext serviceContext,
            ReleaseController controller,
            ReleaseFixture releaseFixture,
            ProjectFixture projectFixture,
            UserFixture userFixture,
            IUrlHelperFactory? iUrlHelperFactory = null) : 
            base(dbContext, resourceController, serviceContext, iUrlHelperFactory)
        {
            _Controller = controller;
            _ReleaseFixture = releaseFixture;
            _ProjectFixture = projectFixture;
            _UserFixture = userFixture;
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var user = _UserFixture.Create();

            var release = _ReleaseFixture.Create("Test Release", createdBy: user);
            _ProjectFixture.GrantAccess(release.ProjectID, 
                user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            InitHttpAndServiceContextWithUser(user);
            object[] id = [release.ID];
            var result = _ResourceController.Get(
                _HttpContext, ResourceTypes.ReleasesRelease, id);

            var objectResult =
                Assert.IsType<Ok<object>>(result);

            var releaseDtoResult =
                Assert.IsType<ReleaseDto>(objectResult.Value);

            Assert.Equal(release.ID, releaseDtoResult.ID);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var user = _UserFixture.Create();

            var release = _ReleaseFixture.Create(createdBy: user);

            _ProjectFixture.GrantAccess(release.ProjectID, 
                user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            var releasePatchDto = new ReleasePatchDto(release.ID, "Updated Title");
            object data = IntegrationTestsUtil.ConvertDtoToObject(releasePatchDto);

            InitHttpAndServiceContextWithUser(user);
            object[] id = [release.ID];
            var result = _ResourceController.Patch(
                _HttpContext, ResourceTypes.ReleasesRelease, id, data);

            var objectResult =
                Assert.IsType<Ok<object>>(result);

            var releaseDtoResult =
                Assert.IsType<ReleaseDto>(objectResult.Value);

            Assert.Equal(releasePatchDto.Title, releaseDtoResult.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var user = _UserFixture.Create();

            var release = _ReleaseFixture.Create(createdBy: user);

            _ProjectFixture.GrantAccess(release.ProjectID, 
                user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            InitHttpAndServiceContextWithUser(user);
            object[] id = [release.ID];
            var result = _ResourceController.Delete(
                _HttpContext, ResourceTypes.ReleasesRelease, id);

            Assert.IsType<Ok>(result);
        }
    }
}
