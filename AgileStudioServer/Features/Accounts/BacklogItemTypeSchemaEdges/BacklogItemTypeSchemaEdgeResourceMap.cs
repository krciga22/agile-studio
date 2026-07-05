using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges
{
    public class BacklogItemTypeSchemaEdgeResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.AccountsBacklogItemTypeSchemaEdge;
        }

        public string GetResourcePermissionScope()
        {
            return Scopes.ACCOUNT_BACKLOG_ITEM_TYPE_SCHEMA_EDGE;
        }

        public Type GetResourceModelType()
        {
            return typeof(BacklogItemTypeSchemaEdgeModel);
        }

        public Type GetResourceDtoType()
        {
            return typeof(BacklogItemTypeSchemaEdgeDto);
        }

        public Type GetResourceDtoCreateType()
        {
            return typeof(BacklogItemTypeSchemaEdgePostDto);
        }

        public Type GetResourceDtoUpdateType()
        {
            throw new NotImplementedException();
        }

        public Type GetResourceModelServiceType()
        {
            return typeof(BacklogItemTypeSchemaEdgeService);
        }

        public ParentScope GetParentResourceScope(Object model)
        {
            return new ParentScope(Scopes.ACCOUNT_BACKLOG_ITEM_TYPE_SCHEMA, 
                ((BacklogItemTypeSchemaEdgeModel)model).SchemaID.ToString());
        }

        public bool IsPermissionedResource()
        {
            return true;
        }
    }
}