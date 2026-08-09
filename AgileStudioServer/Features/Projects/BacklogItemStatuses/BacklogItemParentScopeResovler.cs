using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServer.Features.Projects.BacklogItemStatuses
{
    public class BacklogItemStatusParentScopeResovler(
        BacklogItemStatusService backlogItemStatusService) : IParentScopeResolver
    {
        private readonly BacklogItemStatusService _BacklogItemStatusService = backlogItemStatusService;

        public bool IsSupportedScope(string scope)
        {
            return scope == Scopes.PROJECT_BACKLOG_ITEM_STATUS;
        }

        public ParentScope? GetParentScope(string scope, string? scopeId)
        {
            if (!IsSupportedScope(scope))
                return null;

            string? parentScopeId = null;
            if (scopeId != null)
            {
                BacklogItemStatusModel backlogItemStatus = _BacklogItemStatusService.Get(int.Parse(scopeId));
                parentScopeId = backlogItemStatus.BacklogItemID.ToString();
            }

            return new ParentScope(Scopes.PROJECT_BACKLOG_ITEM, parentScopeId);
        }
    }
}
