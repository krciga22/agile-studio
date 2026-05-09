using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RolePermissions;
using AgileStudioServer.Features.Auth.Roles;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Features.Auth.Permissions;
using AgileStudioServerTest.Features.Auth.Roles;
using AgileStudioServerTest.Features.Users.Users;
using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServerTest.Features.Auth.RolePermissions
{
    public class RolePermissionFixture : AbstractEntityFixture<RolePermissionRepository>
    {
        private readonly RoleFixture _roleFixture;
        private readonly PermissionFixture _permissionFixture;
        private readonly UserFixture _userFixture;

        public RolePermissionFixture(
            RolePermissionRepository rolePermissionRepository,
            RoleFixture roleFixture,
            PermissionFixture permissionFixture,
            UserFixture userFixture) : base(rolePermissionRepository)
        {
            _roleFixture = roleFixture;
            _permissionFixture = permissionFixture;
            _userFixture = userFixture;
        }

        public RolePermissionModel Create(
            RoleModel? role = null,
            PermissionModel? permission = null,
            string scope = Scopes.GLOBAL,
            UserModel? createdBy = null,
            bool? isSystemRolePermission = false)
        {
            role ??= _roleFixture.Create();
            permission ??= _permissionFixture.Create();
            createdBy ??= _userFixture.Create();

            var model = new RolePermissionModel(role.RoleKey, permission.PermissionKey, scope)
            {
                CreatedByID = createdBy.ID,
                IsSystemRolePermission = isSystemRolePermission ?? false
            };

            return _Repository.Create(model);
        }

        public RolePermissionModel? Get(string[] id)
        {
            return _Repository.Get(id);
        }
    }
}