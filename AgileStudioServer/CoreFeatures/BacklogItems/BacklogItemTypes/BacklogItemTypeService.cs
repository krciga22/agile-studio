namespace AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypes
{
    public class BacklogItemTypeService
    {
        private readonly BacklogItemTypeRepository _BacklogItemTypeRepository;

        public BacklogItemTypeService(BacklogItemTypeRepository backlogItemTypeRepository)
        {
            _BacklogItemTypeRepository = backlogItemTypeRepository;
        }

        public virtual List<BacklogItemTypeModel> GetByBacklogItemTypeSchemaId(int backlogItemTypeSchemaId)
        {
            return _BacklogItemTypeRepository.GetByBacklogItemTypeSchemaId(backlogItemTypeSchemaId);
        }

        public virtual BacklogItemTypeModel? Get(int id)
        {
            return _BacklogItemTypeRepository.Get(id);
        }

        public virtual BacklogItemTypeModel Create(BacklogItemTypeModel backlogItemType)
        {
            return _BacklogItemTypeRepository.Create(backlogItemType);
        }

        public virtual BacklogItemTypeModel Update(BacklogItemTypeModel backlogItemType)
        {
            return _BacklogItemTypeRepository.Update(backlogItemType);
        }

        public virtual void Delete(BacklogItemTypeModel backlogItemType)
        {
            _BacklogItemTypeRepository.Delete(backlogItemType);
        }
    }
}
