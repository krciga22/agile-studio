using AgileStudioServer.CoreFeatures.Auth.Permissions;
using AgileStudioServer.Data;
using AgileStudioServerTest.CoreFeatures.Auth.Permissions;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.Auth.Permissions
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
        public void GetByScope_ReturnsPermissionsInScope()
        {
            var inScope = "in-scope";
            var notInScope = "not-in-scope";

            var permissionsInScope = new List<PermissionModel>
            {
                _PermissionFixture.Create("test-permission-1", scope: inScope),
                _PermissionFixture.Create("test-permission-2", scope: inScope),
                _PermissionFixture.Create("test-permission-3", scope: inScope),
            };
            var permissionsNotInScope = new List<PermissionModel>
            {
                _PermissionFixture.Create("test-permission-4", scope: notInScope),
                _PermissionFixture.Create("test-permission-5", scope: notInScope),
                _PermissionFixture.Create("test-permission-6", scope: notInScope),
            };

            var returnedPermissionsInScope = _PermissionService.GetByScope(inScope);
            var returnedPermissionsNotInScope = _PermissionService.GetByScope(notInScope);

            Assert.Equal(permissionsInScope.Count, returnedPermissionsInScope.Count);
            foreach (var permission in returnedPermissionsInScope)
            {
                Assert.Equal(permission.Scope, inScope);
            }

            Assert.Equal(permissionsNotInScope.Count, returnedPermissionsNotInScope.Count);
            foreach (var permission in returnedPermissionsNotInScope)
            {
                Assert.Equal(permission.Scope, notInScope);
            }
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