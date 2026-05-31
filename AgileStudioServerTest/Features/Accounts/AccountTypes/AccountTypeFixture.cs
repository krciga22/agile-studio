using AgileStudioServer.Features.Accounts.AccountTypes;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Accounts.AccountTypes
{
    public class AccountTypeFixture : AbstractEntityFixture<AccountTypeRepository>
    {
        private readonly UserFixture _userFixture;

        public AccountTypeFixture(
            AccountTypeRepository accountTypeRepository,
            UserFixture userFixture) : base(accountTypeRepository)
        {
            _userFixture = userFixture;
        }

        public AccountTypeModel Create(
            string? title = null,
            UserModel? createdBy = null)
        {
            title ??= "Test AccountType";
            createdBy ??= _userFixture.Create();

            var accountType = new AccountTypeModel(title)
            {
                CreatedByID = createdBy.ID,
            };

            return _Repository.Create(accountType);
        }

        public AccountTypeModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}