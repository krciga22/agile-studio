using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Roles;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.Accounts
{
    public class AccountService : AbstractModelService<AccountModel, int>
    {
        private readonly AccountRepository _AccountRepository;
        private readonly ServiceContext _ServiceContext;
        private readonly RoleGrantService _RoleGrantService;

        public AccountService(
            AccountRepository accountRepository,
            ServiceContext serviceContext,
            RoleGrantService roleGrantService)
        {
            _AccountRepository = accountRepository;
            _ServiceContext = serviceContext;
            _RoleGrantService = roleGrantService;
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

        public AccountModel CreateIndividualAccountForUser(UserModel user)
        {
            AccountModel account = new (AccountTypes.AccountTypes.INDIVIDUAL)
            {
                CreatedByID = user.ID
            };
            account = _AccountRepository.Create(account);

            var roleGrant = new RoleGrantModel(
                    RoleKeys.ACCOUNTS_ACCOUNT_OWNER,
                    RoleSubjectTypes.USER,
                    user.ID.ToString(),
                    Scopes.ACCOUNT
                )
            {
                ScopeID = account.ID.ToString(),
                CreatedByID = user.ID
            };

            _RoleGrantService.Create(roleGrant);

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