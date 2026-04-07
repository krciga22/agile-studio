using AgileStudioServer.Core.APIs.DTOs;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItems;
using AgileStudioServer.CoreFeatures.Releases.Releases;
using AgileStudioServer.CoreFeatures.Resources.Resource;
using AgileStudioServer.CoreFeatures.Sprints.Sprints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.CoreFeatures.Projects.Projects
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly ResourceController _ResourceController;

        private readonly BacklogItemService _BacklogItemService;

        private readonly SprintService _SprintService;

        private readonly ReleaseService _ReleaseService;

        private readonly Hydrator _Hydrator;

        public ProjectController(
            ResourceController resourceController,
            ProjectService projectService,
            BacklogItemService backlogItemDataProvider,
            SprintService sprintDataProvider,
            ReleaseService releaseDataProvider,
            Hydrator Hydrator)
        {
            _ResourceController = resourceController;
            _BacklogItemService = backlogItemDataProvider;
            _SprintService = sprintDataProvider;
            _ReleaseService = releaseDataProvider;
            _Hydrator = Hydrator;
        }

        [HttpGet(Name = "GetProjects")]
        [ProducesResponseType(typeof(PaginatedResultsDto<ProjectDto, ProjectModel>), StatusCodes.Status200OK)]
        public IActionResult Get([FromQuery] GetCollectionQueryParams queryParams)
        {   
            return _ResourceController.Get(ResourceTypes.ProjectsProject, queryParams);
        }

        [HttpGet("{id}", Name = "GetProject")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            return _ResourceController.Get(ResourceTypes.ProjectsProject, id);
        }

        [HttpGet("{id}/BacklogItems", Name = "GetProjectBacklogItems")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<BacklogItemDto>), StatusCodes.Status200OK)]
        public IActionResult GetBacklogItemsForProject(int id)
        {
            var models = _BacklogItemService.GetByProjectId(id);
            var dtos = _Hydrator.HydrateList<BacklogItemDto>(models);
            return Ok(dtos);
        }

        [HttpGet("{id}/Sprints", Name = "GetProjectSprints")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<SprintSummaryDto>), StatusCodes.Status200OK)]
        public IActionResult GetSprintsForProject(int id)
        {
            var models = _SprintService.GetByProjectId(id);
            var dtos = _Hydrator.HydrateList<SprintSummaryDto>(models);
            return Ok(dtos);
        }

        [HttpGet("{id}/Releases", Name = "GetProjectReleases")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<ReleaseSummaryDto>), StatusCodes.Status200OK)]
        public IActionResult GetReleasesForProject(int id)
        {
            var models = _ReleaseService.GetByProjectId(id);
            var dtos = _Hydrator.HydrateList<ReleaseSummaryDto>(models);
            return Ok(dtos);
        }

        [HttpPost(Name = "CreateProject")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] object data)
        {
            return _ResourceController.Post(ResourceTypes.ProjectsProject, data);
        }

        [HttpPatch("{id}", Name = "UpdateProject")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, [FromBody] object data)
        {
            return _ResourceController.Patch(ResourceTypes.ProjectsProject, id, data);
        }

        [HttpDelete("{id}", Name = "DeleteProject")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            return _ResourceController.Delete(ResourceTypes.ProjectsProject, id);
        }
    }
}