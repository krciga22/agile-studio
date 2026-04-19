using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaFixture : AbstractEntityFixture<BacklogItemTypeSchemaRepository>
    {
        private readonly UserFixture _userFixture;

        public BacklogItemTypeSchemaFixture(
            BacklogItemTypeSchemaRepository backlogItemTypeSchemaRepository, 
            UserFixture userFixture) : base(backlogItemTypeSchemaRepository)
        {
            _userFixture = userFixture;
        }

        public BacklogItemTypeSchemaModel Create(
            string? title = null,
            UserModel? createdBy = null)
        {
            title ??= "Test BacklogItemTypeSchema";
            createdBy ??= _userFixture.Create();

            var backlogItemTypeSchema = new BacklogItemTypeSchemaModel(title)
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
