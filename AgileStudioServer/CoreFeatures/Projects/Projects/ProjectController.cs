using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItems;
using AgileStudioServer.CoreFeatures.Releases.Releases;
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
        private readonly ProjectService _ProjectService;

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
            _ProjectService = projectService;
            _BacklogItemService = backlogItemDataProvider;
            _SprintService = sprintDataProvider;
            _ReleaseService = releaseDataProvider;
            _Hydrator = Hydrator;
        }

        [HttpGet(Name = "GetProjects")]
        [ProducesResponseType(typeof(List<ProjectDto>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var models = _ProjectService.GetAll();
            var dtos = _Hydrator.HydrateList<ProjectDto>(models);
            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetProject")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _ProjectService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = _Hydrator.Hydrate<ProjectDto>(model);
            return Ok(dto);
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
        public CreatedResult Post(ProjectPostDto projectPostDto)
        {
            ProjectModel model = _Hydrator.Hydrate<ProjectModel>(projectPostDto);
            model = _ProjectService.Create(model);

            string projectUrl = "";
            if (Url != null)
            {
                projectUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? projectUrl;
            }

            var dto = _Hydrator.Hydrate<ProjectDto>(model);

            return Created(projectUrl, dto);
        }

        [HttpPatch("{id}", Name = "UpdateProject")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, ProjectPatchDto projectPatchDto)
        {
            if (id != projectPatchDto.ID)
            {
                return BadRequest();
            }

            ProjectDto dto;
            try
            {
                ProjectModel model = _Hydrator.Hydrate<ProjectModel>(projectPatchDto);
                model = _ProjectService.Update(model);
                dto = _Hydrator.Hydrate<ProjectDto>(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(ProjectModel)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new OkObjectResult(dto);
        }

        [HttpDelete("{id}", Name = "DeleteProject")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            ProjectModel? model = _ProjectService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _ProjectService.Delete(model);

            return new OkResult();
        }
    }
}