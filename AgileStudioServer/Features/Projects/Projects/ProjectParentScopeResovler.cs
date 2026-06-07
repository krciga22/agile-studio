using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServer.Features.Projects.Projects
{
    public class ProjectParentScopeResovler(ProjectService projectService) : IParentScopeResolver
    {
        private readonly ProjectService _ProjectService = projectService;

        public bool IsSupportedScope(string scope)
        {
            return scope == Scopes.PROJECT;
        }

        public ParentScope? GetParentScope(string scope, string? scopeId)
        {
            if (!IsSupportedScope(scope))
                return null;

            string? parentScopeId = null;
            if(scopeId != null){
                ProjectModel project = _ProjectService.Get(int.Parse(scopeId));
                parentScopeId = project.AccountID.ToString();
            }

            return new ParentScope(Scopes.ACCOUNT, parentScopeId);
        }
    }
}
