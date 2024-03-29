﻿using AgileStudioServer;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Entities;
using AgileStudioServer.Services.DataProviders;

namespace AgileStudioServerTest.IntegrationTests.Services.DataProviders
{
    public class ReleaseDataProviderTest : AbstractDataProviderTest
    {
        private readonly ReleaseDataProvider _DataProvider;

        public ReleaseDataProviderTest(
            DBContext dbContext,
            Fixtures fixtures,
            ReleaseDataProvider releaseDataProvider) : base(dbContext, fixtures)
        {
            _DataProvider = releaseDataProvider;
        }

        [Fact]
        public void CreateRelease_WithReleasePostDto_ReturnsReleaseApiResource()
        {
            var project = _Fixtures.CreateProject();
            var releasePostDto = new ReleasePostDto("v1.0.0", project.ID);

            var releaseApiResource = _DataProvider.Create(releasePostDto);

            Assert.IsType<ReleaseApiResource>(releaseApiResource);
        }

        [Fact]
        public void GetRelease_ById_ReturnsReleaseApiResource()
        {
            var release = _Fixtures.CreateRelease();

            var releaseApiResource = _DataProvider.Get(release.ID);

            Assert.IsType<ReleaseApiResource>(releaseApiResource);
        }

        [Fact]
        public void GetReleases_ByProjectId_ReturnsReleaseApiResources()
        {
            var project = _Fixtures.CreateProject();

            var releases = new List<Release>
            {
                _Fixtures.CreateRelease("v1.0.0", project),
                _Fixtures.CreateRelease("v1.0.1", project)
            };

            List<ReleaseApiResource> releaseApiResources = _DataProvider.ListForProjectId(project.ID);

            Assert.Equal(releases.Count, releaseApiResources.Count);
        }

        [Fact]
        public void UpdateRelease_WithValidReleasePatchDto_ReturnsReleaseApiResource()
        {
            var release = _Fixtures.CreateRelease();
            var releasePatchDto = new ReleasePatchDto("v1.0.1");

            var releaseApiResource = _DataProvider.Update(release.ID, releasePatchDto);

            Assert.IsType<ReleaseApiResource>(releaseApiResource);
            Assert.Equal(releasePatchDto.Title, releaseApiResource.Title);
        }

        [Fact]
        public void DeleteRelease_WithValidId_ReturnsTrue()
        {
            var release = _Fixtures.CreateRelease();

            _DataProvider.Delete(release.ID);

            var result = _DataProvider.Get(release.ID);
            Assert.Null(result);
        }
    }
}
