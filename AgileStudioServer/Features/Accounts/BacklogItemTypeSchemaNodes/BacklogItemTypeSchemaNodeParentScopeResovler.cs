using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes
{
    public class BacklogItemTypeSchemaNodeParentScopeResovler(
        BacklogItemTypeSchemaNodeService backlogItemTypeSchemaNodeService) : IParentScopeResolver
    {
        private readonly BacklogItemTypeSchemaNodeService _BacklogItemTypeSchemaNodeService = backlogItemTypeSchemaNodeService;

        public bool IsSupportedScope(string scope)
        {
            return scope == Scopes.ACCOUNT_BACKLOG_ITEM_TYPE_SCHEMA_NODE;
        }

        public ParentScope? GetParentScope(string scope, string? scopeId)
        {
            if (!IsSupportedScope(scope))
                return null;

            string? parentScopeId = null;
            if (scopeId != null)
            {
                var backlogItemTypeSchemaNode = _BacklogItemTypeSchemaNodeService.Get(int.Parse(scopeId));
                parentScopeId = backlogItemTypeSchemaNode.SchemaID.ToString();
            }

            return new ParentScope(Scopes.ACCOUNT_BACKLOG_ITEM_TYPE_SCHEMA, parentScopeId);
        }
    }
}
