
using AgileStudioServer.CoreFeatures.Projects.Projects;
using AgileStudioServer.CoreFeatures.Releases.Releases;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.Data;
using AgileStudioServerTest.Core.Fixtures;
using AgileStudioServerTest.CoreFeatures.Projects.Projects;
using AgileStudioServerTest.CoreFeatures.Users.Users;

namespace AgileStudioServerTest.CoreFeatures.Releases.Releases
{
    public class ReleaseFixture : AbstractEntityFixture
    {
        private readonly ProjectFixture _projectFixture;

        private readonly UserFixture _userFixture;

        public ReleaseFixture(
            DBContext dbContext, 
            ProjectFixture projectFixture,
            UserFixture userFixture) : base(dbContext)
        {
            _projectFixture = projectFixture;
            _userFixture = userFixture;
        }

        public Release Create(
            string? title = null,
            Project? project = null,
            User? createdBy = null)
        {
            title ??= "v1.0.0";
            project ??= _projectFixture.Create();
            createdBy ??= _userFixture.Create();

            var release = new Release(title, project.ID)
            {
                CreatedBy = createdBy
            };
            _DBContext.Release.Add(release);
            _DBContext.SaveChanges();
            return release;
        }
    }
}
