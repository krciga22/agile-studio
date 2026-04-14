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
    [MapResourceGetCollection(ResourceTypes.ProjectsProject)]
    [MapResourceGet(ResourceTypes.ProjectsProject)]
    [MapResourcePostAttribute(ResourceTypes.ProjectsProject)]
    [MapResourcePatch(ResourceTypes.ProjectsProject)]
    [MapResourceDelete(ResourceTypes.ProjectsProject)]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly BacklogItemService _BacklogItemService;

        private readonly SprintService _SprintService;

        private readonly ReleaseService _ReleaseService;

        private readonly Hydrator _Hydrator;

        public ProjectController(
            ProjectService projectService,
            BacklogItemService backlogItemDataProvider,
            SprintService sprintDataProvider,
            ReleaseService releaseDataProvider,
            Hydrator Hydrator)
        {
            _BacklogItemService = backlogItemDataProvider;
            _SprintService = sprintDataProvider;
            _ReleaseService = releaseDataProvider;
            _Hydrator = Hydrator;
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
    }
}