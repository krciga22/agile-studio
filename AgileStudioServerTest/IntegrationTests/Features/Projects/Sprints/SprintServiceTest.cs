using AgileStudioServer.Data;
using AgileStudioServer.Features.Projects.Projects;
using AgileStudioServer.Features.Projects.Sprints;
using AgileStudioServerTest.Features.Projects.Projects;
using AgileStudioServerTest.Features.Projects.Sprints;

namespace AgileStudioServerTest.IntegrationTests.Features.Projects.Sprints
{
    public class SprintServiceTest : AbstractServiceTest
    {
        private readonly SprintService _sprintService;

        private readonly SprintFixture _SprintFixture;

        private readonly ProjectFixture _ProjectFixture;

        public SprintServiceTest(
            DBContext dbContext,
            SprintService sprintService,
            SprintFixture sprintFixture,
            ProjectFixture projectFixture) : base(dbContext)
        {
            _sprintService = sprintService;
            _SprintFixture = sprintFixture;
            _ProjectFixture = projectFixture;
        }

        [Fact]
        public void Create_ReturnsSprint()
        {
            int nextSprintNumber = _sprintService.GetNextSprintNumber();
            ProjectModel project = _ProjectFixture.Create();
            SprintModel sprint = new(nextSprintNumber, project.ID);

            sprint = _sprintService.Create(sprint);

            Assert.NotNull(sprint);
            Assert.True(sprint.ID > 0);
        }

        [Fact]
        public void Get_ReturnsSprint()
        {
            var sprint = _SprintFixture.Create();

            var returnedSprint = _sprintService.Get(sprint.ID);

            Assert.NotNull(returnedSprint);
            Assert.Equal(sprint.ID, returnedSprint.ID);
        }

        [Fact]
        public void GetAll_ReturnsAllSprints()
        {
            var project = _ProjectFixture.Create();
            var nextSprintNumber = _sprintService.GetNextSprintNumber();
            var sprints = new List<SprintModel>
            {
                _SprintFixture.Create(nextSprintNumber + 1, project),
                _SprintFixture.Create(nextSprintNumber + 2,project)
            };

            List<SprintModel> returnedSprints = _sprintService.GetByProjectId(project.ID);

            Assert.Equal(sprints.Count, returnedSprints.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedSprint()
        {
            var sprint = _SprintFixture.Create();
            var nextSprintNumber = _sprintService.GetNextSprintNumber();

            sprint.SprintNumber = nextSprintNumber;
            sprint = _sprintService.Update(sprint);

            Assert.NotNull(sprint);
            Assert.Equal(nextSprintNumber, sprint.SprintNumber);
        }

        [Fact]
        public void Delete_DeletesSprint()
        {
            var sprint = _SprintFixture.Create();

            _sprintService.Delete(sprint);

            sprint = _sprintService.Get(sprint.ID);
            Assert.Null(sprint);
        }
    }
}
