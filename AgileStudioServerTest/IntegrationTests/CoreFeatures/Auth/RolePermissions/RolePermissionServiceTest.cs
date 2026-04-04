using AgileStudioServer.CoreFeatures.Auth.RolePermissions;
using AgileStudioServer.CoreFeatures.Auth.Roles;
using AgileStudioServer.Data;
using AgileStudioServerTest.CoreFeatures.Auth.Permissions;
using AgileStudioServerTest.CoreFeatures.Auth.RolePermissions;
using AgileStudioServerTest.CoreFeatures.Auth.Roles;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.Auth.RolePermissions
{
    public class RolePermissionServiceTest : AbstractServiceTest
    {
        private readonly RolePermissionService _RolePermissionService;
        private readonly RolePermissionFixture _RolePermissionFixture;
        private readonly RoleFixture _RoleFixture;
        private readonly PermissionFixture _PermissionFixture;

        public RolePermissionServiceTest(
            DBContext dbContext,
            RolePermissionService rolePermissionService,
            RolePermissionFixture rolePermissionFixture,
            RoleFixture roleFixture,
            PermissionFixture permissionFixture) : base(dbContext)
        {
            _RolePermissionService = rolePermissionService;
            _RolePermissionFixture = rolePermissionFixture;
            _RoleFixture = roleFixture;
            _PermissionFixture = permissionFixture;
        }

        [Fact]
        public void Create_ReturnsRolePermission()
        {
            var role = _RoleFixture.Create("test-role");
            var permission = _PermissionFixture.Create("test-permission");
            var rolePermission = new RolePermissionModel(role.RoleKey, permission.PermissionKey);

            var returned = _RolePermissionService.Create(rolePermission);

            Assert.NotNull(returned);
            Assert.Equal(rolePermission.RoleKey, returned.RoleKey);
            Assert.Equal(rolePermission.PermissionKey, returned.PermissionKey);
        }

        [Fact]
        public void Get_ReturnsRolePermission()
        {
            var rolePermission = _RolePermissionFixture.Create();

            var returned = _RolePermissionService.Get(
                rolePermission.RoleKey, rolePermission.PermissionKey);

            Assert.NotNull(returned);
            Assert.Equal(rolePermission.RoleKey, returned.RoleKey);
            Assert.Equal(rolePermission.PermissionKey, returned.PermissionKey);
        }

        [Fact]
        public void Delete_DeletesRolePermission()
        {
            var rolePermission = _RolePermissionFixture.Create();

            _RolePermissionService.Delete(rolePermission);

            var deleted = _RolePermissionService.Get(
                rolePermission.RoleKey, rolePermission.PermissionKey);
            Assert.Null(deleted);
        }

        [Fact]
        public void GetByRole_ReturnsRolePermissionsWithRole()
        {
            var rolePermission = _RolePermissionFixture.Create();

            var results = _RolePermissionService.GetByRole(rolePermission.RoleKey);

            Assert.NotNull(results);
            Assert.Contains(results, rp => rp.RoleKey == rolePermission.RoleKey 
                && rp.PermissionKey == rolePermission.PermissionKey);
            foreach (var rp in results){
                Assert.Equal(rolePermission.RoleKey, rp.RoleKey);
            }
        }

        [Fact]
        public void GetByPermission_ReturnsRolePermissionsWithPermission()
        {
            var rolePermission = _RolePermissionFixture.Create();

            var results = _RolePermissionService.GetByPermission(rolePermission.PermissionKey);

            Assert.NotNull(results);
            Assert.Contains(results, rp => rp.RoleKey == rolePermission.RoleKey 
                && rp.PermissionKey == rolePermission.PermissionKey);
            foreach (var rp in results){
                Assert.Equal(rolePermission.PermissionKey, rp.PermissionKey);
            }
        }

        [Fact]
        public void GetByScope_ReturnsRolePermissionsInScope()
        {
            var inScope = "in-scope";
            var notInScope = "not-in-scope";

            var rolePermissionInScope = _RolePermissionFixture.Create(
                _RoleFixture.Create("test-role-in-scope", scope: inScope),
                _PermissionFixture.Create("test-permission-in-scope", scope: inScope)
            );

            var rolePermissionNotInScope = _RolePermissionFixture.Create(
                _RoleFixture.Create("test-role-not-in-scope", scope: notInScope),
                _PermissionFixture.Create("test-permission-not-in-scope", scope: notInScope)
            );

            var returnedRolePermissionsInScope = _RolePermissionService.GetByScope(inScope);
            var returnedRolePermissionsNotInScope = _RolePermissionService.GetByScope(notInScope);

            Assert.Contains(returnedRolePermissionsInScope, rp => rp.RoleKey == rolePermissionInScope.RoleKey
                && rp.PermissionKey == rolePermissionInScope.PermissionKey);
            foreach (var rolePermission in returnedRolePermissionsInScope){
                RoleModel? role = _RoleFixture.Get(rolePermission.RoleKey);
                Assert.NotNull(role);
                Assert.Equal(inScope, role.Scope);
            }

            Assert.Contains(returnedRolePermissionsNotInScope, rp => rp.RoleKey == rolePermissionNotInScope.RoleKey
                && rp.PermissionKey == rolePermissionNotInScope.PermissionKey);
            foreach (var rolePermission in returnedRolePermissionsNotInScope)
            {
                RoleModel? role = _RoleFixture.Get(rolePermission.RoleKey);
                Assert.NotNull(role);
                Assert.Equal(notInScope, role.Scope);
            }
        }
    }
}