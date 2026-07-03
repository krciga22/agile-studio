using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Data;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Accounts.Workflows;
using AgileStudioServerTest.Features.Accounts.Accounts;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.BacklogItemTypes
{
    public class BacklogItemTypeControllerTest : AbstractControllerTest
    {
        private const int NON_EXISTANT_ID = 1234567;

        private readonly WorkflowFixture _WorkflowFixture;

        private readonly BacklogItemTypeFixture _BacklogItemTypeFixture;

        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;

        private readonly BacklogItemTypeController _Controller;

        private readonly AccountFixture _AccountFixture;

        public BacklogItemTypeControllerTest(
            DBContext dbContext,
            BacklogItemTypeController controller,
            AccountFixture accountFixture,
            WorkflowFixture workflowFixture,
            BacklogItemTypeFixture backlogItemTypeFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture) : base(dbContext)
        {
            _Controller = controller;
            _AccountFixture = accountFixture;
            _BacklogItemTypeFixture = backlogItemTypeFixture;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _WorkflowFixture = workflowFixture;
        }

        // todo add tests
    }
}
