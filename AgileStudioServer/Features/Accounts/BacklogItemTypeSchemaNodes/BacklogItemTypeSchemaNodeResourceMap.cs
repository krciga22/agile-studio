using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes
{
    public class BacklogItemTypeSchemaNodeResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.AccountsBacklogItemTypeSchemaNode;
        }

        public string GetResourcePermissionScope()
        {
            return Scopes.ACCOUNT_BACKLOG_ITEM_TYPE_SCHEMA_NODE;
        }

        public Type GetResourceModelType()
        {
            return typeof(BacklogItemTypeSchemaNodeModel);
        }

        public Type GetResourceDtoType()
        {
            return typeof(BacklogItemTypeSchemaNodeDto);
        }

        public Type GetResourceDtoCreateType()
        {
            return typeof(BacklogItemTypeSchemaNodePostDto);
        }

        public Type GetResourceDtoUpdateType()
        {
            throw new NotImplementedException();
        }

        public Type GetResourceModelServiceType()
        {
            return typeof(BacklogItemTypeSchemaNodeService);
        }

        public Type? GetResourceModelRepositoryType()
        {
            return typeof(BacklogItemTypeSchemaNodeRepository);
        }

        public ParentScope GetParentResourceScope(Object model)
        {
            return new ParentScope(Scopes.ACCOUNT_BACKLOG_ITEM_TYPE_SCHEMA, 
                ((BacklogItemTypeSchemaNodeModel)model).SchemaID.ToString());
        }

        public bool IsPermissionedResource()
        {
            return true;
        }
    }
}