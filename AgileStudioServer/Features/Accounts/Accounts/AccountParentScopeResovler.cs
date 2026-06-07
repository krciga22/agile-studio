using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServer.Features.Accounts.Accounts
{
    public class AccountParentScopeResovler(AccountService accountService) : IParentScopeResolver
    {
        private readonly AccountService _AccountService = accountService;

        public bool IsSupportedScope(string scope)
        {
            return scope == Scopes.ACCOUNT;
        }

        public ParentScope? GetParentScope(string scope, string? scopeId)
        {
            if (!IsSupportedScope(scope))
                return null;

            // Validate account exists when an ID is provided; parent remains GLOBAL (no id).
            if (scopeId != null)
            {
                _AccountService.Get(int.Parse(scopeId));
            }

            return new ParentScope(Scopes.GLOBAL, null);
        }
    }
}