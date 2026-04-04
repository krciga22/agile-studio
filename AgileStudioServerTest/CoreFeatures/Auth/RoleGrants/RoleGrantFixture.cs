using AgileStudioServer.CoreFeatures.Auth.RoleGrants;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Auth.Roles;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.Auth.RoleGrants
{
    public class RoleGrantFixture(
        RoleGrantRepository roleGrantRepository,
        UserFixture userFixture,
        RoleFixture roleFixture) : AbstractEntityFixture<RoleGrantRepository>(roleGrantRepository)
    {
        private readonly UserFixture _userFixture = userFixture;
        private readonly RoleFixture _RoleFixture = roleFixture;

        public RoleGrantModel Create(
            string? roleKey = null,
            string? subjectType = null,
            string? subjectID = null,
            string? scope = null,
            string? scopeID = null,
            UserModel? createdBy = null)
        {
            subjectType ??= RoleSubjectTypes.USER;
            createdBy ??= _userFixture.Create();
            subjectID ??= createdBy.ID.ToString();

            if(roleKey == null){
                var role = _RoleFixture.Create();
                roleKey = role.RoleKey;
            }

            var grant = new RoleGrantModel(roleKey, subjectType, subjectID)
            {
                Scope = scope,
                ScopeID = scopeID,
                CreatedByID = createdBy.ID
            };

            return _Repository.Create(grant);
        }

        public RoleGrantModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}