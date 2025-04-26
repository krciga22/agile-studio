using AgileStudioServer.Core.Services;

namespace AgileStudioServer.CoreFeatures.BacklogItems.ChildBacklogItemTypes
{
    public class ChildBacklogItemTypeService : ServiceBase
    {
        private readonly ChildBacklogItemTypeRepository _ChildBacklogItemTypeRepository;

        public ChildBacklogItemTypeService(ChildBacklogItemTypeRepository childBacklogItemTypeRepository)
        {
            _ChildBacklogItemTypeRepository = childBacklogItemTypeRepository;
        }

        public virtual List<ChildBacklogItemTypeModel> GetByParentTypeId(int parentTypeId)
        {
            return _ChildBacklogItemTypeRepository.GetByParentTypeId(parentTypeId);
        }

        public virtual List<ChildBacklogItemTypeModel> GetByChildTypeId(int childTypeId)
        {
            return _ChildBacklogItemTypeRepository.GetByChildTypeId(childTypeId);
        }

        public virtual ChildBacklogItemTypeModel? Get(int id)
        {
            return _ChildBacklogItemTypeRepository.Get(id);
        }

        public virtual ChildBacklogItemTypeModel? Get(int parentTypeId, int childTypeId)
        {
            return _ChildBacklogItemTypeRepository.Get(parentTypeId, childTypeId);
        }

        public virtual ChildBacklogItemTypeModel Create(ChildBacklogItemTypeModel childBacklogItemType)
        {
            return _ChildBacklogItemTypeRepository.Create(childBacklogItemType);
        }

        public virtual ChildBacklogItemTypeModel Update(ChildBacklogItemTypeModel childBacklogItemType)
        {
            return _ChildBacklogItemTypeRepository.Update(childBacklogItemType);
        }

        public virtual void Delete(ChildBacklogItemTypeModel childBacklogItemType)
        {
            _ChildBacklogItemTypeRepository.Delete(childBacklogItemType);
        }
    }
}
