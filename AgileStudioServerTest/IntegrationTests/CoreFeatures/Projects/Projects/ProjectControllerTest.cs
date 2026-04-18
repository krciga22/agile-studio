using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.CoreFeatures.Projects.BacklogItems;
using AgileStudioServer.CoreFeatures.Projects.Sprints;
using AgileStudioServer.CoreFeatures.Projects.Releases;
using AgileStudioServer.CoreFeatures.Projects.Projects;
using AgileStudioServerTest.CoreFeatures.Projects.Projects;
using AgileStudioServer.Data;
using AgileStudioServer.Core.APIs.DTOs;
using AgileStudioServerTest.CoreFeatures.Projects.Releases;
using AgileStudioServerTest.CoreFeatures.Projects.Sprints;
using AgileStudioServerTest.CoreFeatures.Projects.BacklogItems;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemTypeSchemas;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemTypes;
using AgileStudioServerTest.CoreFeatures.Accounts.BacklogItemLinkTypeSchemas;

namespace AgileStudioServerTest.IntegrationTests.CoreFeatures.Projects.Projects
{
    public class ProjectControllerTest : AbstractControllerTest
    {
        private readonly ProjectController _Controller;

        private readonly ProjectFixture _ProjectFixture;

        private readonly BacklogItemFixture _BacklogItemFixture;

        private readonly BacklogItemTypeFixture _BacklogItemTypeFixture;

        private readonly BacklogItemTypeSchemaFixture _BacklogItemTypeSchemaFixture;

        private readonly BacklogItemLinkTypeSchemaFixture _BacklogItemLinkTypeSchemaFixture;

        private readonly SprintFixture _SprintFixture;

        private readonly ReleaseFixture _ReleaseFixture;

        public ProjectControllerTest(
            DBContext dbContext,
            ProjectController controller,
            ProjectFixture projectFixture,
            BacklogItemFixture backlogItemFixture,
            BacklogItemTypeFixture backlogItemTypeFixture,
            BacklogItemTypeSchemaFixture backlogItemTypeSchemaFixture,
            BacklogItemLinkTypeSchemaFixture backlogItemLinkTypeSchemaFixture,
            SprintFixture sprintFixture,
            ReleaseFixture releaseFixture) : base(dbContext)
        {
            _Controller = controller;
            _ProjectFixture = projectFixture;
            _BacklogItemFixture = backlogItemFixture;
            _BacklogItemTypeFixture = backlogItemTypeFixture;
            _BacklogItemTypeSchemaFixture = backlogItemTypeSchemaFixture;
            _BacklogItemLinkTypeSchemaFixture = backlogItemLinkTypeSchemaFixture;
            _SprintFixture = sprintFixture;
            _ReleaseFixture = releaseFixture;
        }

        [Fact]
        public void Get_WithNoArguments_ReturnsDtos()
        {
            //List<ProjectModel> projects = new() {
            //    _ProjectFixture.Create("Test Project 1"),
            //    _ProjectFixture.Create("Test Project 2")
            //};

            //PaginatedResultsDto<ProjectDto, ProjectModel>? projectDtos = null;
            //IActionResult result = _Controller.Get(
            //    new GetCollectionQueryParams());
            //if (result is OkObjectResult okResult)
            //{
            //    projectDtos = okResult.Value as PaginatedResultsDto<ProjectDto, ProjectModel>;
            //}

            //Assert.IsType<PaginatedResultsDto<ProjectDto, ProjectModel>>(projectDtos);
            //Assert.Equal(projects.Count, projectDtos.Items.Count);
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            //var project = _ProjectFixture.Create();

            //ProjectDto? projectDto = null;
            //IActionResult result = _Controller.Get(project.ID);
            //if (result is OkObjectResult okResult)
            //{
            //    projectDto = okResult.Value as ProjectDto;
            //}

            //Assert.IsType<ProjectDto>(projectDto);
            //Assert.Equal(project.ID, projectDto.ID);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFoundResult()
        {
            //IActionResult result = _Controller.Get(Constants.NonExistantId);

            //Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public void GetBacklogItemsForProject_WithId_ReturnsDtos()
        {
            //var project = _ProjectFixture.Create();
            //var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Get(
            //    project.BacklogItemTypeSchemaID);
            //var backlogItemType = _BacklogItemTypeFixture.Create(
            //    backlogItemTypeSchema: backlogItemTypeSchema);

            //List<BacklogItemModel> backlogItems = new() {
            //    _BacklogItemFixture.Create(
            //        title: "Test Backlog Item 1",
            //        project: project,
            //        backlogItemType: backlogItemType),
            //    _BacklogItemFixture.Create(
            //        title: "Test Backlog Item 2",
            //        project: project,
            //        backlogItemType: backlogItemType)
            //};

            //List<BacklogItemDto>? dtos = null;
            //IActionResult result = _Controller.GetBacklogItemsForProject(project.ID);
            //if (result is OkObjectResult okResult)
            //{
            //    dtos = okResult.Value as List<BacklogItemDto>;
            //}

            //Assert.IsType<List<BacklogItemDto>>(dtos);
            //Assert.Equal(backlogItems.Count, dtos.Count);
        }

        [Fact]
        public void GetSprintsForProject_WithId_ReturnsDtos()
        {
            //var project = _ProjectFixture.Create();

            //List<SprintModel> sprints = new() {
            //    _SprintFixture.Create(
            //        sprintNumber: 1,
            //        project: project),
            //    _SprintFixture.Create(
            //        sprintNumber: 2,
            //        project: project)
            //};

            //List<SprintSummaryDto>? dtos = null;
            //IActionResult result = _Controller.GetSprintsForProject(project.ID);
            //if (result is OkObjectResult okResult)
            //{
            //    dtos = okResult.Value as List<SprintSummaryDto>;
            //}

            //Assert.IsType<List<SprintSummaryDto>>(dtos);
            //Assert.Equal(sprints.Count, dtos.Count);
        }

        [Fact]
        public void GetReleasesForProject_WithId_ReturnsDtos()
        {
            //var project = _ProjectFixture.Create();

            //List<ReleaseModel> releases = new() {
            //    _ReleaseFixture.Create(
            //        title: "v1.0.0",
            //        project: project),
            //    _ReleaseFixture.Create(
            //        title: "v1.0.1",
            //        project: project)
            //};

            //List<ReleaseSummaryDto>? dtos = null;
            //IActionResult result = _Controller.GetReleasesForProject(project.ID);
            //if (result is OkObjectResult okResult)
            //{
            //    dtos = okResult.Value as List<ReleaseSummaryDto>;
            //}

            //Assert.IsType<List<ReleaseSummaryDto>>(dtos);
            //Assert.Equal(releases.Count, dtos.Count);
        }

        [Fact]
        public void Post_WithDto_ReturnsDto()
        {
            //var backlogItemTypeSchema = _BacklogItemTypeSchemaFixture.Create();
            //var backlogItemLinkTypeSchema = _BacklogItemLinkTypeSchemaFixture.Create();
            //var projectPostDto = new ProjectPostDto(
            //    "Test Project", 
            //    backlogItemTypeSchema.ID, 
            //    backlogItemLinkTypeSchema.ID);

            //ProjectDto? projectDto = null;
            //IActionResult result = _Controller.Post(projectPostDto);
            //if (result is CreatedResult createdResult)
            //{
            //    projectDto = createdResult.Value as ProjectDto;
            //}

            //Assert.IsType<ProjectDto>(projectDto);
            //Assert.Equal(projectPostDto.Title, projectDto.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            //var project = _ProjectFixture.Create();
            //var title = $"{project.Title} Updated";
            //var projectPatchDto = new ProjectPatchDto(project.ID, title);

            //IActionResult result = _Controller.Patch(project.ID, projectPatchDto);
            //ProjectDto? projectDto = null;
            //if (result is OkObjectResult okObjectResult)
            //{
            //    projectDto = okObjectResult.Value as ProjectDto;
            //}

            //Assert.IsType<ProjectDto>(projectDto);
            //Assert.Equal(projectPatchDto.Title, projectDto.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            //var project = _ProjectFixture.Create();

            //IActionResult result = _Controller.Delete(project.ID);

            //Assert.IsType<OkResult>(result as OkResult);
        }

        [Fact]
        public void Delete_WithInvalidId_ReturnsNotFoundResult()
        {
            //IActionResult result = _Controller.Delete(Constants.NonExistantId);

            //Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }
    }
}
