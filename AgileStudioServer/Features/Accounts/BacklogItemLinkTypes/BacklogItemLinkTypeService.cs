using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeService : AbstractModelService<BacklogItemLinkTypeModel, int>
    {
        private readonly BacklogItemLinkTypeRepository _BacklogItemLinkTypeRepository;

        public BacklogItemLinkTypeService(BacklogItemLinkTypeRepository backlogItemLinkTypeRepository)
        {
            _BacklogItemLinkTypeRepository = backlogItemLinkTypeRepository;
        }

        public virtual PaginationResults<BacklogItemLinkTypeModel> GetByAccountID(int accountID)
        {
            return _BacklogItemLinkTypeRepository.GetByAccountID(accountID);
        }

        public virtual PaginationResults<BacklogItemLinkTypeModel> GetByProjectID(int projectID)
        {
            return _BacklogItemLinkTypeRepository.GetByProjectID(projectID);
        }

        public override PaginationResults<BacklogItemLinkTypeModel> GetCollection()
        {
            throw new NotImplementedException();
        }

        public override PaginationResults<BacklogItemLinkTypeModel> GetSubCollection(string parentResourceType, object[] id)
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
        public override BacklogItemLinkTypeModel Get(int id)
        {
            return _BacklogItemLinkTypeRepository.Get(id) ??
                throw new ModelNotFoundException(nameof(BacklogItemLinkTypeModel), id.ToString());
        }

        public override int GetIdentifier(BacklogItemLinkTypeModel backlogItemLinkType)
        {
            return backlogItemLinkType.ID;
        }

        public override BacklogItemLinkTypeModel Create(BacklogItemLinkTypeModel backlogItemLinkType)
        {
            return _BacklogItemLinkTypeRepository.Create(backlogItemLinkType);
        }

        public override BacklogItemLinkTypeModel Update(BacklogItemLinkTypeModel backlogItemLinkType)
        {
            return _BacklogItemLinkTypeRepository.Update(backlogItemLinkType);
        }

        public override void Delete(BacklogItemLinkTypeModel backlogItemLinkType)
        {
            _BacklogItemLinkTypeRepository.Delete(backlogItemLinkType);
        }
    }
}
