
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.Data;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemLinkTypes;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryFixture : AbstractEntityFixture
    {
        private readonly BacklogItemLinkTypeSchemaFixture _backlogItemLinkTypeSchemaFixture;
        private readonly BacklogItemLinkTypeFixture _backlogItemLinkTypeFixture;
        private readonly UserFixture _userFixture;

        public BacklogItemLinkTypeSchemaEntryFixture(
            DBContext dbContext,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture,
            BacklogItemLinkTypeFixture backlogItemLinkTypeFixture,
            UserFixture userFixture) : base(dbContext)
        {
            _backlogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
            _backlogItemLinkTypeFixture = backlogItemLinkTypeFixture;
            _userFixture = userFixture;
        }

        public BacklogItemLinkTypeSchemaEntry Create(
            BacklogItemLinkTypeSchema? backlogItemLinkTypeSchema = null,
            BacklogItemLinkType? backlogItemLinkType = null,
            User? createdBy = null)
        {
            backlogItemLinkTypeSchema ??= _backlogItemLinkTypeSchemaFixture.Create();
            backlogItemLinkType ??= _backlogItemLinkTypeFixture.Create();
            createdBy ??= _userFixture.Create();

            var backlogItemLinkTypeSchemaEntry = new BacklogItemLinkTypeSchemaEntry(
                backlogItemLinkTypeSchema.ID,
                backlogItemLinkType.ID)
            {
                CreatedBy = createdBy,
            };
            _DBContext.BacklogItemLinkTypeSchemaEntry.Add(backlogItemLinkTypeSchemaEntry);
            _DBContext.SaveChanges();
            return backlogItemLinkTypeSchemaEntry;
        }
    }
}
