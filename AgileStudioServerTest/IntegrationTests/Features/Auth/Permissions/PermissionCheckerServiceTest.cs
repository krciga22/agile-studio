using AgileStudioServer.Data;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Roles;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServerTest.Features.Auth.Permissions;
using AgileStudioServerTest.Features.Auth.RoleGrants;
using AgileStudioServerTest.Features.Auth.RolePermissions;
using AgileStudioServerTest.Features.Auth.Roles;
using AgileStudioServerTest.Features.Projects.Projects;
using AgileStudioServerTest.Features.Projects.Sprints;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.IntegrationTests.Features.Auth.Permissions
{
    public class PermissionCheckerServiceTest : AbstractServiceTest
    {
        private readonly PermissionCheckerService _PermissionCheckerService;
        private readonly RoleGrantFixture _RoleGrantFixture;
        private readonly RoleFixture _RoleFixture;
        private readonly RolePermissionFixture _RolePermissionFixture;
        private readonly UserFixture _UserFixture;
        private readonly ProjectFixture _ProjectFixture;
        private readonly SprintFixture _SprintFixture;
        private readonly PermissionFixture _PermissionFixture;

        public PermissionCheckerServiceTest(
            DBContext dbContext,
            PermissionCheckerService permissionCheckerService,
            RoleGrantFixture roleGrantFixture,
            RoleFixture roleFixture,
            RolePermissionFixture rolePermissionFixture,
            UserFixture userFixture,
            ProjectFixture projectFixture,
            SprintFixture sprintFixture,
            PermissionFixture permissionFixture) : base(dbContext)
        {
            _PermissionCheckerService = permissionCheckerService;
            _RoleGrantFixture = roleGrantFixture;
            _RoleFixture = roleFixture;
            _RolePermissionFixture = rolePermissionFixture;
            _UserFixture = userFixture;
            _ProjectFixture = projectFixture;
            _SprintFixture = sprintFixture;
            _PermissionFixture = permissionFixture;
        }

        [Fact]
        public void CheckPermissionsForScope_ReturnsTrue_WhenPermissionExists()
        {
            var user = _UserFixture.Create();
            var project = _ProjectFixture.Create();

            _ProjectFixture.GrantAccess(project.ID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            var result = _PermissionCheckerService.CheckPermissions(
                RoleSubjectTypes.USER,
                user.ID.ToString(),
                Scopes.PROJECT,
                project.ID.ToString(),
                PermissionKeys.READ);

            Assert.True(result);
        }

        [Fact]
        public void CheckPermissionsForChildScope_ReturnsTrue_WhenParentScopePermissionExists()
        {
            var user = _UserFixture.Create();
            var project = _ProjectFixture.Create();
            var sprint = _SprintFixture.Create(project: project);

            _ProjectFixture.GrantAccess(project.ID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            var result = _PermissionCheckerService.CheckPermissions(
                RoleSubjectTypes.USER,
                user.ID.ToString(),
                Scopes.PROJECT_SPRINT,
                sprint.ID.ToString(),
                PermissionKeys.READ);

            Assert.True(result);
        }

        [Fact]
        public void CheckPermissions_ReturnsFalse_WhenPermissionNotExists()
        {
            var scope = Scopes.PROJECT;

            var user = _UserFixture.Create();
            var role = _RoleFixture.Create(scope: scope);
            var permission = _PermissionFixture.Create();
            var rolePermission = _RolePermissionFixture.Create(role, permission, scope);
            var project = _ProjectFixture.Create();

            var roleKey = role.RoleKey;
            var permissionKey = permission.PermissionKey;
            var subjectType = RoleSubjectTypes.USER;
            var subjectId = user.ID.ToString();
            var scopeId = project.ID.ToString();

            // note that we have not granted the role to the subject, so they should not have the permission

            var result = _PermissionCheckerService.CheckPermissions(
                subjectType, subjectId, scope, scopeId, permissionKey);

            Assert.False(result);
        }

        [Fact]
        public void CheckPermissions_ReturnsFalse_WhenSubjectIsDifferent()
        {
            var scope = Scopes.PROJECT;

            var user = _UserFixture.Create();
            var role = _RoleFixture.Create(scope: scope);
            var permission = _PermissionFixture.Create();
            var rolePermission = _RolePermissionFixture.Create(role, permission, scope);

            var roleKey = role.RoleKey;
            var permissionKey = permission.PermissionKey;
            var subjectType = RoleSubjectTypes.USER;
            var subjectId = user.ID.ToString();
            var differentSubjectId = "different-subject-id";
            string? scopeId = null;

            _RoleGrantFixture.Create(
                roleKey: roleKey,
                subjectType: subjectType,
                subjectID: differentSubjectId,
                scope: scope,
                scopeID: scopeId);

            var result = _PermissionCheckerService.CheckPermissions(
                subjectType, subjectId, scope, scopeId, permissionKey);

            Assert.False(result);
        }

        [Fact]
        public void CheckPermissions_ReturnsFalse_WhenScopeIsDifferent()
        {
            var scope = Scopes.PROJECT;
            var differentScope = "different-scope";

            var user = _UserFixture.Create();
            var role = _RoleFixture.Create(scope: differentScope);
            var permission = _PermissionFixture.Create();
            var rolePermission = _RolePermissionFixture.Create(role, permission, differentScope);

            var roleKey = role.RoleKey;
            var permissionKey = permission.PermissionKey;
            var subjectType = RoleSubjectTypes.USER;
            var subjectId = user.ID.ToString();
            string? scopeId = null;

            _RoleGrantFixture.Create(
                roleKey: roleKey,
                subjectType: subjectType,
                subjectID: subjectId,
                scope: scope,
                scopeID: scopeId);

            var result = _PermissionCheckerService.CheckPermissions(
                subjectType, subjectId, scope, scopeId, permissionKey);

            Assert.False(result);
        }

        [Fact]
        public void CheckPermissions_ReturnsFalse_WhenScopeIdIsDifferent()
        {
            var scope = Scopes.PROJECT;

            var user = _UserFixture.Create();
            var role = _RoleFixture.Create(scope: scope);
            var permission = _PermissionFixture.Create();
            var rolePermission = _RolePermissionFixture.Create(role, permission, scope);
            var project = _ProjectFixture.Create();

            var roleKey = role.RoleKey;
            var permissionKey = permission.PermissionKey;
            var subjectType = RoleSubjectTypes.USER;
            var subjectId = user.ID.ToString();
            var scopeId = project.ID.ToString();
            var differentScopeId = "different-scope-id";

            _RoleGrantFixture.Create(
                roleKey: roleKey,
                subjectType: subjectType,
                subjectID: subjectId,
                scope: scope,
                scopeID: differentScopeId);

            var result = _PermissionCheckerService.CheckPermissions(
                subjectType, subjectId, scope, scopeId, permissionKey);

            Assert.False(result);
        }
    }
}