using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;

namespace AgileStudioServer.Features.Accounts.Accounts
{
    public class AccountService : AbstractModelService<AccountModel, int>
    {
        private readonly AccountRepository _AccountRepository;
        private readonly ServiceContext _ServiceContext;

        public AccountService(
            AccountRepository accountRepository,
            ServiceContext serviceContext)
        {
            _AccountRepository = accountRepository;
            _ServiceContext = serviceContext;
        }

        public override PaginationResults<AccountModel> GetCollection()
        {
            return _AccountRepository.GetAccountsForCurrentUser(_ServiceContext);
        }

        public override PaginationResults<AccountModel> GetSubCollection(string parentResourceType, object[] id)
        {
            throw new NotImplementedException();
        }

        /// <exception cref="ModelNotFoundException"></exception>
        public override AccountModel Get(int id)
        {
            var account = _AccountRepository.Get(id) ??
                throw new ModelNotFoundException(nameof(AccountModel), id.ToString());

            return account;
        }

        public override AccountModel Create(AccountModel model)
        {
            int? createdById = _ServiceContext.GetCurrentUserId();
            if (createdById != null)
            {
                model.CreatedByID = createdById;
            }

            AccountModel account = _AccountRepository.Create(model);

            return account;
        }

        public override AccountModel Update(AccountModel model)
        {
            return _AccountRepository.Update(model);
        }

        public override void Delete(AccountModel model)
        {
            _AccountRepository.Delete(model);
        }

        public override int GetIdentifier(AccountModel account)
        {
            return account.ID;
        }
    }
}