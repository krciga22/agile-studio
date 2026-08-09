using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypeSchemas;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemLinkTypeSchemaEntryController _Controller;

        private readonly BacklogItemLinkTypeFixture _BacklogItemLinkTypeFixture;

        private readonly BacklogItemLinkTypeSchemaFixture _BacklogItemLinkTypeSchemaFixture;

        private readonly BacklogItemLinkTypeSchemaEntryFixture _BacklogItemLinkTypeSchemaEntryFixture;

        public BacklogItemLinkTypeSchemaEntryControllerTest(
            DBContext dbContext,
            BacklogItemLinkTypeSchemaEntryController controller,
            BacklogItemLinkTypeFixture backlogItemLinkTypeFixture,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture,
            BacklogItemLinkTypeSchemaEntryFixture backlogItemLinkTypeSchemaEntryFixture) : base(dbContext)
        {
            _Controller = controller;
            _BacklogItemLinkTypeFixture = backlogItemLinkTypeFixture;
            _BacklogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
            _BacklogItemLinkTypeSchemaEntryFixture = backlogItemLinkTypeSchemaEntryFixture;
        }

        // todo add tests
    }
}
