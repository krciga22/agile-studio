using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServer.Features.Projects.Sprints
{
    public class SprintParentScopeResovler(SprintService sprintService) : IParentScopeResolver
    {
        private readonly SprintService _SprintService = sprintService;

        public bool IsSupportedScope(string scope)
        {
            return scope == Scopes.PROJECT_SPRINT;
        }

        public ParentScope? GetParentScope(string scope, string? scopeId)
        {
            if (!IsSupportedScope(scope))
                return null;

            string? parentScopeId = null;
            if (scopeId != null)
            {
                SprintModel sprint = _SprintService.Get(int.Parse(scopeId));
                parentScopeId = sprint.ProjectID.ToString();
            }

            return new ParentScope(Scopes.PROJECT, parentScopeId);
        }
    }
}
