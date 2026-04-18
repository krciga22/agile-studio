using AgileStudioServer.Core.Services;

namespace AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeService : AbstractService
    {
        private readonly BacklogItemLinkTypeRepository _BacklogItemLinkTypeRepository;

        public BacklogItemLinkTypeService(BacklogItemLinkTypeRepository backlogItemLinkTypeRepository)
        {
            _BacklogItemLinkTypeRepository = backlogItemLinkTypeRepository;
        }

        public virtual BacklogItemLinkTypeModel? Get(int id)
        {
            return _BacklogItemLinkTypeRepository.Get(id);
        }

        public virtual BacklogItemLinkTypeModel Create(BacklogItemLinkTypeModel backlogItemLinkType)
        {
            return _BacklogItemLinkTypeRepository.Create(backlogItemLinkType);
        }

        public virtual BacklogItemLinkTypeModel Update(BacklogItemLinkTypeModel backlogItemLinkType)
        {
            return _BacklogItemLinkTypeRepository.Update(backlogItemLinkType);
        }

        public virtual void Delete(BacklogItemLinkTypeModel backlogItemLinkType)
        {
            _BacklogItemLinkTypeRepository.Delete(backlogItemLinkType);
        }
    }
}
