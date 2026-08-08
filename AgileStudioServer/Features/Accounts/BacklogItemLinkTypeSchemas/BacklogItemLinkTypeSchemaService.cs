using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaService : AbstractModelService<BacklogItemLinkTypeSchemaModel, int>
    {
        private readonly BacklogItemLinkTypeSchemaRepository _BacklogItemLinkTypeSchemaRepository;

        public BacklogItemLinkTypeSchemaService(BacklogItemLinkTypeSchemaRepository backlogItemLinkTypeSchemaRepository)
        {
            _BacklogItemLinkTypeSchemaRepository = backlogItemLinkTypeSchemaRepository;
        }

        public virtual PaginationResults<BacklogItemLinkTypeSchemaModel> GetByAccountID(int accountID)
        {
            return _BacklogItemLinkTypeSchemaRepository.GetByAccountID(accountID);
        }

        public override PaginationResults<BacklogItemLinkTypeSchemaModel> GetCollection()
        {
            throw new NotImplementedException();
        }

        public override PaginationResults<BacklogItemLinkTypeSchemaModel> GetSubCollection(string parentResourceType, object[] id)
        {
            switch (parentResourceType)
            {
                case ResourceTypes.AccountsAccount:
                    return GetByAccountID(int.Parse(id[0].ToString()!));
                default:
                    throw new ArgumentException($"Unsupported parent resource type: {parentResourceType}");
            }
        }

        /// <exception cref="ModelNotFoundException"></exception>
        public override BacklogItemLinkTypeSchemaModel Get(int id)
        {
            return _BacklogItemLinkTypeSchemaRepository.Get(id) ?? 
                throw new ModelNotFoundException(nameof(BacklogItemLinkTypeSchemaModel), id.ToString());
        }

        public override int GetIdentifier(BacklogItemLinkTypeSchemaModel model)
        {
            return model.ID;
        }

        public override BacklogItemLinkTypeSchemaModel Create(BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchema)
        {
            return _BacklogItemLinkTypeSchemaRepository.Create(backlogItemLinkTypeSchema);
        }

        public override BacklogItemLinkTypeSchemaModel Update(BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchema)
        {
            return _BacklogItemLinkTypeSchemaRepository.Update(backlogItemLinkTypeSchema);
        }

        public override void Delete(BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchema)
        {
            _BacklogItemLinkTypeSchemaRepository.Delete(backlogItemLinkTypeSchema);
        }
    }
}
