using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges
{
    public class BacklogItemTypeSchemaEdgeParentScopeResovler(
        BacklogItemTypeSchemaEdgeService backlogItemTypeSchemaEdgeService) : IParentScopeResolver
    {
        private readonly BacklogItemTypeSchemaEdgeService _BacklogItemTypeSchemaEdgeService = backlogItemTypeSchemaEdgeService;

        public bool IsSupportedScope(string scope)
        {
            return scope == Scopes.ACCOUNT_BACKLOG_ITEM_TYPE_SCHEMA_EDGE;
        }

        public ParentScope? GetParentScope(string scope, string? scopeId)
        {
            if (!IsSupportedScope(scope))
                return null;

            string? parentScopeId = null;
            if (scopeId != null)
            {
                var backlogItemTypeSchemaEdge = _BacklogItemTypeSchemaEdgeService.Get(int.Parse(scopeId));
                parentScopeId = backlogItemTypeSchemaEdge.SchemaID.ToString();
            }

            return new ParentScope(Scopes.ACCOUNT_BACKLOG_ITEM_TYPE_SCHEMA, parentScopeId);
        }
    }
}
