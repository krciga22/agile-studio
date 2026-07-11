using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServer.Features.Accounts.Workflows
{
    public class WorkflowParentScopeResovler(WorkflowService releaseService) : IParentScopeResolver
    {
        private readonly WorkflowService _WorkflowService = releaseService;

        public bool IsSupportedScope(string scope)
        {
            return scope == Scopes.ACCOUNT_WORKFLOW;
        }

        public ParentScope? GetParentScope(string scope, string? scopeId)
        {
            if (!IsSupportedScope(scope))
                return null;

            string? parentScopeId = null;
            if (scopeId != null)
            {
                var workflow = _WorkflowService.Get(int.Parse(scopeId));
                parentScopeId = workflow.AccountID.ToString();
            }

            return new ParentScope(Scopes.ACCOUNT, parentScopeId);
        }
    }
}
