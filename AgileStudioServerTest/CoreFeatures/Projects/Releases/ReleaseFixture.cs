
using AgileStudioServer.CoreFeatures.Projects.Projects;
using AgileStudioServer.CoreFeatures.Projects.Releases;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Projects.Projects;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.Projects.Releases
{
    public class ReleaseFixture : AbstractEntityFixture<ReleaseRepository>
    {
        private readonly ProjectFixture _projectFixture;

        private readonly UserFixture _userFixture;

        public ReleaseFixture(
            ReleaseRepository releaseRepository, 
            ProjectFixture projectFixture,
            UserFixture userFixture) : base(releaseRepository)
        {
            _projectFixture = projectFixture;
            _userFixture = userFixture;
        }

        public ReleaseModel Create(
            string? title = null,
            ProjectModel? project = null,
            UserModel? createdBy = null)
        {
            title ??= "v1.0.0";
            project ??= _projectFixture.Create();
            createdBy ??= _userFixture.Create();

            var release = new ReleaseModel(title, project.ID)
            {
                CreatedByID = createdBy.ID
            };
            
            return _Repository.Create(release);
        }

        public ReleaseModel? Get(int id)
        {
            return _Repository.Get(id);
        }
    }
}
