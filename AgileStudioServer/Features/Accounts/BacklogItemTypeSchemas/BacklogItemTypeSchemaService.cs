using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaService : AbstractModelService<BacklogItemTypeSchemaModel, int>
    {
        private readonly BacklogItemTypeSchemaRepository _BacklogItemTypeSchemaRepository;

        private readonly ServiceContext _ServiceContext;

        public BacklogItemTypeSchemaService(
            BacklogItemTypeSchemaRepository backlogItemTypeSchemaRepository, 
            ServiceContext serviceContext)
        {
            _BacklogItemTypeSchemaRepository = backlogItemTypeSchemaRepository;
            _ServiceContext = serviceContext;
        }

        public virtual PaginationResults<BacklogItemTypeSchemaModel> GetByAccountID(int accountID)
        {
            return _BacklogItemTypeSchemaRepository.GetByAccountID(accountID, _ServiceContext);
        }

        public override PaginationResults<BacklogItemTypeSchemaModel> GetCollection()
        {
            throw new NotImplementedException();
        }

        public override PaginationResults<BacklogItemTypeSchemaModel> GetSubCollection(string parentResourceType, object[] id)
        {
            switch (parentResourceType)
            {
                case ResourceTypes.AccountsAccount:
                    return GetByAccountID(int.Parse(id[0].ToString()!));
                default:
                    throw new ArgumentException($"Unsupported parent resource type: {parentResourceType}");
            }
        }

        public override BacklogItemTypeSchemaModel Get(int id)
        {
            return _BacklogItemTypeSchemaRepository.Get(id) ??
                throw new ModelNotFoundException(nameof(BacklogItemTypeSchemaModel), id.ToString());
        }

        public override int GetIdentifier(BacklogItemTypeSchemaModel model)
        {
            return model.ID;
        }

        public override BacklogItemTypeSchemaModel Create(BacklogItemTypeSchemaModel backlogItemTypeSchema)
        {
            return _BacklogItemTypeSchemaRepository.Create(backlogItemTypeSchema);
        }

        public override BacklogItemTypeSchemaModel Update(BacklogItemTypeSchemaModel backlogItemTypeSchema)
        {
            return _BacklogItemTypeSchemaRepository.Update(backlogItemTypeSchema);
        }

        public override void Delete(BacklogItemTypeSchemaModel backlogItemTypeSchema)
        {
            _BacklogItemTypeSchemaRepository.Delete(backlogItemTypeSchema);
        }
    }
}
