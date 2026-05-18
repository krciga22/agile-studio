using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServer.Features.Projects.BacklogItems
{
    public class BacklogItemParentScopeResovler(
        BacklogItemService backlogItemService) : IParentScopeResolver
    {
        private readonly BacklogItemService _BacklogItemService = backlogItemService;

        public bool IsSupportedScope(string scope)
        {
            return scope == Scopes.PROJECT_BACKLOG_ITEM;
        }

        public ParentScope? GetParentScope(string scope, string? scopeId)
        {
            if (!IsSupportedScope(scope))
                return null;

            string? parentScopeId = null;
            if (scopeId != null)
            {
                BacklogItemModel backlogItem = _BacklogItemService.Get(int.Parse(scopeId));
                parentScopeId = backlogItem.ProjectID.ToString();
            }

            return new ParentScope(Scopes.PROJECT, parentScopeId);
        }
    }
}
