namespace AgileStudioServer.Features.Auth.Scopes
{
    public interface IParentScopeResolver
    {
        bool IsSupportedScope(string scope);

        ParentScope? GetParentScope(string scope, string? scopeId);
    }
}
