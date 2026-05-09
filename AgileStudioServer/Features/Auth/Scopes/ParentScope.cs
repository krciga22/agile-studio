namespace AgileStudioServer.Features.Auth.Scopes
{
    public class ParentScope (string scope, string? scopeId)
    {
        public string Scope { get; } = scope;

        public string? ScopeId { get; } = scopeId;
    }
}
