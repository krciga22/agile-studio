namespace AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaService
    {
        private readonly BacklogItemLinkTypeSchemaRepository _BacklogItemLinkTypeSchemaRepository;

        public BacklogItemLinkTypeSchemaService(BacklogItemLinkTypeSchemaRepository backlogItemLinkTypeSchemaRepository)
        {
            _BacklogItemLinkTypeSchemaRepository = backlogItemLinkTypeSchemaRepository;
        }

        public virtual BacklogItemLinkTypeSchemaModel? Get(int id)
        {
            return _BacklogItemLinkTypeSchemaRepository.Get(id);
        }

        public virtual BacklogItemLinkTypeSchemaModel Create(BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchema)
        {
            return _BacklogItemLinkTypeSchemaRepository.Create(backlogItemLinkTypeSchema);
        }

        public virtual BacklogItemLinkTypeSchemaModel Update(BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchema)
        {
            return _BacklogItemLinkTypeSchemaRepository.Update(backlogItemLinkTypeSchema);
        }

        public virtual void Delete(BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchema)
        {
            _BacklogItemLinkTypeSchemaRepository.Delete(backlogItemLinkTypeSchema);
        }
    }
}
