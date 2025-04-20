
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.CoreFeatures.Workflows.Workflows;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServerTest.CoreFeatures.Users.Users;
using AgileStudioServerTest.CoreFeatures.Workflows.Workflows;

namespace AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypes
{
    public class BacklogItemTypeFixture : AbstractEntityFixture<BacklogItemTypeRepository>
    {
        private readonly UserFixture _userFixture;

        private readonly BacklogItemTypeSchemaFixture _backlogItemTypeSchemaFixture;

        private readonly WorkflowFixture _workflowFixture;

        public BacklogItemTypeFixture(
            BacklogItemTypeRepository backlogItemTypeRepository, 
            UserFixture userFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            WorkflowFixture workflowFixture) : base(backlogItemTypeRepository)
        {
            _userFixture = userFixture;
            _backlogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _workflowFixture = workflowFixture;
        }

        public BacklogItemTypeModel Create(
            string? title = null,
            UserModel? createdBy = null,
            BacklogItemTypeSchemaModel? backlogItemTypeSchema = null,
            WorkflowModel? workflow = null)
        {
            title ??= "Test BacklogItemType";
            createdBy ??= _userFixture.Create();
            backlogItemTypeSchema ??= _backlogItemTypeSchemaFixture.Create();
            workflow ??= _workflowFixture.Create();

            var backlogItemType = new BacklogItemTypeModel(title, backlogItemTypeSchema.ID, workflow.ID)
            {
                CreatedByID = createdBy.ID,
            };
            
            return _Repository.Create(backlogItemType);
        }
    }
}
