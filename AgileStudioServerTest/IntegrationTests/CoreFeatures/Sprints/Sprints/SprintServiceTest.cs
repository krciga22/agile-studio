using AgileStudioServer.Data;
using AgileStudioServer.CoreFeatures.Projects.Services.Models;
using AgileStudioServer.CoreFeatures.Sprints.Sprints;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.Sprints.Sprints
{
    public class SprintServiceTest : AbstractServiceTest
    {
        private readonly SprintService _sprintService;

        public SprintServiceTest(
            DBContext dbContext,
            ModelFixtures fixtures,
            SprintService sprintService) : base(dbContext, fixtures)
        {
            _sprintService = sprintService;
        }

        [Fact]
        public void Create_ReturnsSprint()
        {
            int nextSprintNumber = _sprintService.GetNextSprintNumber();
            ProjectModel project = _Fixtures.CreateProject();
            SprintModel sprint = new(nextSprintNumber, project.ID);

            sprint = _sprintService.Create(sprint);

            Assert.NotNull(sprint);
            Assert.True(sprint.ID > 0);
        }

        [Fact]
        public void Get_ReturnsSprint()
        {
            var sprint = _Fixtures.CreateSprint();

            var returnedSprint = _sprintService.Get(sprint.ID);

            Assert.NotNull(returnedSprint);
            Assert.Equal(sprint.ID, returnedSprint.ID);
        }

        [Fact]
        public void GetAll_ReturnsAllSprints()
        {
            var project = _Fixtures.CreateProject();
            var nextSprintNumber = _sprintService.GetNextSprintNumber();
            var sprints = new List<SprintModel>
            {
                _Fixtures.CreateSprint(nextSprintNumber + 1, project),
                _Fixtures.CreateSprint(nextSprintNumber + 2,project)
            };

            List<SprintModel> returnedSprints = _sprintService.GetByProjectId(project.ID);

            Assert.Equal(sprints.Count, returnedSprints.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedSprint()
        {
            var sprint = _Fixtures.CreateSprint();
            var nextSprintNumber = _sprintService.GetNextSprintNumber();

            sprint.SprintNumber = nextSprintNumber;
            sprint = _sprintService.Update(sprint);

            Assert.NotNull(sprint);
            Assert.Equal(nextSprintNumber, sprint.SprintNumber);
        }

        [Fact]
        public void Delete_DeletesSprint()
        {
            var sprint = _Fixtures.CreateSprint();

            _sprintService.Delete(sprint);

            sprint = _sprintService.Get(sprint.ID);
            Assert.Null(sprint);
        }
    }
}
