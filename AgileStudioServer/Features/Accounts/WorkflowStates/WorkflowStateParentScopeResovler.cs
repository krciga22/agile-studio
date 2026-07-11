using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServer.Features.Accounts.WorkflowStates
{
    public class WorkflowStateParentScopeResovler(WorkflowStateService workflowStateService) : IParentScopeResolver
    {
        private readonly WorkflowStateService _WorkflowStateService = workflowStateService;

        public bool IsSupportedScope(string scope)
        {
            return scope == Scopes.ACCOUNT_WORKFLOW_STATE;
        }

        public ParentScope? GetParentScope(string scope, string? scopeId)
        {
            if (!IsSupportedScope(scope))
                return null;

            string? parentScopeId = null;
            if (scopeId != null)
            {
                var workflowState = _WorkflowStateService.Get(int.Parse(scopeId));
                parentScopeId = workflowState.WorkflowId.ToString();
            }

            return new ParentScope(Scopes.ACCOUNT_WORKFLOW, parentScopeId);
        }
    }
}
