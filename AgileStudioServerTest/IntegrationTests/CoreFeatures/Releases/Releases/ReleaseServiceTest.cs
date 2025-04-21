using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.Releases.Releases;
using AgileStudioServer.CoreFeatures.Projects.Projects;
using AgileStudioServerTest.CoreFeatures.Releases.Releases;
using AgileStudioServerTest.CoreFeatures.Projects.Projects;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.Releases.Releases
{
    public class ReleaseServiceTest : AbstractServiceTest
    {
        private readonly ReleaseService _releaseService;

        private readonly ReleaseFixture _ReleaseFixture;

        private readonly ProjectFixture _ProjectFixture;

        public ReleaseServiceTest(
            DBContext dbContext,
            ReleaseService releaseService,
            ReleaseFixture releaseFixture,
            ProjectFixture projectFixture) : base(dbContext)
        {
            _releaseService = releaseService;
            _ReleaseFixture = releaseFixture;
            _ProjectFixture = projectFixture;
        }

        [Fact]
        public void Create_ReturnsRelease()
        {
            ProjectModel project = _ProjectFixture.Create();
            ReleaseModel release = new("Test Release", project.ID);

            release = _releaseService.Create(release);

            Assert.NotNull(release);
            Assert.True(release.ID > 0);
        }

        [Fact]
        public void Get_ReturnsRelease()
        {
            var release = _ReleaseFixture.Create();

            var returnedRelease = _releaseService.Get(release.ID);

            Assert.NotNull(returnedRelease);
            Assert.Equal(release.ID, returnedRelease.ID);
        }

        [Fact]
        public void GetAll_ReturnsAllReleases()
        {
            var project = _ProjectFixture.Create();
            var releases = new List<ReleaseModel>
            {
                _ReleaseFixture.Create("Test Release 1", project),
                _ReleaseFixture.Create("Test Release 2", project)
            };

            List<ReleaseModel> returnedReleases = _releaseService.GetByProjectId(project.ID);

            Assert.Equal(releases.Count, returnedReleases.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedRelease()
        {
            var release = _ReleaseFixture.Create();
            var title = $"{release.Title} Updated";

            release.Title = title;
            release = _releaseService.Update(release);

            Assert.NotNull(release);
            Assert.Equal(title, release.Title);
        }

        [Fact]
        public void Delete_DeletesRelease()
        {
            var release = _ReleaseFixture.Create();

            _releaseService.Delete(release);

            release = _releaseService.Get(release.ID);
            Assert.Null(release);
        }
    }
}
