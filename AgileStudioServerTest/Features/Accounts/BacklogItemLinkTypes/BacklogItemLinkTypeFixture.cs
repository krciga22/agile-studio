using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypes;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Accounts.Accounts;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeFixture : AbstractEntityFixture<BacklogItemLinkTypeRepository>
    {
        private readonly UserFixture _userFixture;
        private readonly AccountFixture _AccountFixture;

        public BacklogItemLinkTypeFixture(
            BacklogItemLinkTypeRepository backlogItemLinkTypeRepository, 
            UserFixture userFixture,
            AccountFixture accountFixture) : base(backlogItemLinkTypeRepository)
        {
            _userFixture = userFixture;
            _AccountFixture = accountFixture;
        }

        public BacklogItemLinkTypeModel Create(
            string? title = null,
            string? titleOpposite = null,
            AccountModel? account = null,
            UserModel? createdBy = null)
        {
            title ??= "Test BacklogItemLinkType";
            titleOpposite ??= "Test BacklogItemLinkTypeOpposite";
            createdBy ??= _userFixture.Create();
            account ??= _AccountFixture.Create(createdBy: createdBy);

            var backlogItemLinkType = new BacklogItemLinkTypeModel(title, titleOpposite, account.ID)
            {
                CreatedByID = createdBy.ID,
            };
            
            return _Repository.Create(backlogItemLinkType);
        }

        public BacklogItemLinkTypeModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
