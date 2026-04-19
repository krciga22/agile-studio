using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Accounts.ChildBacklogItemTypes;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Accounts.ChildBacklogItemTypes
{
    public class ChildBacklogItemTypeFixture : AbstractEntityFixture<ChildBacklogItemTypeRepository>
    {
        private readonly UserFixture _userFixture;

        private readonly BacklogItemTypeSchemaFixture _backlogItemTypeSchemaFixture;

        private readonly BacklogItemTypeFixture _backlogItemTypeFixture;

        public ChildBacklogItemTypeFixture(
            ChildBacklogItemTypeRepository childBacklogItemTypeRepository, 
            UserFixture userFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemTypeFixture backlogItemTypeFixture) : base(childBacklogItemTypeRepository)
        {
            _userFixture = userFixture;
            _backlogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _backlogItemTypeFixture = backlogItemTypeFixture;
        }

        public ChildBacklogItemTypeModel Create(
            BacklogItemTypeModel? parentType = null,
            BacklogItemTypeModel? childType = null,
            BacklogItemTypeSchemaModel? schema = null,
            UserModel? createdBy = null)
        {
            schema ??= _backlogItemTypeSchemaFixture.Create();
            parentType ??= _backlogItemTypeFixture.Create("Story", backlogItemTypeSchema: schema);
            childType ??= _backlogItemTypeFixture.Create("Task", backlogItemTypeSchema: schema);
            createdBy ??= _userFixture.Create();

            var childBacklogItemType = new ChildBacklogItemTypeModel(childType.ID, parentType.ID, schema.ID)
            {
                CreatedByID = createdBy.ID
            };
            
            return _Repository.Create(childBacklogItemType);
        }

        public ChildBacklogItemTypeModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
