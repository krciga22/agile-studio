using AgileStudioServer.Core.Services;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges
{
    public class BacklogItemTypeSchemaEdgeService : AbstractService
    {
        private readonly BacklogItemTypeSchemaEdgeRepository _BacklogItemTypeSchemaEdgeRepository;

        public BacklogItemTypeSchemaEdgeService(BacklogItemTypeSchemaEdgeRepository backlogItemTypeSchemaEdgeRepository)
        {
            _BacklogItemTypeSchemaEdgeRepository = backlogItemTypeSchemaEdgeRepository;
        }

        public virtual List<BacklogItemTypeSchemaEdgeModel> GetByFromTypeId(int? fromTypeId, int schemaId = 0)
        {
            return _BacklogItemTypeSchemaEdgeRepository.GetByFromTypeId(fromTypeId, schemaId);
        }

        public virtual List<BacklogItemTypeSchemaEdgeModel> GetByToTypeId(int toTypeId, int schemaId = 0)
        {
            return _BacklogItemTypeSchemaEdgeRepository.GetByToTypeId(toTypeId, schemaId);
        }

        public virtual List<BacklogItemTypeSchemaEdgeModel> GetBySchemaId(int schemaId)
        {
            return _BacklogItemTypeSchemaEdgeRepository.GetBySchemaId(schemaId);
        }

        /// <exception cref="ModelNotFoundException"></exception>
        public virtual BacklogItemTypeSchemaEdgeModel Get(int id)
        {
            return _BacklogItemTypeSchemaEdgeRepository.Get(id) ??
                throw new ModelNotFoundException(
                    nameof(BacklogItemTypeSchemaEdgeModel), id.ToString());
        }

        /// <exception cref="ModelNotFoundException"></exception>
        public virtual BacklogItemTypeSchemaEdgeModel Get(int? fromTypeId, int toTypeId, int schemaId)
        {
            return _BacklogItemTypeSchemaEdgeRepository.Get(fromTypeId, toTypeId, schemaId) ??
                throw ModelNotFoundException.FromCompositeKey(
                    nameof(BacklogItemTypeSchemaEdgeModel),
                    [fromTypeId ?? 0, toTypeId, schemaId]);
        }

        public virtual BacklogItemTypeSchemaEdgeModel Create(BacklogItemTypeSchemaEdgeModel backlogItemTypeSchemaEdge)
        {
            return _BacklogItemTypeSchemaEdgeRepository.Create(backlogItemTypeSchemaEdge);
        }

        public virtual BacklogItemTypeSchemaEdgeModel Update(BacklogItemTypeSchemaEdgeModel backlogItemTypeSchemaEdge)
        {
            return _BacklogItemTypeSchemaEdgeRepository.Update(backlogItemTypeSchemaEdge);
        }

        public virtual void Delete(BacklogItemTypeSchemaEdgeModel backlogItemTypeSchemaEdge)
        {
            _BacklogItemTypeSchemaEdgeRepository.Delete(backlogItemTypeSchemaEdge);
        }
    }
}
