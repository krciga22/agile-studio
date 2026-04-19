using AgileStudioServer.Data;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Roles;
using AgileStudioServer.Features.Projects.Projects;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Features.Auth.RoleGrants;
using AgileStudioServerTest.Features.Auth.Roles;
using AgileStudioServerTest.Features.Projects.Projects;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.IntegrationTests.Features.Auth.RoleGrants
{
    public class RoleGrantServiceTest : AbstractServiceTest
    {
        private readonly RoleGrantService _RoleGrantService;
        private readonly RoleGrantFixture _RoleGrantFixture;
        private readonly UserFixture _UserFixture;
        private readonly RoleFixture _RoleFixture;
        private readonly ProjectFixture _ProjectFixture;

        public RoleGrantServiceTest(
            DBContext dbContext,
            RoleGrantService roleGrantService,
            RoleGrantFixture roleGrantFixture,
            UserFixture userFixture,
            RoleFixture roleFixture,
            ProjectFixture projectFixture) : base(dbContext)
        {
            _RoleGrantService = roleGrantService;
            _RoleGrantFixture = roleGrantFixture;
            _UserFixture = userFixture;
            _RoleFixture = roleFixture;
            _ProjectFixture = projectFixture;
        }

        [Fact]
        public void Create_ReturnsRoleGrant()
        {
            var user = _UserFixture.Create();
            var role = _RoleFixture.Create("test-role");
            var createdByUser = _UserFixture.Create();
            var grant = new RoleGrantModel(role.RoleKey, RoleSubjectTypes.USER, user.ID.ToString())
            {
                CreatedByID = createdByUser.ID
            };

            var returned = _RoleGrantService.Create(grant);

            Assert.NotNull(returned);
            Assert.Equal(grant.RoleKey, returned.RoleKey);
            Assert.Equal(grant.SubjectType, returned.SubjectType);
            Assert.Equal(grant.SubjectID, returned.SubjectID);
            Assert.Equal(grant.CreatedByID, returned.CreatedByID);
        }

        [Fact]
        public void Get_ReturnsRoleGrant()
        {
            var grant = _RoleGrantFixture.Create();

            var returned = _RoleGrantService.Get(grant.ID);

            Assert.NotNull(returned);
            Assert.Equal(grant.ID, returned.ID);
        }

        [Fact]
        public void Delete_DeletesRoleGrant()
        {
            var grant = _RoleGrantFixture.Create();

            _RoleGrantService.Delete(grant);

            var deleted = _RoleGrantService.Get(grant.ID);
            Assert.Null(deleted);
        }

        [Fact]
        public void GetRoleGrantsWithRoleKey_ReturnsRoleGrantsWithRoleKey()
        {
            var grant = _RoleGrantFixture.Create();

            var results = _RoleGrantService.GetRoleGrants(grant.RoleKey, null, null, null, null);

            Assert.NotNull(results);
            Assert.Contains(results, rg => rg.ID == grant.ID && rg.RoleKey == grant.RoleKey);
            foreach (var rg in results){
                Assert.Equal(grant.RoleKey, rg.RoleKey);
            }
        }

        [Fact]
        public void GetRoleGrantsWithRoleKeySubjectType_ReturnsRoleGrants()
        {
            RoleModel role = _RoleFixture.Create();
            UserModel user = _UserFixture.Create();
            string roleKey = role.RoleKey;
            string subjectType = RoleSubjectTypes.USER;
            string subjectId = user.ID.ToString();
            var grant = _RoleGrantFixture.Create(
                roleKey: role.RoleKey, 
                subjectType: subjectType, 
                subjectID: subjectId);

            var results = _RoleGrantService.GetRoleGrants(roleKey, subjectType, null, null, null);

            Assert.NotNull(results);
            Assert.Contains(results, rg => rg.ID == grant.ID);
            foreach (var rg in results)
            {
                Assert.Equal(grant.RoleKey, roleKey);
                Assert.Equal(grant.SubjectType, subjectType);
            }
        }

        [Fact]
        public void GetRoleGrantsWithRoleKeySubjectTypeSubjectId_ReturnsRoleGrants()
        {
            RoleModel role = _RoleFixture.Create();
            UserModel user = _UserFixture.Create();
            string roleKey = role.RoleKey;
            string subjectType = RoleSubjectTypes.USER;
            string subjectId = user.ID.ToString();
            var grant = _RoleGrantFixture.Create(
                roleKey: role.RoleKey,
                subjectType: subjectType,
                subjectID: subjectId);

            var results = _RoleGrantService.GetRoleGrants(roleKey, subjectType, subjectId, null, null);

            Assert.NotNull(results);
            Assert.Contains(results, rg => rg.ID == grant.ID);
            foreach (var rg in results)
            {
                Assert.Equal(grant.RoleKey, roleKey);
                Assert.Equal(grant.SubjectType, subjectType);
                Assert.Equal(grant.SubjectID, subjectId);
            }
        }

        [Fact]
        public void GetRoleGrantsWithRoleKeySubjectTypeSubjectIdScope_ReturnsRoleGrants()
        {
            RoleModel role = _RoleFixture.Create();
            UserModel user = _UserFixture.Create();
            string roleKey = role.RoleKey;
            string subjectType = RoleSubjectTypes.USER;
            string subjectId = user.ID.ToString();
            string scope = PermissionScopes.PROJECTS;
            var grant = _RoleGrantFixture.Create(
                roleKey: role.RoleKey,
                subjectType: subjectType,
                subjectID: subjectId,
                scope: scope);

            var results = _RoleGrantService.GetRoleGrants(roleKey, subjectType, subjectId, scope, null);

            Assert.NotNull(results);
            Assert.Contains(results, rg => rg.ID == grant.ID);
            foreach (var rg in results)
            {
                Assert.Equal(grant.RoleKey, roleKey);
                Assert.Equal(grant.SubjectType, subjectType);
                Assert.Equal(grant.SubjectID, subjectId);
                Assert.Equal(grant.Scope, scope);
            }
        }

        [Fact]
        public void GetRoleGrantsWithRoleKeySubjectTypeSubjectIdScopeScopeId_ReturnsRoleGrants()
        {
            RoleModel role = _RoleFixture.Create();
            UserModel user = _UserFixture.Create();
            ProjectModel project = _ProjectFixture.Create();
            string roleKey = role.RoleKey;
            string subjectType = RoleSubjectTypes.USER;
            string subjectId = user.ID.ToString();
            string scope = PermissionScopes.PROJECTS;
            string scopeId = project.ID.ToString();
            var grant = _RoleGrantFixture.Create(
                roleKey: role.RoleKey,
                subjectType: subjectType,
                subjectID: subjectId,
                scope: scope,
                scopeID: scopeId);

            var results = _RoleGrantService.GetRoleGrants(roleKey, subjectType, subjectId, scope, scopeId);

            Assert.NotNull(results);
            Assert.Contains(results, rg => rg.ID == grant.ID);
            foreach (var rg in results)
            {
                Assert.Equal(grant.RoleKey, roleKey);
                Assert.Equal(grant.SubjectType, subjectType);
                Assert.Equal(grant.SubjectID, subjectId);
                Assert.Equal(grant.Scope, scope);
                Assert.Equal(grant.ScopeID, scopeId);
            }
        }
    }
}