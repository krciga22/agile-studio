using AgileStudioServer.Core.Services;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryService : AbstractService
    {
        private readonly BacklogItemLinkTypeSchemaEntryRepository _BacklogItemLinkTypeSchemaEntryRepository;

        public BacklogItemLinkTypeSchemaEntryService(BacklogItemLinkTypeSchemaEntryRepository backlogItemLinkTypeSchemaEntryRepository)
        {
            _BacklogItemLinkTypeSchemaEntryRepository = backlogItemLinkTypeSchemaEntryRepository;
        }

        public virtual BacklogItemLinkTypeSchemaEntryModel? Get(int id)
        {
            return _BacklogItemLinkTypeSchemaEntryRepository.Get(id);
        }

        public virtual BacklogItemLinkTypeSchemaEntryModel Create(BacklogItemLinkTypeSchemaEntryModel backlogItemLinkTypeSchemaEntry)
        {
            return _BacklogItemLinkTypeSchemaEntryRepository.Create(backlogItemLinkTypeSchemaEntry);
        }

        public virtual BacklogItemLinkTypeSchemaEntryModel Update(BacklogItemLinkTypeSchemaEntryModel backlogItemLinkTypeSchemaEntry)
        {
            return _BacklogItemLinkTypeSchemaEntryRepository.Update(backlogItemLinkTypeSchemaEntry);
        }

        public virtual void Delete(BacklogItemLinkTypeSchemaEntryModel backlogItemLinkTypeSchemaEntry)
        {
            _BacklogItemLinkTypeSchemaEntryRepository.Delete(backlogItemLinkTypeSchemaEntry);
        }
    }
}
