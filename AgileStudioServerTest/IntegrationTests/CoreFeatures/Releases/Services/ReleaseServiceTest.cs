using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.Releases.Services;
using AgileStudioServer.CoreFeatures.Releases.Services.Models;
using AgileStudioServer.CoreFeatures.Projects.Services.Models;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.Releases.Services
{
    public class ReleaseServiceTest : AbstractServiceTest
    {
        private readonly ReleaseService _releaseService;

        public ReleaseServiceTest(
            DBContext dbContext,
            ModelFixtures fixtures,
            ReleaseService releaseService) : base(dbContext, fixtures)
        {
            _releaseService = releaseService;
        }

        [Fact]
        public void Create_ReturnsRelease()
        {
            Project project = _Fixtures.CreateProject();
            ReleaseModel release = new("Test Release", project.ID);

            release = _releaseService.Create(release);

            Assert.NotNull(release);
            Assert.True(release.ID > 0);
        }

        [Fact]
        public void Get_ReturnsRelease()
        {
            var release = _Fixtures.CreateRelease();

            var returnedRelease = _releaseService.Get(release.ID);

            Assert.NotNull(returnedRelease);
            Assert.Equal(release.ID, returnedRelease.ID);
        }

        [Fact]
        public void GetAll_ReturnsAllReleases()
        {
            var project = _Fixtures.CreateProject();
            var releases = new List<ReleaseModel>
            {
                _Fixtures.CreateRelease("Test Release 1", project),
                _Fixtures.CreateRelease("Test Release 2", project)
            };

            List<ReleaseModel> returnedReleases = _releaseService.GetByProjectId(project.ID);

            Assert.Equal(releases.Count, returnedReleases.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedRelease()
        {
            var release = _Fixtures.CreateRelease();
            var title = $"{release.Title} Updated";

            release.Title = title;
            release = _releaseService.Update(release);

            Assert.NotNull(release);
            Assert.Equal(title, release.Title);
        }

        [Fact]
        public void Delete_DeletesRelease()
        {
            var release = _Fixtures.CreateRelease();

            _releaseService.Delete(release);

            release = _releaseService.Get(release.ID);
            Assert.Null(release);
        }
    }
}
