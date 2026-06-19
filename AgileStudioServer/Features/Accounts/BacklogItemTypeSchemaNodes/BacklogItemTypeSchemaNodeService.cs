using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes
{
    public class BacklogItemTypeSchemaNodeService : AbstractService
    {
        private readonly BacklogItemTypeSchemaNodeRepository _BacklogItemTypeSchemaNodeRepository;

        public BacklogItemTypeSchemaNodeService(BacklogItemTypeSchemaNodeRepository backlogItemTypeSchemaNodeRepository)
        {
            _BacklogItemTypeSchemaNodeRepository = backlogItemTypeSchemaNodeRepository;
        }

        public virtual BacklogItemTypeSchemaNodeModel? Get(int id)
        {
            return _BacklogItemTypeSchemaNodeRepository.Get(id);
        }

        /// <exception cref="ModelNotFoundException"></exception>
        public virtual BacklogItemTypeSchemaNodeModel GetBySchemaId(int schemaId)
        {
            var node = _BacklogItemTypeSchemaNodeRepository.GetBySchemaId(schemaId) ?? 
                throw new ModelNotFoundException(nameof(BacklogItemTypeSchemaNodeModel), schemaId.ToString());

            return node;
        }

        public virtual BacklogItemTypeSchemaNodeModel Create(BacklogItemTypeSchemaNodeModel backlogItemTypeSchemaNode)
        {
            return _BacklogItemTypeSchemaNodeRepository.Create(backlogItemTypeSchemaNode);
        }

        public virtual BacklogItemTypeSchemaNodeModel Update(BacklogItemTypeSchemaNodeModel backlogItemTypeSchemaNode)
        {
            return _BacklogItemTypeSchemaNodeRepository.Update(backlogItemTypeSchemaNode);
        }

        public virtual void Delete(BacklogItemTypeSchemaNodeModel backlogItemTypeSchemaNode)
        {
            _BacklogItemTypeSchemaNodeRepository.Delete(backlogItemTypeSchemaNode);
        }
    }
}
