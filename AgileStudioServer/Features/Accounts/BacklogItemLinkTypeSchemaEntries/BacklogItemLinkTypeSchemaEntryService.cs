using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryService : AbstractModelService<BacklogItemLinkTypeSchemaEntryModel, int>
    {
        private readonly BacklogItemLinkTypeSchemaEntryRepository _BacklogItemLinkTypeSchemaEntryRepository;

        public BacklogItemLinkTypeSchemaEntryService(BacklogItemLinkTypeSchemaEntryRepository backlogItemLinkTypeSchemaEntryRepository)
        {
            _BacklogItemLinkTypeSchemaEntryRepository = backlogItemLinkTypeSchemaEntryRepository;
        }

        public virtual PaginationResults<BacklogItemLinkTypeSchemaEntryModel> GetByBacklogItemLinkTypeSchemaID(int schemaID)
        {
            return _BacklogItemLinkTypeSchemaEntryRepository.GetBySchemaID(schemaID);
        }

        public override PaginationResults<BacklogItemLinkTypeSchemaEntryModel> GetCollection()
        {
            throw new NotImplementedException();
        }

        public override PaginationResults<BacklogItemLinkTypeSchemaEntryModel> GetSubCollection(string parentResourceType, object[] id)
        {
            switch (parentResourceType)
            {
                case ResourceTypes.AccountsBacklogItemLinkTypeSchema:
                    return GetByBacklogItemLinkTypeSchemaID(int.Parse(id[0].ToString()!));
                default:
                    throw new ArgumentException($"Unsupported parent resource type: {parentResourceType}");
            }
        }

        public override BacklogItemLinkTypeSchemaEntryModel Get(int id)
        {
            return _BacklogItemLinkTypeSchemaEntryRepository.Get(id) ??
                throw new ModelNotFoundException(
                    nameof(BacklogItemLinkTypeSchemaEntryModel), id.ToString());
        }

        public override int GetIdentifier(BacklogItemLinkTypeSchemaEntryModel model)
        {
            return model.ID;
        }

        public override BacklogItemLinkTypeSchemaEntryModel Create(BacklogItemLinkTypeSchemaEntryModel backlogItemLinkTypeSchemaEntry)
        {
            return _BacklogItemLinkTypeSchemaEntryRepository.Create(backlogItemLinkTypeSchemaEntry);
        }

        public override BacklogItemLinkTypeSchemaEntryModel Update(BacklogItemLinkTypeSchemaEntryModel backlogItemLinkTypeSchemaEntry)
        {
            return _BacklogItemLinkTypeSchemaEntryRepository.Update(backlogItemLinkTypeSchemaEntry);
        }

        public override void Delete(BacklogItemLinkTypeSchemaEntryModel backlogItemLinkTypeSchemaEntry)
        {
            _BacklogItemLinkTypeSchemaEntryRepository.Delete(backlogItemLinkTypeSchemaEntry);
        }
    }
}
