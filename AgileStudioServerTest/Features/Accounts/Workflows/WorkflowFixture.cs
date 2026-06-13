using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.Workflows;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Accounts.Accounts;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Accounts.Workflows
{
    public class WorkflowFixture : AbstractEntityFixture<WorkflowRepository>
    {
        private readonly UserFixture _userFixture;

        private readonly AccountFixture _AccountFixture;

        public WorkflowFixture(
            WorkflowRepository workflowRepository, 
            UserFixture userFixture,
            AccountFixture accountFixture) : base(workflowRepository)
        {
            _userFixture = userFixture;
            _AccountFixture = accountFixture;
        }

        public WorkflowModel Create(
            string? title = null,
            UserModel? createdBy = null,
            AccountModel? account = null)
        {
            title ??= "Test Workflow";
            createdBy ??= _userFixture.Create();
            account ??= _AccountFixture.Create(createdBy: createdBy);

            var workflow = new WorkflowModel(title, account.ID)
            {
                CreatedById = createdBy.ID
            };
            
            return _Repository.Create(workflow);
        }

        public WorkflowModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
