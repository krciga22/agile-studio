using AgileStudioServer.CoreFeatures.Auth.Permissions;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.Auth.Permissions
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
            UserModel? createdBy = null)
        {
            permissionKey ??= "test-permission";
            title ??= "Test Permission";
            createdBy ??= _userFixture.Create();

            var permission = new PermissionModel(permissionKey, title)
            {
                Description = description,
                Scope = scope,
                CreatedByID = createdBy.ID
            };

            return _Repository.Create(permission);
        }

        public PermissionModel? Get(string permissionKey)
        {
            return _Repository.Get(permissionKey);
        }
    }
}