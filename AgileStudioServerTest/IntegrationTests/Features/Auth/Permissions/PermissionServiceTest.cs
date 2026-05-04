using AgileStudioServer.Data;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServerTest.Features.Auth.Permissions;

namespace AgileStudioServerTest.IntegrationTests.Features.Auth.Permissions
{
    public class PermissionServiceTest : AbstractServiceTest
    {
        private readonly PermissionService _PermissionService;
        private readonly PermissionFixture _PermissionFixture;

        public PermissionServiceTest(
            DBContext dbContext,
            PermissionService permissionService,
            PermissionFixture permissionFixture) : base(dbContext)
        {
            _PermissionService = permissionService;
            _PermissionFixture = permissionFixture;
        }

        [Fact]
        public void Create_ReturnsPermission()
        {
            var permission = new PermissionModel("test-permission", "Test Permission");

            var createdPermission = _PermissionService.Create(permission);

            Assert.NotNull(createdPermission);
            Assert.Equal(permission.PermissionKey, createdPermission.PermissionKey);
        }

        [Fact]
        public void Get_ReturnsPermission()
        {
            var permission = _PermissionFixture.Create();

            var returnedPermission = _PermissionService.Get(permission.PermissionKey);

            Assert.NotNull(returnedPermission);
            Assert.Equal(permission.PermissionKey, returnedPermission.PermissionKey);
        }

        [Fact]
        public void Update_ReturnsUpdatedPermission()
        {
            var permission = _PermissionFixture.Create();
            var newTitle = $"{permission.Title} Updated";

            permission.Title = newTitle;
            permission = _PermissionService.Update(permission);

            Assert.NotNull(permission);
            Assert.Equal(newTitle, permission.Title);
        }

        [Fact]
        public void Delete_DeletesPermission()
        {
            var permission = _PermissionFixture.Create();

            _PermissionService.Delete(permission);

            permission = _PermissionService.Get(permission.PermissionKey);
            Assert.Null(permission);
        }
    }
}