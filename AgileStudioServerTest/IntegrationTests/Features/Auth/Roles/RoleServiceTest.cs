using AgileStudioServer.Data;
using AgileStudioServer.Features.Auth.Roles;
using AgileStudioServerTest.Features.Auth.Roles;

namespace AgileStudioServerTest.IntegrationTests.Features.Auth.Roles
{
    public class RoleServiceTest : AbstractServiceTest
    {
        private readonly RoleService _roleService;
        private readonly RoleFixture _RoleFixture;

        public RoleServiceTest(
            DBContext dbContext,
            RoleService roleService,
            RoleFixture roleFixture) : base(dbContext)
        {
            _roleService = roleService;
            _RoleFixture = roleFixture;
        }

        [Fact]
        public void Create_ReturnsRole()
        {
            var role = new RoleModel("test-role", "Test Role");

            var createdRole = _roleService.Create(role);

            Assert.NotNull(createdRole);
            Assert.Equal(role.RoleKey, createdRole.RoleKey);
        }

        [Fact]
        public void Get_ReturnsRole()
        {
            var role = _RoleFixture.Create();

            var returnedRole = _roleService.Get(role.RoleKey);

            Assert.NotNull(returnedRole);
            Assert.Equal(role.RoleKey, returnedRole.RoleKey);
        }

        [Fact]
        public void GetByScope_ReturnsRolesInScope()
        {
            var inScope = "in-scope";
            var notInScope = "not-in-scope";

            var rolesInScope = new List<RoleModel>
            {
                _RoleFixture.Create("test-role-1", scope: inScope),
                _RoleFixture.Create("test-role-2", scope: inScope),
                _RoleFixture.Create("test-role-3", scope: inScope),
            };
            var rolesNotInScope = new List<RoleModel>
            {
                _RoleFixture.Create("test-role-4", scope: notInScope),
                _RoleFixture.Create("test-role-5", scope: notInScope),
                _RoleFixture.Create("test-role-6", scope: notInScope),
            };

            var returnedRolesInScope = _roleService.GetByScope(inScope);
            var returnedRolesNotInScope = _roleService.GetByScope(notInScope);

            Assert.Equal(rolesInScope.Count, returnedRolesInScope.Count);
            foreach (var role in returnedRolesInScope){
                Assert.Equal(role.Scope, inScope);
            }

            Assert.Equal(rolesNotInScope.Count, returnedRolesNotInScope.Count);
            foreach (var role in returnedRolesNotInScope){
                Assert.Equal(role.Scope, notInScope);
            }
        }

        [Fact]
        public void Update_ReturnsUpdatedRole()
        {
            var role = _RoleFixture.Create();
            var newTitle = $"{role.Title} Updated";

            role.Title = newTitle;
            role = _roleService.Update(role);

            Assert.NotNull(role);
            Assert.Equal(newTitle, role.Title);
        }

        [Fact]
        public void Delete_DeletesRole()
        {
            var role = _RoleFixture.Create();

            _roleService.Delete(role);

            role = _roleService.Get(role.RoleKey);
            Assert.Null(role);
        }
    }
}