using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Auth.Roles;
using AgileStudioServer.Features.Projects.Projects;
using AgileStudioServerTest.Features.Accounts.Accounts;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Projects.Projects;
using AgileStudioServerTest.Features.Users.Users;

namespace AgileStudioServerTest.IntegrationTests.Features.Projects.Projects
{
    public class ProjectServiceTest : AbstractServiceTest
    {
        private readonly ProjectService _projectService;

        private readonly ProjectFixture _ProjectFixture;

        private readonly AccountFixture _AccountFixture;

        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;

        private readonly BacklogItemLinkTypeSchemaFixture _BacklogItemLinkTypeSchemaFixture;

        private readonly UserFixture _UserFixture;

        private readonly ServiceContext _ServiceContext;

        public ProjectServiceTest(
            DBContext dbContext,
            ProjectService projectService,
            ProjectFixture projectFixture,
            AccountFixture accountFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture,
            UserFixture userFixture,
            ServiceContext serviceContext) : base(dbContext)
        {
            _projectService = projectService;
            _ProjectFixture = projectFixture;
            _AccountFixture = accountFixture;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _BacklogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
            _UserFixture = userFixture;
            _ServiceContext = serviceContext;
        }

        [Fact]
        public void Create_ReturnsProject()
        {
            AccountModel account = _AccountFixture.Create();
            BacklogItemTypeSchemaModel typeSchema = _BacklogItemTypeSchemaFixture.Create();
            BacklogItemLinkTypeSchemaModel linkTypeSchema = _BacklogItemLinkTypeSchemaFixture.Create();
            ProjectModel project = new(account.ID, "Test Project", typeSchema.ID, linkTypeSchema.ID);

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
        public void GetCollection_ReturnsProjectsReadableByCurrentUser()
        {
            var user = _UserFixture.Create();

            AccountModel account = _AccountFixture.Create();

            var readableProjects = new List<ProjectModel>{
                _ProjectFixture.Create(
                    account, "Owned Project 1", createdBy: user),
                _ProjectFixture.Create(
                    account, "Owned Project 2", createdBy: user)
            };

            readableProjects.ForEach(project =>
                _ProjectFixture.GrantAccess(
                    project.ID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN));

            var nonReadableProjects = new List<ProjectModel>{
                _ProjectFixture.Create(account, "Other Project 1"),
                _ProjectFixture.Create(account, "Other Project 2")
            };

            var projects  = readableProjects.Concat(nonReadableProjects).ToList();

            _ServiceContext.currentUser = IntegrationTestsUtil.
                GenerateCurrentUserClaimsPrincipal(user.ID);

            PaginationResults<ProjectModel> returnedProjects = _projectService.GetCollection();

            Assert.Equal(readableProjects.Count, returnedProjects.Items.Count);
            readableProjects.ForEach(project =>
                Assert.Contains(returnedProjects.Items, p => p.ID == project.ID));
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
