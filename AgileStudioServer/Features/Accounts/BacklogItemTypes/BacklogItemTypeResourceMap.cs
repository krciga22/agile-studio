using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypes
{
    public class BacklogItemTypeResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.AccountsBacklogItemType;
        }

        public string GetResourcePermissionScope()
        {
            return Scopes.ACCOUNT_BACKLOG_ITEM_TYPE;
        }

        public Type GetResourceModelType()
        {
            return typeof(BacklogItemTypeModel);
        }

        public Type GetResourceDtoType()
        {
            return typeof(BacklogItemTypeDto);
        }

        public Type GetResourceDtoCreateType()
        {
            return typeof(BacklogItemTypePostDto);
        }

        public Type GetResourceDtoUpdateType()
        {
            return typeof(BacklogItemTypePatchDto);
        }

        public Type GetResourceModelServiceType()
        {
            return typeof(BacklogItemTypeService);
        }

        public Type? GetResourceModelRepositoryType()
        {
            return typeof(BacklogItemTypeRepository);
        }

        public ParentScope GetParentResourceScope(Object model)
        {
            return new ParentScope(Scopes.ACCOUNT, 
                ((BacklogItemTypeModel)model).AccountID.ToString());
        }

        public bool IsPermissionedResource()
        {
            return true;
        }
    }
}