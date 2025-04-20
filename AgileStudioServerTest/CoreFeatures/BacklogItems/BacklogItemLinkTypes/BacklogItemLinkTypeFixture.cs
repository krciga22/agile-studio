
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemLinkTypes
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
            _Repository.Create(backlogItemLinkType);
            return backlogItemLinkType;
        }
    }
}
