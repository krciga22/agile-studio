using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.AccountsBacklogItemTypeSchema;
        }

        public string GetResourcePermissionScope()
        {
            return Scopes.ACCOUNT_BACKLOG_ITEM_TYPE_SCHEMA;
        }

        public Type GetResourceModelType()
        {
            return typeof(BacklogItemTypeSchemaModel);
        }

        public Type GetResourceDtoType()
        {
            return typeof(BacklogItemTypeSchemaDto);
        }

        public Type GetResourceDtoCreateType()
        {
            return typeof(BacklogItemTypeSchemaPostDto);
        }

        public Type GetResourceDtoUpdateType()
        {
            return typeof(BacklogItemTypeSchemaPatchDto);
        }

        public Type GetResourceModelServiceType()
        {
            return typeof(BacklogItemTypeSchemaService);
        }

        public Type? GetResourceModelRepositoryType()
        {
            return typeof(BacklogItemTypeSchemaRepository);
        }

        public ParentScope GetParentResourceScope(Object model)
        {
            return new ParentScope(Scopes.ACCOUNT, 
                ((BacklogItemTypeSchemaModel)model).AccountID.ToString());
        }

        public bool IsPermissionedResource()
        {
            return true;
        }
    }
}