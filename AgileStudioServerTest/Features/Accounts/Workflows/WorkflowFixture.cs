using AgileStudioServer.Features.Accounts.Workflows;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Accounts.Workflows
{
    public class WorkflowFixture : AbstractEntityFixture<WorkflowRepository>
    {
        private readonly UserFixture _userFixture;

        public WorkflowFixture(
            WorkflowRepository workflowRepository, 
            UserFixture userFixture) : base(workflowRepository)
        {
            _userFixture = userFixture;
        }

        public WorkflowModel Create(
            string? title = null,
            UserModel? createdBy = null)
        {
            title ??= "Test Workflow";
            createdBy ??= _userFixture.Create();

            var workflow = new WorkflowModel(title)
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
