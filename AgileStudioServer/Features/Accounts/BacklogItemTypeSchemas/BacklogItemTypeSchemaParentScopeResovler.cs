using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaParentScopeResovler(
        BacklogItemTypeSchemaService backlogItemTypeSchemaService) : IParentScopeResolver
    {
        private readonly BacklogItemTypeSchemaService _BacklogItemTypeSchemaService = backlogItemTypeSchemaService;

        public bool IsSupportedScope(string scope)
        {
            return scope == Scopes.ACCOUNT_BACKLOG_ITEM_TYPE_SCHEMA;
        }

        public ParentScope? GetParentScope(string scope, string? scopeId)
        {
            if (!IsSupportedScope(scope))
                return null;

            string? parentScopeId = null;
            if (scopeId != null)
            {
                var backlogItemTypeSchema = _BacklogItemTypeSchemaService.Get(int.Parse(scopeId));
                parentScopeId = backlogItemTypeSchema.AccountID.ToString();
            }

            return new ParentScope(Scopes.ACCOUNT, parentScopeId);
        }
    }
}
