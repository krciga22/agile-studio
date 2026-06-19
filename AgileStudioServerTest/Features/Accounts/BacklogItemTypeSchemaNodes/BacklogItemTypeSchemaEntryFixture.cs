using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Users.Users;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemaEdges;

namespace AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemaNodes
{
    public class BacklogItemTypeSchemaNodeFixture : AbstractEntityFixture<BacklogItemTypeSchemaNodeRepository>
    {
        private readonly UserFixture _userFixture;

        private readonly BacklogItemTypeSchemaFixture _backlogItemTypeSchemaFixture;

        private readonly BacklogItemTypeFixture _backlogItemTypeFixture;

        private readonly BacklogItemTypeSchemaEdgeFixture _backlogItemTypeSchemaEdgeFixture;

        public BacklogItemTypeSchemaNodeFixture(
            BacklogItemTypeSchemaNodeRepository backlogItemTypeSchemaNodeRepository,
            UserFixture userFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemTypeFixture backlogItemTypeFixture,
            BacklogItemTypeSchemaEdgeFixture backlogItemTypeSchemaEdgeFixture) : base(backlogItemTypeSchemaNodeRepository)
        {
            _userFixture = userFixture;
            _backlogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _backlogItemTypeFixture = backlogItemTypeFixture;
            _backlogItemTypeSchemaEdgeFixture = backlogItemTypeSchemaEdgeFixture;
        }

        public BacklogItemTypeSchemaNodeModel Create(
            BacklogItemTypeSchemaModel? schema = null,
            BacklogItemTypeModel? backlogItemType = null,
            UserModel? createdBy = null,
            List<BacklogItemTypeModel>? fromEdges = null,
            List<BacklogItemTypeModel>? toEdges = null)
        {
            schema ??= _backlogItemTypeSchemaFixture.Create();
            backlogItemType ??= _backlogItemTypeFixture.Create("Story");
            createdBy ??= _userFixture.Create();

            var backlogItemTypeSchemaNode = new BacklogItemTypeSchemaNodeModel(schema.ID, backlogItemType.ID)
            {
                CreatedByID = createdBy.ID
            };
            
            var model = _Repository.Create(backlogItemTypeSchemaNode);

            if (fromEdges != null && fromEdges.Count > 0)
            {
                fromEdges.ForEach(fromBacklogItemType => {
                    _backlogItemTypeSchemaEdgeFixture.Create(
                        schema: schema,
                        fromType: fromBacklogItemType,
                        toType: backlogItemType,
                        createdBy: createdBy
                    );
                });
            }

            if (toEdges != null && toEdges.Count > 0)
            {
                toEdges.ForEach(toBacklogItemType => {
                    _backlogItemTypeSchemaEdgeFixture.Create(
                        schema: schema,
                        fromType: backlogItemType,
                        toType: toBacklogItemType,
                        createdBy: createdBy
                    );
                });
            }

            return model;
        }

        public BacklogItemTypeSchemaNodeModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
