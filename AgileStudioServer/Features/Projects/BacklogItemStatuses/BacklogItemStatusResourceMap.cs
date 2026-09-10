using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Projects.BacklogItemStatuses
{
    public class BacklogItemStatusResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.ProjectsBacklogItemStatus;
        }

        public string GetResourcePermissionScope()
        {
            return Scopes.PROJECT_BACKLOG_ITEM_STATUS;
        }

        public Type GetResourceModelType()
        {
            return typeof(BacklogItemStatusModel);
        }

        public Type GetResourceDtoType()
        {
            return typeof(BacklogItemStatusDto);
        }

        public Type GetResourceDtoCreateType()
        {
            return typeof(BacklogItemStatusPostDto);
        }

        public Type GetResourceDtoUpdateType()
        {
            throw new NotImplementedException();
        }

        public Type GetResourceModelServiceType()
        {
            return typeof(BacklogItemStatusService);
        }

        public Type? GetResourceModelRepositoryType()
        {
            return typeof(BacklogItemStatusRepository);
        }

        public ParentScope GetParentResourceScope(Object model)
        {
            BacklogItemStatusModel backlogItemStatusModel = ((BacklogItemStatusModel)model);
            return new ParentScope(Scopes.PROJECT_BACKLOG_ITEM, backlogItemStatusModel.BacklogItemID.ToString());
        }

        public bool IsPermissionedResource()
        {
            return true;
        }
    }
}