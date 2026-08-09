using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryParentScopeResovler(
        BacklogItemLinkTypeSchemaEntryService backlogItemLinkTypeSchemaEntryService) : 
        IParentScopeResolver
    {
        private readonly BacklogItemLinkTypeSchemaEntryService _BacklogItemLinkTypeSchemaEntryService = backlogItemLinkTypeSchemaEntryService;

        public bool IsSupportedScope(string scope)
        {
            return scope == Scopes.ACCOUNT_BACKLOG_ITEM_LINK_TYPE_SCHEMA_ENTRY;
        }

        public ParentScope? GetParentScope(string scope, string? scopeId)
        {
            if (!IsSupportedScope(scope))
                return null;

            string? parentScopeId = null;
            if (scopeId != null)
            {
                var backlogItemLinkTypeSchemaEntry = _BacklogItemLinkTypeSchemaEntryService.Get(int.Parse(scopeId));
                parentScopeId = backlogItemLinkTypeSchemaEntry.BacklogItemLinkTypeSchemaID.ToString();
            }

            return new ParentScope(Scopes.ACCOUNT_BACKLOG_ITEM_LINK_TYPE_SCHEMA, parentScopeId);
        }
    }
}
