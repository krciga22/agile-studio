using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypes
{
    public class BacklogItemTypeService : AbstractModelService<BacklogItemTypeModel, int>
    {
        private readonly BacklogItemTypeRepository _BacklogItemTypeRepository;

        private readonly ServiceContext _ServiceContext;

        public BacklogItemTypeService(
            BacklogItemTypeRepository backlogItemTypeRepository,
            ServiceContext serviceContext)
        {
            _BacklogItemTypeRepository = backlogItemTypeRepository;
            _ServiceContext = serviceContext;
        }

        public virtual List<BacklogItemTypeModel> GetByBacklogItemTypeSchemaId(int backlogItemTypeSchemaId)
        {
            return _BacklogItemTypeRepository.GetByBacklogItemTypeSchemaId(backlogItemTypeSchemaId);
        }

        public virtual PaginationResults<BacklogItemTypeModel> GetByAccountID(int accountID)
        {
            return _BacklogItemTypeRepository.GetByAccountID(accountID, _ServiceContext);
        }

        public override PaginationResults<BacklogItemTypeModel> GetCollection()
        {
            throw new NotImplementedException();
        }

        public override PaginationResults<BacklogItemTypeModel> GetSubCollection(String parentResourceType, Object[] id)
        {
            switch (parentResourceType)
            {
                case ResourceTypes.AccountsAccount:
                    return GetByAccountID(int.Parse(id[0].ToString()!));
                default:
                    throw new ArgumentException($"Unsupported parent resource type: {parentResourceType}");
            }
        }

        public override BacklogItemTypeModel Get(int id)
        {
            return _BacklogItemTypeRepository.Get(id) ??
                throw new ModelNotFoundException(nameof(BacklogItemTypeModel), id.ToString());
        }

        public override int GetIdentifier(BacklogItemTypeModel backlogItemType)
        {
            return backlogItemType.ID;
        }

        public override BacklogItemTypeModel Create(BacklogItemTypeModel backlogItemType)
        {
            return _BacklogItemTypeRepository.Create(backlogItemType);
        }

        public override BacklogItemTypeModel Update(BacklogItemTypeModel backlogItemType)
        {
            return _BacklogItemTypeRepository.Update(backlogItemType);
        }

        public override void Delete(BacklogItemTypeModel backlogItemType)
        {
            _BacklogItemTypeRepository.Delete(backlogItemType);
        }
    }
}
