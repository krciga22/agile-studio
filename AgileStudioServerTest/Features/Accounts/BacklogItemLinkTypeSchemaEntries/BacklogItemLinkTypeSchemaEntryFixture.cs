using AgileStudioServer.Features.Accounts.BacklogItemLinkTypes;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryFixture : AbstractEntityFixture<BacklogItemLinkTypeSchemaEntryRepository>
    {
        private readonly BacklogItemLinkTypeSchemaFixture _backlogItemLinkTypeSchemaFixture;
        private readonly BacklogItemLinkTypeFixture _backlogItemLinkTypeFixture;
        private readonly UserFixture _userFixture;

        public BacklogItemLinkTypeSchemaEntryFixture(
            BacklogItemLinkTypeSchemaEntryRepository backlogItemLinkTypeSchemaEntryRepository,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture,
            BacklogItemLinkTypeFixture backlogItemLinkTypeFixture,
            UserFixture userFixture) : base(backlogItemLinkTypeSchemaEntryRepository)
        {
            _backlogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
            _backlogItemLinkTypeFixture = backlogItemLinkTypeFixture;
            _userFixture = userFixture;
        }

        public BacklogItemLinkTypeSchemaEntryModel Create(
            BacklogItemLinkTypeSchemaModel? backlogItemLinkTypeSchema = null,
            BacklogItemLinkTypeModel? backlogItemLinkType = null,
            UserModel? createdBy = null)
        {
            backlogItemLinkTypeSchema ??= _backlogItemLinkTypeSchemaFixture.Create();
            backlogItemLinkType ??= _backlogItemLinkTypeFixture.Create();
            createdBy ??= _userFixture.Create();

            var backlogItemLinkTypeSchemaEntry = new BacklogItemLinkTypeSchemaEntryModel(
                backlogItemLinkTypeSchema.ID,
                backlogItemLinkType.ID)
            {
                CreatedByID = createdBy.ID,
            };
            
            return _Repository.Create(backlogItemLinkTypeSchemaEntry);
        }

        public BacklogItemLinkTypeSchemaEntryModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
