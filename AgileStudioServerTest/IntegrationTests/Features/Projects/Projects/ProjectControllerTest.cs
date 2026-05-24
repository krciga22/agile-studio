using AgileStudioServer.Core.APIs;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Auth.Roles;
using AgileStudioServer.Features.Projects.BacklogItems;
using AgileStudioServer.Features.Projects.Projects;
using AgileStudioServer.Features.Projects.Releases;
using AgileStudioServer.Features.Projects.Sprints;
using AgileStudioServer.Features.Resources.Resource;
using AgileStudioServerTest.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypes;
using AgileStudioServerTest.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.Features.Accounts.Workflows;
using AgileStudioServerTest.Features.Accounts.WorkflowStates;
using AgileStudioServerTest.Features.Projects.BacklogItems;
using AgileStudioServerTest.Features.Projects.Projects;
using AgileStudioServerTest.Features.Projects.Releases;
using AgileStudioServerTest.Features.Projects.Sprints;
using AgileStudioServerTest.Features.Users.Users;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AgileStudioServerTest.IntegrationTests.Features.Projects.Projects
{
    public class ProjectControllerTest : ResourceControllerTest
    {
        private readonly ResourceController _ResourceController;

        private readonly ProjectFixture _ProjectFixture;

        private readonly BacklogItemFixture _BacklogItemFixture;

        private readonly BacklogItemTypeFixture _BacklogItemTypeFixture;

        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;

        private readonly BacklogItemLinkTypeSchemaFixture _BacklogItemLinkTypeSchemaFixture;

        private readonly SprintFixture _SprintFixture;

        private readonly ReleaseFixture _ReleaseFixture;

        private readonly UserFixture _UserFixture;

        private readonly WorkflowFixture _WorkflowFixture;

        private readonly WorkflowStateFixture _WorkflowStateFixture;

        public ProjectControllerTest(
            DBContext dbContext,
            ResourceController resourceController,
            ProjectFixture projectFixture,
            BacklogItemFixture backlogItemFixture,
            BacklogItemTypeFixture backlogItemTypeFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture,
            SprintFixture sprintFixture,
            ReleaseFixture releaseFixture,
            UserFixture userFixture,
            WorkflowFixture workflowFixture,
            WorkflowStateFixture workflowStateFixture,
            ServiceContext serviceContext,
            IUrlHelperFactory? iUrlHelperFactory = null) : 
            base(dbContext, serviceContext, iUrlHelperFactory)
        {
            _ResourceController = resourceController;
            _ProjectFixture = projectFixture;
            _BacklogItemFixture = backlogItemFixture;
            _BacklogItemTypeFixture = backlogItemTypeFixture;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _BacklogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
            _SprintFixture = sprintFixture;
            _ReleaseFixture = releaseFixture;
            _UserFixture = userFixture;
            _WorkflowFixture = workflowFixture;
            _WorkflowStateFixture = workflowStateFixture;
        }

        [Fact]
        public void Get_WithNoArguments_ReturnsDtos()
        {
            var user = _UserFixture.Create();

            var project1 = _ProjectFixture.Create(
                "Test Project 1", createdBy: user);
            _ProjectFixture.GrantAccess(project1.ID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            var project2 = _ProjectFixture.Create(
                "Test Project 2", createdBy: user);
            _ProjectFixture.GrantAccess(project2.ID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            var projects = new List<ProjectModel> { project1, project2 };

            InitHttpAndServiceContextWithUser(user);
            var result = _ResourceController.GetCollection(
                _HttpContext, ResourceTypes.ProjectsProject, 
                new GetCollectionQueryParams());

            var objectResult =
                Assert.IsType<Ok<PaginationResults<object>>>(result);

            var paginationResult = 
                Assert.IsType<PaginationResults<object>>(objectResult.Value);

            Assert.Equal(projects.Count, paginationResult.Items.Count);
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var user = _UserFixture.Create();

            var project = _ProjectFixture.Create(
                "Test Project", createdBy: user);
            _ProjectFixture.GrantAccess(project.ID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            InitHttpAndServiceContextWithUser(user);
            object[] id = [project.ID];
            var result = _ResourceController.Get(
                _HttpContext, ResourceTypes.ProjectsProject, id);

            var objectResult =
                Assert.IsType<Ok<object>>(result);

            var projectDtoResult =
                Assert.IsType<ProjectDto>(objectResult.Value);

            Assert.Equal(project.ID, projectDtoResult.ID);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFoundResult()
        {
            var user = _UserFixture.Create();

            InitHttpAndServiceContextWithUser(user);
            object[] id = [Constants.NonExistantId];
            var result = _ResourceController.Get(
                _HttpContext, ResourceTypes.ProjectsProject, id);

            Assert.IsType<NotFound>(result);
        }

        [Fact]
        public void GetBacklogItemsForProject_WithId_ReturnsDtos()
        {
            var user = _UserFixture.Create();

            var project = _ProjectFixture.Create(
                "Test Project 1", createdBy: user);

            _ProjectFixture.GrantAccess(
                project.ID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Get(
                project.BacklogItemTypeSchemaID);

            var backlogItemType = _BacklogItemTypeFixture.Create(
                backlogItemTypeSchema: backlogItemTypeSchema);

            var backlogItem1 = _BacklogItemFixture.Create(
                title: "Test Backlog Item 1",
                project: project,
                backlogItemType: backlogItemType);

            var backlogItem2 = _BacklogItemFixture.Create(
                title: "Test Backlog Item 2",
                project: project,
                backlogItemType: backlogItemType);

            var backlogItems = new List<BacklogItemModel> { 
                backlogItem1, backlogItem2 
            };

            InitHttpAndServiceContextWithUser(user);
            object[] parentId = [project.ID];
            var result = _ResourceController.GetSubCollection(
                _HttpContext, ResourceTypes.BacklogItemsBacklogItem, 
                ResourceTypes.ProjectsProject, parentId,
                new GetCollectionQueryParams());

            var objectResult =
                Assert.IsType<Ok<PaginationResults<object>>>(result);

            var paginationResult =
                Assert.IsType<PaginationResults<object>>(objectResult.Value);

            Assert.Equal(backlogItems.Count, paginationResult.Items.Count);

            foreach(BacklogItemModel backlogItem in backlogItems)
            {
                Assert.Contains(paginationResult.Items, item => 
                    (item as BacklogItemDto)?.ID == backlogItem.ID);
            }
        }

        [Fact]
        public void PostBacklogItemForProject_WithDto_ReturnsDto()
        {
            var user = _UserFixture.Create();

            var project = _ProjectFixture.Create(
                "Test Project", createdBy: user);

            _ProjectFixture.GrantAccess(
                project.ID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Get(
                project.BacklogItemTypeSchemaID);

            var backlogItemType = _BacklogItemTypeFixture.Create(
                backlogItemTypeSchema: backlogItemTypeSchema);

            var workflow = _WorkflowFixture.Get(backlogItemType.WorkflowID);

            var workflowState = _WorkflowStateFixture.Create(
                "Test Workflow State", workflow: workflow);

            var backlogItemPostDto = new BacklogItemPostDto(
                "Test Backlog Item", project.ID, backlogItemType.ID, workflowState.ID);

            object data = IntegrationTestsUtil.ConvertDtoToObject(backlogItemPostDto);

            InitHttpAndServiceContextWithUser(user);
            object[] parentId = new object[] { project.ID };
            var result = _ResourceController.PostSub(
                _HttpContext, ResourceTypes.BacklogItemsBacklogItem, data,
                ResourceTypes.ProjectsProject, parentId, GetUrlHelper());

            var objectResult =
                Assert.IsType<Created<object>>(result);

            var backlogItemDtoResult =
                Assert.IsType<BacklogItemDto>(objectResult.Value);

            Assert.Equal(project.ID, backlogItemDtoResult.Project.ID);
            Assert.Equal(backlogItemPostDto.Title, backlogItemDtoResult.Title);
            Assert.Equal(backlogItemType.ID, backlogItemDtoResult.BacklogItemType.ID);
        }

        [Fact]
        public void GetSprintsForProject_WithId_ReturnsDtos()
        {
            var user = _UserFixture.Create();

            var project = _ProjectFixture.Create(
                "Test Project 1", createdBy: user);

            _ProjectFixture.GrantAccess(
                project.ID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            var sprint1 = _SprintFixture.Create(project: project);

            var sprint2 = _SprintFixture.Create(project: project);

            var sprints = new List<SprintModel> {
                sprint1, sprint2
            };

            InitHttpAndServiceContextWithUser(user);
            object[] parentId = [project.ID];
            var result = _ResourceController.GetSubCollection(
                _HttpContext, ResourceTypes.SprintsSprint,
                ResourceTypes.ProjectsProject, parentId,
                new GetCollectionQueryParams());

            var objectResult =
                Assert.IsType<Ok<PaginationResults<object>>>(result);

            var paginationResult =
                Assert.IsType<PaginationResults<object>>(objectResult.Value);

            Assert.Equal(sprints.Count, paginationResult.Items.Count);

            foreach (SprintModel sprint in sprints)
            {
                Assert.Contains(paginationResult.Items, item =>
                    (item as SprintDto)?.ID == sprint.ID);
            }
        }

        [Fact]
        public void PostSprintForProject_WithDto_ReturnsDto()
        {
            var user = _UserFixture.Create();

            var project = _ProjectFixture.Create(
                "Test Project", createdBy: user);

            _ProjectFixture.GrantAccess(
                project.ID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            var sprintPostDto = new SprintPostDto(project.ID)
            {
                Description = "Test Sprint Description"
            };

            object data = IntegrationTestsUtil.ConvertDtoToObject(sprintPostDto);

            InitHttpAndServiceContextWithUser(user);
            object[] parentId = { project.ID };
            var result = _ResourceController.PostSub(
                _HttpContext, ResourceTypes.SprintsSprint, data,
                ResourceTypes.ProjectsProject, parentId, GetUrlHelper());

            var objectResult =
                Assert.IsType<Created<object>>(result);

            var sprintDtoResult =
                Assert.IsType<SprintDto>(objectResult.Value);

            Assert.Equal(1, sprintDtoResult.SprintNumber);
            Assert.Equal(project.ID, sprintDtoResult.Project.ID);
            Assert.Equal(sprintPostDto.Description, sprintDtoResult.Description);
        }

        [Fact]
        public void GetReleasesForProject_WithId_ReturnsDtos()
        {
            var user = _UserFixture.Create();

            var project = _ProjectFixture.Create(
                "Test Project 1", createdBy: user);

            _ProjectFixture.GrantAccess(
                project.ID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            var release1 = _ReleaseFixture.Create(project: project);

            var release2 = _ReleaseFixture.Create(project: project);

            var releases = new List<ReleaseModel> {
                release1, release2
            };

            InitHttpAndServiceContextWithUser(user);
            object[] parentId = [project.ID];
            var result = _ResourceController.GetSubCollection(
                _HttpContext, ResourceTypes.ReleasesRelease,
                ResourceTypes.ProjectsProject, parentId,
                new GetCollectionQueryParams());

            var objectResult =
                Assert.IsType<Ok<PaginationResults<object>>>(result);

            var paginationResult =
                Assert.IsType<PaginationResults<object>>(objectResult.Value);

            Assert.Equal(releases.Count, paginationResult.Items.Count);

            foreach (ReleaseModel release in releases)
            {
                Assert.Contains(paginationResult.Items, item =>
                    (item as ReleaseDto)?.ID == release.ID);
            }
        }

        [Fact]
        public void PostReleaseForProject_WithDto_ReturnsDto()
        {
            var user = _UserFixture.Create();

            var project = _ProjectFixture.Create(
                "Test Project", createdBy: user);

            _ProjectFixture.GrantAccess(
                project.ID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            var releasePostDto = new ReleasePostDto("Test Release", project.ID);

            object data = IntegrationTestsUtil.ConvertDtoToObject(releasePostDto);

            InitHttpAndServiceContextWithUser(user);
            object[] parentId = { project.ID };
            var result = _ResourceController.PostSub(
                _HttpContext, ResourceTypes.ReleasesRelease, data,
                ResourceTypes.ProjectsProject, parentId, GetUrlHelper());

            var objectResult =
                Assert.IsType<Created<object>>(result);

            var releaseDtoResult =
                Assert.IsType<ReleaseDto>(objectResult.Value);

            Assert.Equal(project.ID, releaseDtoResult.Project.ID);
            Assert.Equal(releasePostDto.Title, releaseDtoResult.Title);
        }

        [Fact]
        public void Post_WithDto_ReturnsDto()
        {
            var user = _UserFixture.Create();

            var backlogItemTypeSchema = 
                _BacklogItemTypeSchemaFixture.Create();

            var backlogItemLinkTypeSchema = 
                _BacklogItemLinkTypeSchemaFixture.Create();

            var projectPostDto = new ProjectPostDto("Test Project",
                backlogItemTypeSchema.ID, backlogItemLinkTypeSchema.ID);

            object data = IntegrationTestsUtil.ConvertDtoToObject(projectPostDto);

            InitHttpAndServiceContextWithUser(user);
            var result = _ResourceController.Post(
                _HttpContext, ResourceTypes.ProjectsProject, data, GetUrlHelper());

            var objectResult =
                Assert.IsType<Created<object>>(result);

            var projectDtoResult =
                Assert.IsType<ProjectDto>(objectResult.Value);

            Assert.Equal(projectPostDto.Title, projectDtoResult.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var user = _UserFixture.Create();

            var project = _ProjectFixture.Create(
                "Test Project", createdBy: user);

            _ProjectFixture.GrantAccess(
                project.ID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            var projectPatchDto = new ProjectPatchDto(project.ID, "Test Project Z");
            object data = IntegrationTestsUtil.ConvertDtoToObject(projectPatchDto);

            InitHttpAndServiceContextWithUser(user);
            object[] id = [project.ID];
            var result = _ResourceController.Patch(
                _HttpContext, ResourceTypes.ProjectsProject, id, data);

            var objectResult =
                Assert.IsType<Ok<object>>(result);

            var projectDtoResult =
                Assert.IsType<ProjectDto>(objectResult.Value);

            Assert.Equal(projectPatchDto.Title, projectDtoResult.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var user = _UserFixture.Create();

            var project = _ProjectFixture.Create(
                "Test Project", createdBy: user);

            _ProjectFixture.GrantAccess(
                project.ID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            InitHttpAndServiceContextWithUser(user);
            object[] id = [project.ID];
            var result = _ResourceController.Delete(
                _HttpContext, ResourceTypes.ProjectsProject, id);

            Assert.IsType<Ok>(result);
        }

        [Fact]
        public void Delete_WithInvalidId_ReturnsNotFoundResult()
        {
            var user = _UserFixture.Create();

            var project = _ProjectFixture.Create(
                "Test Project", createdBy: user);

            _ProjectFixture.GrantAccess(
                project.ID, user.ID, RoleKeys.PROJECTS_PROJECT_ADMIN);

            InitHttpAndServiceContextWithUser(user);
            object[] id = [Constants.NonExistantId];
            var result = _ResourceController.Delete(
                _HttpContext, ResourceTypes.ProjectsProject, id);

            Assert.IsType<NotFound>(result);
        }
    }
}
