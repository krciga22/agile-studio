using AgileStudioServer.Core.Services;
using AgileStudioServer.CoreFeatures.Auth.RoleGrants;

namespace AgileStudioServer.CoreFeatures.Auth.Permissions
{
    public class PermissionCheckerService(RoleGrantRepository roleGrantRepository) : AbstractService
    {
        private readonly RoleGrantRepository _RoleGrantRepository = roleGrantRepository;

        public bool CheckPermissions(
            string subjectType, string subjectId, string scope, 
            string? scopeId, string permissionKey)
        {
             var roleGrants = _RoleGrantRepository.GetRoleGrantsBySubjectScopeAndPermission(
                subjectType, subjectId, scope, scopeId, permissionKey);

            return roleGrants.Count > 0;
        }
    }
}