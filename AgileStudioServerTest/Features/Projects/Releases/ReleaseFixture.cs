using AgileStudioServer.Features.Projects.Projects;
using AgileStudioServer.Features.Projects.Releases;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.Features.Projects.Projects;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.Features.Projects.Releases
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
