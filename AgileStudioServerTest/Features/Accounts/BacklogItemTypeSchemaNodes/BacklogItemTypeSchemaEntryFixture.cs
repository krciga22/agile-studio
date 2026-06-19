using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Users.Users;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes;

namespace AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemaNodes
{
    public class BacklogItemTypeSchemaNodeFixture : AbstractEntityFixture<BacklogItemTypeSchemaNodeRepository>
    {
        private readonly UserFixture _userFixture;

        private readonly BacklogItemTypeSchemaFixture _backlogItemTypeSchemaFixture;

        private readonly BacklogItemTypeFixture _backlogItemTypeFixture;

        public BacklogItemTypeSchemaNodeFixture(
            BacklogItemTypeSchemaNodeRepository backlogItemTypeSchemaNodeRepository, 
            UserFixture userFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemTypeFixture backlogItemTypeFixture) : base(backlogItemTypeSchemaNodeRepository)
        {
            _userFixture = userFixture;
            _backlogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _backlogItemTypeFixture = backlogItemTypeFixture;
        }

        public BacklogItemTypeSchemaNodeModel Create(
            BacklogItemTypeSchemaModel? schema = null,
            BacklogItemTypeModel? backlogItemType = null,
            UserModel? createdBy = null)
        {
            schema ??= _backlogItemTypeSchemaFixture.Create();
            backlogItemType ??= _backlogItemTypeFixture.Create("Story");
            createdBy ??= _userFixture.Create();

            var backlogItemTypeSchemaNode = new BacklogItemTypeSchemaNodeModel(schema.ID, backlogItemType.ID)
            {
                CreatedByID = createdBy.ID
            };
            
            return _Repository.Create(backlogItemTypeSchemaNode);
        }

        public BacklogItemTypeSchemaNodeModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
