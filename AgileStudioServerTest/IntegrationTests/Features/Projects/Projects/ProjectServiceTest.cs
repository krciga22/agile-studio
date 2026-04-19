using AgileStudioServer.Data;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Projects.Projects;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Projects.Projects;

namespace AgileStudioServerTest.IntegrationTests.Features.Projects.Projects
{
    public class ProjectServiceTest : AbstractServiceTest
    {
        private readonly ProjectService _projectService;

        private readonly ProjectFixture _ProjectFixture;

        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;

        private readonly BacklogItemLinkTypeSchemaFixture _BacklogItemLinkTypeSchemaFixture;

        public ProjectServiceTest(
            DBContext dbContext,
            ProjectService projectService,
            ProjectFixture projectFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture) : base(dbContext)
        {
            _projectService = projectService;
            _ProjectFixture = projectFixture;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _BacklogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
        }

        [Fact]
        public void Create_ReturnsProject()
        {
            BacklogItemTypeSchemaModel typeSchema = _BacklogItemTypeSchemaFixture.Create();
            BacklogItemLinkTypeSchemaModel linkTypeSchema = _BacklogItemLinkTypeSchemaFixture.Create();
            ProjectModel project = new("Test Project", typeSchema.ID, linkTypeSchema.ID);

            project = _projectService.Create(project);

            Assert.NotNull(project);
            Assert.True(project.ID > 0);
        }

        [Fact]
        public void Get_ReturnsProject()
        {
            var project = _ProjectFixture.Create();

            var returnedProject = _projectService.Get(project.ID);

            Assert.NotNull(returnedProject);
            Assert.Equal(project.ID, returnedProject.ID);
        }

        [Fact]
        public void GetAll_ReturnsAllProjects()
        {
            var projects = new List<ProjectModel>
            {
                _ProjectFixture.Create("Test Project 1"),
                _ProjectFixture.Create("Test Project 2")
            };

            _DBContext.GetType();

            PaginationResults<ProjectModel> returnedProjects = _projectService.GetCollection();
            Assert.Equal(projects.Count, returnedProjects.Items.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedProject()
        {
            var project = _ProjectFixture.Create();
            var title = $"{project.Title} Updated";

            project.Title = title;
            project = _projectService.Update(project);

            Assert.NotNull(project);
            Assert.Equal(title, project.Title);
        }

        [Fact]
        public void Delete_DeletesProject()
        {
            var project = _ProjectFixture.Create();

            _projectService.Delete(project);

            Assert.Throws<ModelNotFoundException>(() => 
                _projectService.Get(project.ID));
        }
    }
}
