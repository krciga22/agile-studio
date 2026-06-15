using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypes
{
    public class BacklogItemTypeParentScopeResovler(BacklogItemTypeService releaseService) : IParentScopeResolver
    {
        private readonly BacklogItemTypeService _BacklogItemTypeService = releaseService;

        public bool IsSupportedScope(string scope)
        {
            return scope == Scopes.ACCOUNT_BACKLOG_ITEM_TYPE;
        }

        public ParentScope? GetParentScope(string scope, string? scopeId)
        {
            if (!IsSupportedScope(scope))
                return null;

            string? parentScopeId = null;
            if (scopeId != null)
            {
                var backlogItemType = _BacklogItemTypeService.Get(int.Parse(scopeId));
                parentScopeId = backlogItemType.AccountID.ToString();
            }

            return new ParentScope(Scopes.ACCOUNT, parentScopeId);
        }
    }
}
