using AgileStudioServer.Core.Services;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEntries
{
    public class BacklogItemTypeSchemaEntryService : AbstractService
    {
        private readonly BacklogItemTypeSchemaEntryRepository _BacklogItemTypeSchemaEntryRepository;

        public BacklogItemTypeSchemaEntryService(BacklogItemTypeSchemaEntryRepository backlogItemTypeSchemaEntryRepository)
        {
            _BacklogItemTypeSchemaEntryRepository = backlogItemTypeSchemaEntryRepository;
        }

        public virtual List<BacklogItemTypeSchemaEntryModel> GetByParentTypeId(int parentTypeId)
        {
            return _BacklogItemTypeSchemaEntryRepository.GetByParentTypeId(parentTypeId);
        }

        public virtual List<BacklogItemTypeSchemaEntryModel> GetByChildTypeId(int childTypeId)
        {
            return _BacklogItemTypeSchemaEntryRepository.GetByChildTypeId(childTypeId);
        }

        public virtual BacklogItemTypeSchemaEntryModel? Get(int id)
        {
            return _BacklogItemTypeSchemaEntryRepository.Get(id);
        }

        public virtual BacklogItemTypeSchemaEntryModel? Get(int parentTypeId, int childTypeId)
        {
            return _BacklogItemTypeSchemaEntryRepository.Get(parentTypeId, childTypeId);
        }

        public virtual BacklogItemTypeSchemaEntryModel Create(BacklogItemTypeSchemaEntryModel backlogItemTypeSchemaEntry)
        {
            return _BacklogItemTypeSchemaEntryRepository.Create(backlogItemTypeSchemaEntry);
        }

        public virtual BacklogItemTypeSchemaEntryModel Update(BacklogItemTypeSchemaEntryModel backlogItemTypeSchemaEntry)
        {
            return _BacklogItemTypeSchemaEntryRepository.Update(backlogItemTypeSchemaEntry);
        }

        public virtual void Delete(BacklogItemTypeSchemaEntryModel backlogItemTypeSchemaEntry)
        {
            _BacklogItemTypeSchemaEntryRepository.Delete(backlogItemTypeSchemaEntry);
        }
    }
}
