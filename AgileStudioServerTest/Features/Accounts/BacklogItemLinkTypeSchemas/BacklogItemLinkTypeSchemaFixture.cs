using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Accounts.Accounts;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaFixture : AbstractEntityFixture<BacklogItemLinkTypeSchemaRepository>
    {
        private readonly UserFixture _userFixture;
        private readonly AccountFixture _AccountFixture;

        public BacklogItemLinkTypeSchemaFixture(
            BacklogItemLinkTypeSchemaRepository backlogItemLinkTypeSchemaRepository, 
            UserFixture userFixture,
            AccountFixture accountFixture) : base(backlogItemLinkTypeSchemaRepository)
        {
            _userFixture = userFixture;
            _AccountFixture = accountFixture;
        }

        public BacklogItemLinkTypeSchemaModel Create(
            string? title = null,
            UserModel? createdBy = null,
            AccountModel? account = null)
        {
            title ??= "Test BacklogItemLinkTypeSchema";
            createdBy ??= _userFixture.Create();
            account ??= _AccountFixture.Create(createdBy: createdBy);

            var backlogItemLinkTypeSchema = new BacklogItemLinkTypeSchemaModel(title, account.ID)
            {
                CreatedByID = createdBy.ID,
            };
            
            return _Repository.Create(backlogItemLinkTypeSchema);
        }

        public BacklogItemLinkTypeSchemaModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
