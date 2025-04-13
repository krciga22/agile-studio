
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.CoreFeatures.Workflows.Workflows;
using AgileStudioServer.Data;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServerTest.CoreFeatures.Users.Users;
using AgileStudioServerTest.CoreFeatures.Workflows.Workflows;

namespace AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypes
{
    public class BacklogItemTypeFixture : AbstractEntityFixture
    {
        private readonly UserFixture _userFixture;

        private readonly BacklogItemTypeSchemaFixture _backlogItemTypeSchemaFixture;

        private readonly WorkflowFixture _workflowFixture;

        public BacklogItemTypeFixture(
            DBContext dbContext, 
            UserFixture userFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            WorkflowFixture workflowFixture) : base(dbContext)
        {
            _userFixture = userFixture;
            _backlogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _workflowFixture = workflowFixture;
        }

        public BacklogItemType Create(
            string? title = null,
            User? createdBy = null,
            BacklogItemTypeSchema? backlogItemTypeSchema = null,
            Workflow? workflow = null)
        {
            title ??= "Test BacklogItemType";
            createdBy ??= _userFixture.Create();
            backlogItemTypeSchema ??= _backlogItemTypeSchemaFixture.Create();
            workflow ??= _workflowFixture.Create();

            var backlogItemType = new BacklogItemType(title, backlogItemTypeSchema.ID, workflow.ID)
            {
                CreatedBy = createdBy,
            };
            _DBContext.BacklogItemType.Add(backlogItemType);
            _DBContext.SaveChanges();
            return backlogItemType;
        }
    }
}
