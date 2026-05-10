using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Projects.Releases;

namespace AgileStudioServer.Features.Releases.Releases
{
    public class ReleaseParentScopeResovler(ReleaseService releaseService) : IParentScopeResolver
    {
        private readonly ReleaseService _ReleaseService = releaseService;

        public bool IsSupportedScope(string scope)
        {
            return scope == Scopes.PROJECT_RELEASE;
        }

        public ParentScope? GetParentScope(string scope, string? scopeId)
        {
            if (!IsSupportedScope(scope))
                return null;

            string? parentScopeId = null;
            if (scopeId != null)
            {
                ReleaseModel release = _ReleaseService.Get(int.Parse(scopeId));
                parentScopeId = release.ProjectID.ToString();
            }

            return new ParentScope(Scopes.PROJECT, parentScopeId);
        }
    }
}
