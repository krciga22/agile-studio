
using AgileStudioServer.CoreFeatures.Projects.Projects;
using AgileStudioServer.CoreFeatures.Sprints.Sprints;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.Data;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Projects.Projects;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.Sprints.Sprints
{
    public class SprintFixture : AbstractEntityFixture
    {
        private readonly ProjectFixture _projectFixture;

        private readonly UserFixture _userFixture;

        public SprintFixture(
            DBContext dbContext,
            ProjectFixture projectFixture,
            UserFixture userFixture) : base(dbContext)
        {
            _projectFixture = projectFixture;
            _userFixture = userFixture;
        }

        public Sprint Create(
            int? sprintNumber = null,
            Project? project = null,
            User? createdBy = null)
        {
            int nextSprintNumber = sprintNumber ?? 1;
            project ??= _projectFixture.Create();
            createdBy ??= _userFixture.Create();

            var sprint = new Sprint(nextSprintNumber, project.ID)
            {
                CreatedBy = createdBy
            };
            _DBContext.Sprint.Add(sprint);
            _DBContext.SaveChanges();
            return sprint;
        }
    }
}
