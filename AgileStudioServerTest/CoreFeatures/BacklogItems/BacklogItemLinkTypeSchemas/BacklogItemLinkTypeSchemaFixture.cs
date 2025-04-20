
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaFixture : AbstractEntityFixture<BacklogItemLinkTypeSchemaRepository>
    {
        private readonly UserFixture _userFixture;

        public BacklogItemLinkTypeSchemaFixture(
            BacklogItemLinkTypeSchemaRepository backlogItemLinkTypeSchemaRepository, 
            UserFixture userFixture) : base(backlogItemLinkTypeSchemaRepository)
        {
            _userFixture = userFixture;
        }

        public BacklogItemLinkTypeSchemaModel Create(
            string? title = null,
            UserModel? createdBy = null)
        {
            title ??= "Test BacklogItemLinkTypeSchema";
            createdBy ??= _userFixture.Create();

            var backlogItemLinkTypeSchema = new BacklogItemLinkTypeSchemaModel(title)
            {
                CreatedByID = createdBy.ID,
            };
            _Repository.Create(backlogItemLinkTypeSchema);
            return backlogItemLinkTypeSchema;
        }
    }
}
