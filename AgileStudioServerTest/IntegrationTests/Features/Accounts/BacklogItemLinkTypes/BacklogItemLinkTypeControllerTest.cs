using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypes;
using AgileStudioServer.Data;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypes;
using AgileStudioServerTest.Features.Accounts.Accounts;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemLinkTypeController _Controller;

        private readonly BacklogItemLinkTypeFixture _BacklogItemLinkTypeFixture;

        private readonly AccountFixture _AccountFixture;

        public BacklogItemLinkTypeControllerTest(
            DBContext dbContext,
            BacklogItemLinkTypeController controller,
            BacklogItemLinkTypeFixture backlogItemLinkTypeFixture,
            AccountFixture accountFixture) : base(dbContext)
        {
            _Controller = controller;
            _BacklogItemLinkTypeFixture = backlogItemLinkTypeFixture;
            _AccountFixture = accountFixture;
        }

        // todo add tests
    }
}
