using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.AccountTypes;

namespace AgileStudioServer.Features.AccountTypes.AccountTypes
{
    public class AccountTypeService : AbstractModelService<AccountTypeModel, int>
    {
        private readonly AccountTypeRepository _AccountTypeRepository;

        public AccountTypeService(AccountTypeRepository accountTypeRepository)
        {
            _AccountTypeRepository = accountTypeRepository;
        }

        public override PaginationResults<AccountTypeModel> GetCollection()
        {
            List<AccountTypeModel> accountTypes = _AccountTypeRepository.GetAll();
            return new PaginationResults<AccountTypeModel>(
                accountTypes, accountTypes.Count, 1, accountTypes.Count);
        }

        public override PaginationResults<AccountTypeModel> GetSubCollection(string parentResourceType, object[] id)
        {
            throw new NotImplementedException();
        }

        /// <exception cref="ModelNotFoundException"></exception>
        public override AccountTypeModel Get(int id)
        {
            var accountType = _AccountTypeRepository.Get(id) ??
                throw new ModelNotFoundException(nameof(AccountTypeModel), id.ToString());

            return accountType;
        }

        public override AccountTypeModel Create(AccountTypeModel model)
        {
            throw new NotImplementedException();
        }

        public override AccountTypeModel Update(AccountTypeModel model)
        {
            throw new NotImplementedException();
        }

        public override void Delete(AccountTypeModel model)
        {
            throw new NotImplementedException();
        }

        public override int GetIdentifier(AccountTypeModel accountType)
        {
            return accountType.ID;
        }
    }
}