using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Accounts.Accounts;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaFixture : AbstractEntityFixture<BacklogItemTypeSchemaRepository>
    {
        private readonly UserFixture _userFixture;

        private readonly AccountFixture _accountFixture;

        public BacklogItemTypeSchemaFixture(
            BacklogItemTypeSchemaRepository backlogItemTypeSchemaRepository, 
            UserFixture userFixture,
            AccountFixture accountFixture) : base(backlogItemTypeSchemaRepository)
        {
            _userFixture = userFixture;
            _accountFixture = accountFixture;
        }

        public BacklogItemTypeSchemaModel Create(
            string? title = null,
            UserModel? createdBy = null,
            AccountModel? account = null)
        {
            title ??= "Test BacklogItemTypeSchema";
            createdBy ??= _userFixture.Create();
            account ??= _accountFixture.Create(createdBy: createdBy);

            var backlogItemTypeSchema = new BacklogItemTypeSchemaModel(title, account.ID)
            {
                CreatedById = createdBy.ID
            };
            
            return _Repository.Create(backlogItemTypeSchema);
        }

        public BacklogItemTypeSchemaModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
