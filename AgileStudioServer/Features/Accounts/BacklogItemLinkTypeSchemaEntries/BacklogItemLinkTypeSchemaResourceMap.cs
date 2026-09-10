using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemaEntrySchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.AccountsBacklogItemLinkTypeSchemaEntry;
        }

        public string GetResourcePermissionScope()
        {
            return Scopes.ACCOUNT_BACKLOG_ITEM_LINK_TYPE_SCHEMA_ENTRY;
        }

        public Type GetResourceModelType()
        {
            return typeof(BacklogItemLinkTypeSchemaEntryModel);
        }

        public Type GetResourceDtoType()
        {
            return typeof(BacklogItemLinkTypeSchemaEntryDto);
        }

        public Type GetResourceDtoCreateType()
        {
            return typeof(BacklogItemLinkTypeSchemaEntryPostDto);
        }

        public Type GetResourceDtoUpdateType()
        {
            throw new NotImplementedException();
        }

        public Type GetResourceModelServiceType()
        {
            return typeof(BacklogItemLinkTypeSchemaEntryService);
        }

        public Type? GetResourceModelRepositoryType()
        {
            return typeof(BacklogItemLinkTypeSchemaEntryRepository);
        }

        public ParentScope GetParentResourceScope(Object model)
        {
            return new ParentScope(Scopes.ACCOUNT_BACKLOG_ITEM_LINK_TYPE_SCHEMA, 
                ((BacklogItemLinkTypeSchemaEntryModel)model).BacklogItemLinkTypeSchemaID.ToString());
        }

        public bool IsPermissionedResource()
        {
            return true;
        }
    }
}