
using AgileStudioServer.CoreFeatures.Accounts.BacklogItemTypes;
using AgileStudioServer.CoreFeatures.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.Accounts.Workflows;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.CoreFeatures.Accounts.Workflows;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemTypes
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

        public BacklogItemTypeModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
