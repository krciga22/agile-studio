using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.Accounts
{
    public class AccountResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.AccountsAccount;
        }

        public string GetResourcePermissionScope()
        {
            return Scopes.ACCOUNT;
        }

        public Type GetResourceModelType()
        {
            return typeof(AccountModel);
        }

        public Type GetResourceDtoType()
        {
            return typeof(AccountDto);
        }

        public Type GetResourceDtoCreateType()
        {
            throw new NotImplementedException();
        }

        public Type GetResourceDtoUpdateType()
        {
            throw new NotImplementedException();
        }

        public Type GetResourceModelServiceType()
        {
            return typeof(AccountService);
        }

        public ParentScope GetParentResourceScope(Object model)
        {
            return new ParentScope(Scopes.GLOBAL, null);
        }
    }
}