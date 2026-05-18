using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Projects.BacklogItems
{
    public class BacklogItemResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.BacklogItemsBacklogItem;
        }

        public string GetResourcePermissionScope()
        {
            return Scopes.PROJECT_BACKLOG_ITEM;
        }

        public Type GetResourceModelType()
        {
            return typeof(BacklogItemModel);
        }

        public Type GetResourceDtoType()
        {
            return typeof(BacklogItemDto);
        }

        public Type GetResourceDtoCreateType()
        {
            return typeof(BacklogItemPostDto);
        }

        public Type GetResourceDtoUpdateType()
        {
            return typeof(BacklogItemPatchDto);
        }

        public Type GetResourceModelServiceType()
        {
            return typeof(BacklogItemService);
        }

        public ParentScope GetParentResourceScope(Object model)
        {
            BacklogItemModel backlogItemModel = ((BacklogItemModel)model);
            return new ParentScope(Scopes.PROJECT, backlogItemModel.ProjectID.ToString());
        }
    }
}