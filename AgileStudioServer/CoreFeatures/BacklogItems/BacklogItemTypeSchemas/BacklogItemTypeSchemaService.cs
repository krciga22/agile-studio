namespace AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaService
    {
        private readonly BacklogItemTypeSchemaRepository _BacklogItemTypeSchemaRepository;

        public BacklogItemTypeSchemaService(BacklogItemTypeSchemaRepository backlogItemTypeSchemaRepository)
        {
            _BacklogItemTypeSchemaRepository = backlogItemTypeSchemaRepository;
        }

        public virtual List<BacklogItemTypeSchemaModel> GetAll()
        {
            return _BacklogItemTypeSchemaRepository.GetAll();
        }

        public virtual BacklogItemTypeSchemaModel? Get(int id)
        {
            return _BacklogItemTypeSchemaRepository.Get(id);
        }

        public virtual BacklogItemTypeSchemaModel Create(BacklogItemTypeSchemaModel backlogItemTypeSchema)
        {
            return _BacklogItemTypeSchemaRepository.Create(backlogItemTypeSchema);
        }

        public virtual BacklogItemTypeSchemaModel Update(BacklogItemTypeSchemaModel backlogItemTypeSchema)
        {
            return _BacklogItemTypeSchemaRepository.Update(backlogItemTypeSchema);
        }

        public virtual void Delete(BacklogItemTypeSchemaModel backlogItemTypeSchema)
        {
            _BacklogItemTypeSchemaRepository.Delete(backlogItemTypeSchema);
        }
    }
}
