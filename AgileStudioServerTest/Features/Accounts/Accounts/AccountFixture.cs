using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.AccountTypes;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Accounts.AccountTypes;
using AgileStudioServerTest.Features.Auth.RoleGrants;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Accounts.Accounts
{
    public class AccountFixture : AbstractEntityFixture<AccountRepository>
    {
        private readonly AccountTypeFixture _accountTypeFixture;
        private readonly UserFixture _userFixture;
        private readonly RoleGrantFixture _RoleGrantFixture;

        public AccountFixture(
            AccountRepository accountRepository,
            AccountTypeFixture accountTypeFixture,
            UserFixture userFixture,
            RoleGrantFixture roleGrantFixture) : base(accountRepository)
        {
            _accountTypeFixture = accountTypeFixture;
            _userFixture = userFixture;
            _RoleGrantFixture = roleGrantFixture;
        }

        public AccountModel Create(
            AccountTypeModel? accountType = null,
            DateTime? createdOn = null,
            UserModel? createdBy = null)
        {
            accountType ??= _accountTypeFixture.Create();
            createdOn ??= DateTime.UtcNow;
            createdBy ??= _userFixture.Create();

            var account = new AccountModel(accountType.ID);

            if (createdBy != null){
                account.CreatedByID = createdBy.ID;
            }

            if (createdOn != null){
                account.CreatedOn = (DateTime) createdOn;
            }

            return _Repository.Create(account);
        }

        public AccountModel? Get(int id)
        {
            return _Repository.Get(id);
        }

        public RoleGrantModel GrantAccess(int id, int userId, string roleKey)
        {
            return _RoleGrantFixture.Create(
                subjectType: RoleSubjectTypes.USER,
                subjectID: userId.ToString(),
                roleKey: roleKey,
                scope: Scopes.ACCOUNT,
                scopeID: id.ToString()
            );
        }
    }
}