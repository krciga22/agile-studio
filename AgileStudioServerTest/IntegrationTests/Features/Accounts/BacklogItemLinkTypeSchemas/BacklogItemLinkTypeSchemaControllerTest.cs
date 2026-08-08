using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServerTest.Features.Accounts.Accounts;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypeSchemas;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemLinkTypeSchemaController _Controller;

        private readonly BacklogItemLinkTypeSchemaFixture _BacklogItemLinkTypeSchemaFixture;

        private readonly AccountFixture _AccountFixture;

        public BacklogItemLinkTypeSchemaControllerTest(
            DBContext dbContext,
            BacklogItemLinkTypeSchemaController controller,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture,
            AccountFixture accountFixture) : base(dbContext)
        {
            _Controller = controller;
            _BacklogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
            _AccountFixture = accountFixture;
        }

        // todo add tests
    }
}
