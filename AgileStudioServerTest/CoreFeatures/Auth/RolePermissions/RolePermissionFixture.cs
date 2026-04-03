using AgileStudioServer.CoreFeatures.Auth.RolePermissions;
using AgileStudioServer.CoreFeatures.Auth.Permissions;
using AgileStudioServer.CoreFeatures.Auth.Roles;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Auth.Permissions;
using AgileStudioServerTest.CoreFeatures.Auth.Roles;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.Auth.RolePermissions
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
            UserModel? createdBy = null)
        {
            role ??= _roleFixture.Create();
            permission ??= _permissionFixture.Create();
            createdBy ??= _userFixture.Create();

            var model = new RolePermissionModel(role.RoleKey, permission.PermissionKey)
            {
                CreatedByID = createdBy.ID
            };

            return _Repository.Create(model);
        }

        public RolePermissionModel? Get(string[] id)
        {
            return _Repository.Get(id);
        }
    }
}