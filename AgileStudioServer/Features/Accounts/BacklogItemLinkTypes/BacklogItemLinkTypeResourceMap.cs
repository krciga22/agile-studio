using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.AccountsBacklogItemLinkType;
        }

        public string GetResourcePermissionScope()
        {
            return Scopes.ACCOUNT_BACKLOG_ITEM_LINK_TYPE;
        }

        public Type GetResourceModelType()
        {
            return typeof(BacklogItemLinkTypeModel);
        }

        public Type GetResourceDtoType()
        {
            return typeof(BacklogItemLinkTypeDto);
        }

        public Type GetResourceDtoCreateType()
        {
            return typeof(BacklogItemLinkTypePostDto);
        }

        public Type GetResourceDtoUpdateType()
        {
            return typeof(BacklogItemLinkTypePatchDto);
        }

        public Type GetResourceModelServiceType()
        {
            return typeof(BacklogItemLinkTypeService);
        }

        public ParentScope GetParentResourceScope(Object model)
        {
            return new ParentScope(Scopes.ACCOUNT, 
                ((BacklogItemLinkTypeModel)model).AccountID.ToString());
        }

        public bool IsPermissionedResource()
        {
            return true;
        }
    }
}