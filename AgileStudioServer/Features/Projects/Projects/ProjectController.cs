using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Features.Projects.BacklogItems;
using AgileStudioServer.Features.Projects.Sprints;
using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Projects.Projects
{
    [ApiController]
    [Route("Projects/[controller]")]
    [ApiExplorerSettings(GroupName = "projects")]
    [MapResourceGetCollection(ResourceTypes.ProjectsProject)]
    [MapResourceGet(ResourceTypes.ProjectsProject)]
    [MapResourcePost(ResourceTypes.ProjectsProject)]
    [MapResourcePatch(ResourceTypes.ProjectsProject)]
    [MapResourceDelete(ResourceTypes.ProjectsProject)]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly BacklogItemService _BacklogItemService;

        private readonly SprintService _SprintService;

        private readonly Hydrator _Hydrator;

        public ProjectController(
            ProjectService projectService,
            BacklogItemService backlogItemDataProvider,
            SprintService sprintDataProvider,
            Hydrator Hydrator)
        {
            _BacklogItemService = backlogItemDataProvider;
            _SprintService = sprintDataProvider;
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
    }
}