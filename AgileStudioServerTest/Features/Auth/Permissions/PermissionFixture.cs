using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Auth.Permissions
{
    public class PermissionFixture(
        PermissionRepository permissionRepository,
        UserFixture userFixture) : AbstractEntityFixture<PermissionRepository>(permissionRepository)
    {
        private readonly UserFixture _userFixture = userFixture;

        public PermissionModel Create(
            string? permissionKey = null,
            string? title = null,
            string? description = null,
            string? scope = null,
            UserModel? createdBy = null,
            bool? isSystemPermission = false)
        {
            permissionKey ??= "test-permission";
            title ??= "Test Permission";
            createdBy ??= _userFixture.Create();

            var permission = new PermissionModel(permissionKey, title)
            {
                Description = description,
                CreatedByID = createdBy.ID,
                IsSystemPermission = isSystemPermission ?? false
            };

            return _Repository.Create(permission);
        }

        public PermissionModel? Get(string permissionKey)
        {
            return _Repository.Get(permissionKey);
        }
    }
}