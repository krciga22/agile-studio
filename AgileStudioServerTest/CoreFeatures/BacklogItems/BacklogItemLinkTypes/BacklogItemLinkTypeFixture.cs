
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.Data;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeFixture : AbstractEntityFixture
    {
        private readonly UserFixture _userFixture;

        public BacklogItemLinkTypeFixture(DBContext dbContext, UserFixture userFixture) : base(dbContext)
        {
            _userFixture = userFixture;
        }

        public BacklogItemLinkType Create(
            string? title = null,
            string? titleOpposite = null,
            User? createdBy = null)
        {
            title ??= "Test BacklogItemLinkType";
            titleOpposite ??= "Test BacklogItemLinkTypeOpposite";
            createdBy ??= _userFixture.Create();

            var backlogItemLinkType = new BacklogItemLinkType(title, titleOpposite)
            {
                CreatedBy = createdBy,
            };
            _DBContext.BacklogItemLinkType.Add(backlogItemLinkType);
            _DBContext.SaveChanges();
            return backlogItemLinkType;
        }
    }
}
