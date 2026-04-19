using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Accounts.Workflows;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Accounts.Workflows;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Accounts.BacklogItemTypes
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
