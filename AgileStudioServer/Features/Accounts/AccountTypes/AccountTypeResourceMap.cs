using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.AccountTypes.AccountTypes;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.AccountTypes
{
    public class AccountTypeResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.AccountsAccountType;
        }

        public string GetResourcePermissionScope()
        {
            return Scopes.GLOBAL;
        }

        public Type GetResourceModelType()
        {
            return typeof(AccountTypeModel);
        }

        public Type GetResourceDtoType()
        {
            return typeof(AccountTypeDto);
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
            return typeof(AccountTypeService);
        }

        public ParentScope GetParentResourceScope(Object model)
        {
            return new ParentScope(Scopes.GLOBAL, null);
        }

        public bool IsPermissionedResource()
        {
            return false;
        }
    }
}