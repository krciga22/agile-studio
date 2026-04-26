using AgileStudioServer.Core.Services;
using AgileStudioServer.Features.Auth.RoleGrants;

namespace AgileStudioServer.Features.Auth.Permissions
{
    public class PermissionCheckerService(RoleGrantRepository roleGrantRepository) : AbstractService
    {
        private readonly RoleGrantRepository _RoleGrantRepository = roleGrantRepository;

        /// <exception cref="UnauthorizedAccessException"></exception>
        public void ValidatePermissions(
            string subjectType, string subjectId, string scope, 
            string? scopeId, string permissionKey)
        {
            if (!CheckPermissions(subjectType, subjectId, scope, scopeId, permissionKey)){
                throw new UnauthorizedAccessException("User does not have the required permissions.");
            }
        }

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