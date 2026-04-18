
using AgileStudioServer.CoreFeatures.Accounts.Workflows;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.Accounts.Workflows
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
