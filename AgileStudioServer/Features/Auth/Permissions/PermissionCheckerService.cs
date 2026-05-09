using AgileStudioServer.Core.Services;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.RolePermissions;
using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServer.Features.Auth.Permissions
{
    public class PermissionCheckerService(
        RoleGrantService roleGrantService,
        RolePermissionService rolePermissionService,
        IEnumerable<IParentScopeResolver> parentScopeResolvers) : AbstractService
    {
        private readonly RoleGrantService _RoleGrantService = roleGrantService;
        private readonly RolePermissionService _RolePermissionService = rolePermissionService;
        private readonly IEnumerable<IParentScopeResolver> _ParentScopeResolvers = parentScopeResolvers;

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
            List<RoleGrantModel> roleGrants = [];
            roleGrants.AddRange(
                _RoleGrantService.GetRoleGrantsBySubjectScopeAndPermission(
                    subjectType, subjectId, scope, scopeId, permissionKey));

            List<ParentScope> parentScopes = ResolveParentScopes(scope, scopeId);
            foreach(ParentScope parentScope in parentScopes)
            {
                roleGrants.AddRange(
                    _RoleGrantService.GetRoleGrantsBySubjectScopeAndPermission(
                        subjectType, subjectId, parentScope.Scope, parentScope.ScopeId, permissionKey));
            }

            roleGrants = roleGrants
                .GroupBy(grant => grant.ID)
                .Select(group => group.First())
                .ToList();

            foreach (RoleGrantModel roleGrant in roleGrants)
            {
                // todo remove after scope is made not nullable in RoleGrantModel
                if (roleGrant.Scope == null){
                    continue;
                }

                RolePermissionModel? rolePermission = _RolePermissionService.Get(roleGrant.RoleKey, permissionKey, scope);
                if (rolePermission != null){
                    return true;
                }
            }


            return false;
        }

        // todo add this to a separate service
        private List<ParentScope> ResolveParentScopes(string scope, string? scopeId, int depth = 1)
        {
            if (depth > PermissionsConstants.MaxParentScopeResolutionDepth){
                throw new Exception("Max depth reached for resolving parent scopes.");
            }

            List<ParentScope> parentScopes = _ParentScopeResolvers
                .Where(resolver => resolver.IsSupportedScope(scope))
                .Select(resolver => resolver.GetParentScope(scope, scopeId))
                .Where(parentScope => parentScope != null)
                .Select(parentScope => parentScope!)
                .ToList();

            int maxParentScopes = PermissionsConstants.MaxParentScopesPerScope;
            if (parentScopes.Count > maxParentScopes){
                throw new Exception($"{parentScopes.Count} parent scopes found for scope \"{scope}\"." +
                    $"There should only be {maxParentScopes} parent scope for a given scope.");
            }

            List<ParentScope> moreParentScopes = [];
            foreach (ParentScope parentScope in parentScopes){
                moreParentScopes.AddRange(
                    ResolveParentScopes(parentScope.Scope, parentScope.ScopeId, depth + 1));
            }
            parentScopes.AddRange(moreParentScopes);

            return parentScopes;
        }
    }
}