using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Users.Users;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges;

namespace AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemaEdges
{
    public class BacklogItemTypeSchemaEdgeFixture : AbstractEntityFixture<BacklogItemTypeSchemaEdgeRepository>
    {
        private readonly UserFixture _userFixture;

        private readonly BacklogItemTypeSchemaFixture _backlogItemTypeSchemaFixture;

        private readonly BacklogItemTypeFixture _backlogItemTypeFixture;

        public BacklogItemTypeSchemaEdgeFixture(
            BacklogItemTypeSchemaEdgeRepository backlogItemTypeSchemaEdgeRepository, 
            UserFixture userFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemTypeFixture backlogItemTypeFixture) : base(backlogItemTypeSchemaEdgeRepository)
        {
            _userFixture = userFixture;
            _backlogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _backlogItemTypeFixture = backlogItemTypeFixture;
        }

        public BacklogItemTypeSchemaEdgeModel Create(
            BacklogItemTypeSchemaModel? schema = null,
            BacklogItemTypeModel? fromType = null,
            BacklogItemTypeModel? toType = null,
            UserModel? createdBy = null)
        {
            schema ??= _backlogItemTypeSchemaFixture.Create();
            fromType ??= _backlogItemTypeFixture.Create("Story");
            toType ??= _backlogItemTypeFixture.Create("Task");
            createdBy ??= _userFixture.Create();

            var backlogItemTypeSchemaEdge = new BacklogItemTypeSchemaEdgeModel(schema.ID, fromType.ID, toType.ID)
            {
                CreatedByID = createdBy.ID
            };
            
            return _Repository.Create(backlogItemTypeSchemaEdge);
        }

        public BacklogItemTypeSchemaEdgeModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
