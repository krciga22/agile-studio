using AgileStudioServer.CoreFeatures.Auth.RoleGrants;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.Auth.RoleGrants
{
    public class RoleGrantFixture(
        RoleGrantRepository roleGrantRepository,
        UserFixture userFixture) : AbstractEntityFixture<RoleGrantRepository>(roleGrantRepository)
    {
        private readonly UserFixture _userFixture = userFixture;

        public RoleGrantModel Create(
            string? roleKey = null,
            string? subjectType = null,
            string? subjectID = null,
            string? scope = null,
            string? scopeID = null,
            UserModel? createdBy = null)
        {
            roleKey ??= "test-role";
            subjectType ??= RoleSubjectTypes.USER;
            createdBy ??= _userFixture.Create();
            subjectID ??= createdBy.ID.ToString();

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
            return _Repository.GetById(id);
        }
    }
}