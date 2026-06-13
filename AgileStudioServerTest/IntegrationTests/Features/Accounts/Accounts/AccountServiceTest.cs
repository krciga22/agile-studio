using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.AccountTypes;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Roles;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Features.Accounts.Accounts;
using AgileStudioServerTest.Features.Accounts.AccountTypes;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.Accounts
{
    public class AccountServiceTest : AbstractServiceTest
    {
        private readonly AccountService _accountService;

        private readonly AccountFixture _AccountFixture;

        private readonly AccountTypeFixture _AccountTypeFixture;

        private readonly UserFixture _UserFixture;

        private readonly RoleGrantService _RoleGrantService;

        private readonly ServiceContext _ServiceContext;

        public AccountServiceTest(
            DBContext dbContext,
            AccountService accountService,
            AccountFixture accountFixture,
            AccountTypeFixture accountTypeFixture,
            UserFixture userFixture,
            RoleGrantService roleGrantService,
            ServiceContext serviceContext) : base(dbContext)
        {
            _accountService = accountService;
            _AccountFixture = accountFixture;
            _AccountTypeFixture = accountTypeFixture;
            _UserFixture = userFixture;
            _RoleGrantService = roleGrantService;
            _ServiceContext = serviceContext;
        }

        [Fact]
        public void Create_ReturnsAccount()
        {
            AccountTypeModel accountType = _AccountTypeFixture.Create();
            AccountModel account = new(accountType.ID);

            account = _accountService.Create(account);

            Assert.NotNull(account);
            Assert.True(account.ID > 0);
            Assert.Equal(accountType.ID, account.AccountTypeID);
        }

        [Fact]
        public void CreateIndividualAccountForUser_ReturnsAccount()
        {
            var user = _UserFixture.Create();

            AccountModel account = _accountService.CreateIndividualAccountForUser(user);
            List<RoleGrantModel> roleGrants = _RoleGrantService.GetRoleGrants(
                RoleKeys.ACCOUNTS_ACCOUNT_OWNER,
                RoleSubjectTypes.USER,
                user.ID.ToString(),
                Scopes.ACCOUNT,
                account.ID.ToString());

            Assert.NotNull(account);
            Assert.True(account.ID > 0);
            Assert.Equal(AccountTypes.INDIVIDUAL, account.AccountTypeID);
            Assert.Equal(user.ID, account.CreatedByID);
            Assert.Single(roleGrants);
        }

        [Fact]
        public void Get_ReturnsAccount()
        {
            AccountModel account = _AccountFixture.Create();

            var returnedAccount = _accountService.Get(account.ID);

            Assert.NotNull(returnedAccount);
            Assert.Equal(account.ID, returnedAccount.ID);
        }

        [Fact]
        public void GetIndividualAccountForUser_ReturnsAccount()
        {
            UserModel user = _UserFixture.Create();
            int accountTypeId = AccountTypes.INDIVIDUAL;
            AccountTypeModel accountType = GetAccountType(accountTypeId);
            AccountModel account = _AccountFixture.Create(accountType);
            _AccountFixture.GrantAccess(account.ID, user.ID, RoleKeys.ACCOUNTS_ACCOUNT_OWNER);

            var returnedAccount = _accountService.GetIndividualAccountForUser(user.ID);

            Assert.NotNull(returnedAccount);
            Assert.Equal(account.ID, returnedAccount.ID);
            Assert.Equal(account.AccountTypeID, returnedAccount.AccountTypeID);
        }

        [Fact]
        public void GetIndividualAccountForUser_HavingOtherAccountType_ReturnsNull()
        {
            UserModel user = _UserFixture.Create();
            int accountTypeId = AccountTypes.BUSINESS;
            AccountTypeModel accountType = GetAccountType(accountTypeId);
            AccountModel account = _AccountFixture.Create(accountType);
            _AccountFixture.GrantAccess(account.ID, user.ID, RoleKeys.ACCOUNTS_ACCOUNT_OWNER);

            var returnedAccount = _accountService.GetIndividualAccountForUser(user.ID);

            Assert.Null(returnedAccount);
        }

        [Fact]
        public void GetCollection_ReturnsAccountsReadableByCurrentUser()
        {
            var user = _UserFixture.Create();

            var accountType = _AccountTypeFixture.Create();

            var readableAccount1 = _AccountFixture.Create(accountType);
            _AccountFixture.GrantAccess(readableAccount1.ID,
                user.ID, RoleKeys.ACCOUNTS_ACCOUNT_OWNER);

            var readableAccount2 = _AccountFixture.Create(accountType);
            _AccountFixture.GrantAccess(readableAccount2.ID,
                user.ID, RoleKeys.ACCOUNTS_ACCOUNT_OWNER);

            var readableAccounts = new List<AccountModel>
            {
                readableAccount1,  readableAccount2
            };

            var otherAccountType = _AccountTypeFixture.Create();
            var nonReadableAccounts = new List<AccountModel>
            {
                _AccountFixture.Create(otherAccountType),
                _AccountFixture.Create(otherAccountType)
            };

            _ServiceContext.currentUser = IntegrationTestsUtil.GenerateCurrentUserClaimsPrincipal(user.ID);

            PaginationResults<AccountModel> returnedAccounts = _accountService.GetCollection();

            Assert.Equal(readableAccounts.Count, returnedAccounts.Items.Count);
            readableAccounts.ForEach(account =>
                Assert.Contains(returnedAccounts.Items, a => a.ID == account.ID));
        }

        [Fact]
        public void Update_ReturnsUpdatedAccount()
        {
            var accountType1 = _AccountTypeFixture.Create();
            var accountType2 = _AccountTypeFixture.Create();

            AccountModel account = _AccountFixture.Create(accountType1);
            account.AccountTypeID = accountType2.ID;

            account = _accountService.Update(account);

            Assert.NotNull(account);
            Assert.Equal(accountType2.ID, account.AccountTypeID);
        }

        [Fact]
        public void Delete_DeletesAccount()
        {
            AccountModel account = _AccountFixture.Create();

            _accountService.Delete(account);

            Assert.Throws<ModelNotFoundException>(() =>
                _accountService.Get(account.ID));
        }

        private AccountTypeModel GetAccountType(int accountTypeId)
        {
            return _AccountTypeFixture.Get(accountTypeId) ??
                throw new Exception($"AccountType with key {accountTypeId} not found");
        }
    }
}