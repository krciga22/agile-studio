using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Auth.Roles;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Auth.RoleGrants
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