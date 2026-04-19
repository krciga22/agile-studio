using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypeSchemas
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
            
            return _Repository.Create(backlogItemLinkTypeSchema);
        }

        public BacklogItemLinkTypeSchemaModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
