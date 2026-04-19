using AgileStudioServer.Features.Accounts.BacklogItemLinkTypes;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeFixture : AbstractEntityFixture<BacklogItemLinkTypeRepository>
    {
        private readonly UserFixture _userFixture;

        public BacklogItemLinkTypeFixture(
            BacklogItemLinkTypeRepository backlogItemLinkTypeRepository, 
            UserFixture userFixture) : base(backlogItemLinkTypeRepository)
        {
            _userFixture = userFixture;
        }

        public BacklogItemLinkTypeModel Create(
            string? title = null,
            string? titleOpposite = null,
            UserModel? createdBy = null)
        {
            title ??= "Test BacklogItemLinkType";
            titleOpposite ??= "Test BacklogItemLinkTypeOpposite";
            createdBy ??= _userFixture.Create();

            var backlogItemLinkType = new BacklogItemLinkTypeModel(title, titleOpposite)
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
