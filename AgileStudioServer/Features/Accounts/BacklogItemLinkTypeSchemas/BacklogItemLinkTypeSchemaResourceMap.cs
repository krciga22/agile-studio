using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemaSchemas
{
    public class BacklogItemLinkTypeSchemaResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.AccountsBacklogItemLinkTypeSchema;
        }

        public string GetResourcePermissionScope()
        {
            return Scopes.ACCOUNT_BACKLOG_ITEM_LINK_TYPE_SCHEMA;
        }

        public Type GetResourceModelType()
        {
            return typeof(BacklogItemLinkTypeSchemaModel);
        }

        public Type GetResourceDtoType()
        {
            return typeof(BacklogItemLinkTypeSchemaDto);
        }

        public Type GetResourceDtoCreateType()
        {
            return typeof(BacklogItemLinkTypeSchemaPostDto);
        }

        public Type GetResourceDtoUpdateType()
        {
            return typeof(BacklogItemLinkTypeSchemaPatchDto);
        }

        public Type GetResourceModelServiceType()
        {
            return typeof(BacklogItemLinkTypeSchemaService);
        }

        public ParentScope GetParentResourceScope(Object model)
        {
            return new ParentScope(Scopes.ACCOUNT, 
                ((BacklogItemLinkTypeSchemaModel)model).AccountID.ToString());
        }

        public bool IsPermissionedResource()
        {
            return true;
        }
    }
}