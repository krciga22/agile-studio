using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEntries;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemaEntries
{
    public class BacklogItemTypeSchemaEntryFixture : AbstractEntityFixture<BacklogItemTypeSchemaEntryRepository>
    {
        private readonly UserFixture _userFixture;

        private readonly BacklogItemTypeSchemaFixture _backlogItemTypeSchemaFixture;

        private readonly BacklogItemTypeFixture _backlogItemTypeFixture;

        public BacklogItemTypeSchemaEntryFixture(
            BacklogItemTypeSchemaEntryRepository backlogItemTypeSchemaEntryRepository, 
            UserFixture userFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemTypeFixture backlogItemTypeFixture) : base(backlogItemTypeSchemaEntryRepository)
        {
            _userFixture = userFixture;
            _backlogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _backlogItemTypeFixture = backlogItemTypeFixture;
        }

        public BacklogItemTypeSchemaEntryModel Create(
            BacklogItemTypeModel? parentType = null,
            BacklogItemTypeModel? childType = null,
            BacklogItemTypeSchemaModel? schema = null,
            UserModel? createdBy = null)
        {
            schema ??= _backlogItemTypeSchemaFixture.Create();
            parentType ??= _backlogItemTypeFixture.Create("Story", backlogItemTypeSchema: schema);
            childType ??= _backlogItemTypeFixture.Create("Task", backlogItemTypeSchema: schema);
            createdBy ??= _userFixture.Create();

            var backlogItemTypeSchemaEntry = new BacklogItemTypeSchemaEntryModel(childType.ID, parentType.ID, schema.ID)
            {
                CreatedByID = createdBy.ID
            };
            
            return _Repository.Create(backlogItemTypeSchemaEntry);
        }

        public BacklogItemTypeSchemaEntryModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
