using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaParentScopeResovler(BacklogItemLinkTypeSchemaService backlogItemLinkTypeSchemaService) : IParentScopeResolver
    {
        private readonly BacklogItemLinkTypeSchemaService _BacklogItemLinkTypeSchemaService = backlogItemLinkTypeSchemaService;

        public bool IsSupportedScope(string scope)
        {
            return scope == Scopes.ACCOUNT_BACKLOG_ITEM_LINK_TYPE_SCHEMA;
        }

        public ParentScope? GetParentScope(string scope, string? scopeId)
        {
            if (!IsSupportedScope(scope))
                return null;

            string? parentScopeId = null;
            if (scopeId != null)
            {
                var backlogItemLinkTypeSchema = _BacklogItemLinkTypeSchemaService.Get(int.Parse(scopeId));
                parentScopeId = backlogItemLinkTypeSchema.AccountID.ToString();
            }

            return new ParentScope(Scopes.ACCOUNT, parentScopeId);
        }
    }
}
