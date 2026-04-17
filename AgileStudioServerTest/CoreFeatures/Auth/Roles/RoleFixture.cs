using AgileStudioServer.CoreFeatures.Auth.Roles;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.Auth.Roles
{
    public class RoleFixture(
        RoleRepository roleRepository,
        UserFixture userFixture) : AbstractEntityFixture<RoleRepository>(roleRepository)
    {
        private readonly UserFixture _userFixture = userFixture;

        public RoleModel Create(
            string? roleKey = null,
            string? title = null,
            string? description = null,
            string? scope = null,
            UserModel? createdBy = null,
            bool? isSystemRole = false)
        {
            roleKey ??= "test-role";
            title ??= "Test Role";
            createdBy ??= _userFixture.Create();

            var role = new RoleModel(roleKey, title)
            {
                Description = description,
                Scope = scope,
                CreatedByID = createdBy.ID,
                IsSystemRole = isSystemRole ?? false
            };

            return _Repository.Create(role);
        }

        public RoleModel? Get(string roleKey)
        {
            return _Repository.Get(roleKey);
        }
    }
}