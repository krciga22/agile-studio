using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Data;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Accounts.Accounts;

namespace AgileStudioServerTest.IntegrationTests.Features.Accounts.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemTypeSchemaController _Controller;

        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;

        private readonly BacklogItemTypeFixture _BacklogItemTypeFixture;
        private readonly AccountFixture _AccountFixture;

        public BacklogItemTypeSchemaControllerTest(
            DBContext dbContext,
            BacklogItemTypeSchemaController controller,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemTypeFixture backlogItemTypeFixture,
            AccountFixture accountFixture) : base(dbContext)
        {
            _Controller = controller;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _BacklogItemTypeFixture = backlogItemTypeFixture;
            _AccountFixture = accountFixture;
        }

        // todo add tests
    }
}
