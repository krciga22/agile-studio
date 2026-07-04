using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes
{
    public class BacklogItemTypeSchemaNodeService : AbstractModelService<BacklogItemTypeSchemaNodeModel, int>
    {
        private readonly BacklogItemTypeSchemaNodeRepository _BacklogItemTypeSchemaNodeRepository;

        public BacklogItemTypeSchemaNodeService(BacklogItemTypeSchemaNodeRepository backlogItemTypeSchemaNodeRepository)
        {
            _BacklogItemTypeSchemaNodeRepository = backlogItemTypeSchemaNodeRepository;
        }

        public override PaginationResults<BacklogItemTypeSchemaNodeModel> GetCollection()
        {
            throw new NotImplementedException();
        }

        public override PaginationResults<BacklogItemTypeSchemaNodeModel> GetSubCollection(string parentResourceType, object[] id)
        {
            switch (parentResourceType)
            {
                case ResourceTypes.AccountsBacklogItemTypeSchema:
                    return GetBySchemaId(int.Parse(id[0].ToString()!));
                default:
                    throw new ArgumentException($"Unsupported parent resource type: {parentResourceType}");
            }
        }

        public override BacklogItemTypeSchemaNodeModel Get(int id)
        {
            return _BacklogItemTypeSchemaNodeRepository.Get(id) ??
                throw new ModelNotFoundException(
                        nameof(BacklogItemTypeSchemaNodeModel), 
                        id.ToString());
        }

        /// <exception cref="ModelNotFoundException"></exception>
        public virtual BacklogItemTypeSchemaNodeModel Get(int schemaId, int backlogItemTypeId)
        {
            return _BacklogItemTypeSchemaNodeRepository.Get(schemaId, backlogItemTypeId) ??
                throw ModelNotFoundException.FromCompositeKey(
                        nameof(BacklogItemTypeSchemaNodeModel), 
                        [schemaId, backlogItemTypeId]);
        }

        public virtual PaginationResults<BacklogItemTypeSchemaNodeModel> GetBySchemaId(int schemaId)
        {
            return _BacklogItemTypeSchemaNodeRepository.GetBySchemaId(schemaId);
        }

        public override int GetIdentifier(BacklogItemTypeSchemaNodeModel model)
        {
            return model.ID;
        }

        public override BacklogItemTypeSchemaNodeModel Create(BacklogItemTypeSchemaNodeModel backlogItemTypeSchemaNode)
        {
            return _BacklogItemTypeSchemaNodeRepository.Create(backlogItemTypeSchemaNode);
        }

        public override BacklogItemTypeSchemaNodeModel Update(BacklogItemTypeSchemaNodeModel backlogItemTypeSchemaNode)
        {
            return _BacklogItemTypeSchemaNodeRepository.Update(backlogItemTypeSchemaNode);
        }

        public override void Delete(BacklogItemTypeSchemaNodeModel backlogItemTypeSchemaNode)
        {
            _BacklogItemTypeSchemaNodeRepository.Delete(backlogItemTypeSchemaNode);
        }
    }
}
