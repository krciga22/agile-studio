
using AgileStudioServer.CoreFeatures.Projects.Projects;
using AgileStudioServer.CoreFeatures.Sprints.Sprints;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Projects.Projects;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.Sprints.Sprints
{
    public class SprintFixture : AbstractEntityFixture<SprintRepository>
    {
        private readonly ProjectFixture _projectFixture;

        private readonly UserFixture _userFixture;

        public SprintFixture(
            SprintRepository sprintRepository,
            ProjectFixture projectFixture,
            UserFixture userFixture) : base(sprintRepository)
        {
            _projectFixture = projectFixture;
            _userFixture = userFixture;
        }

        public SprintModel Create(
            int? sprintNumber = null,
            ProjectModel? project = null,
            UserModel? createdBy = null)
        {
            int nextSprintNumber = sprintNumber ?? 1;
            project ??= _projectFixture.Create();
            createdBy ??= _userFixture.Create();

            var sprint = new SprintModel(nextSprintNumber, project.ID)
            {
                CreatedByID = createdBy.ID
            };
            
            return _Repository.Create(sprint);
        }

        public SprintModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
