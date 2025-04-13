
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.CoreFeatures.Workflows.Workflows;
using AgileStudioServer.Data;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.Workflows.Workflows
{
    public class WorkflowFixture : AbstractEntityFixture
    {
        private readonly UserFixture _userFixture;

        public WorkflowFixture(DBContext dbContext, UserFixture userFixture) : base(dbContext)
        {
            _userFixture = userFixture;
        }

        public Workflow Create(
            string? title = null,
            User? createdBy = null)
        {
            title ??= "Test Workflow";
            createdBy ??= _userFixture.Create();

            var workflow = new Workflow(title)
            {
                CreatedBy = createdBy
            };
            _DBContext.Workflow.Add(workflow);
            _DBContext.SaveChanges();
            return workflow;
        }
    }
}
