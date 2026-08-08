using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeParentScopeResovler(BacklogItemLinkTypeService backlogItemLinkTypeService) : IParentScopeResolver
    {
        private readonly BacklogItemLinkTypeService _BacklogItemLinkTypeService = backlogItemLinkTypeService;

        public bool IsSupportedScope(string scope)
        {
            return scope == Scopes.ACCOUNT_BACKLOG_ITEM_LINK_TYPE;
        }

        public ParentScope? GetParentScope(string scope, string? scopeId)
        {
            if (!IsSupportedScope(scope))
                return null;

            string? parentScopeId = null;
            if (scopeId != null)
            {
                var backlogItemLinkType = _BacklogItemLinkTypeService.Get(int.Parse(scopeId));
                parentScopeId = backlogItemLinkType.AccountID.ToString();
            }

            return new ParentScope(Scopes.ACCOUNT, parentScopeId);
        }
    }
}
