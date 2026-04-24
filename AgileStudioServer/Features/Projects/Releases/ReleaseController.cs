using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Features.Projects.Projects;
using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Projects.Releases
{
    [ApiController]
    [Route("Projects/Releases")]
    [ApiExplorerSettings(GroupName = "projects")]
    [MapResourceGet(ResourceTypes.ReleasesRelease)]
    [MapResourcePost(ResourceTypes.ReleasesRelease)]
    [MapResourcePatch(ResourceTypes.ReleasesRelease)]
    [MapResourceDelete(ResourceTypes.ReleasesRelease)]
    [Authorize]
    public class ReleaseController : ControllerBase
    {
        private readonly ReleaseService _ReleaseService;
        private readonly Hydrator _Hydrator;

        public ReleaseController(
            ReleaseService releaseService,
            ProjectService projectService,
            Hydrator hydrator)
        {
            _ReleaseService = releaseService;
            _Hydrator = hydrator;
        }

        [Tags("Project")]
        [HttpGet("/Projects/Project/{id}/Releases", Name = "GetProjectReleases")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<ReleaseDto>), StatusCodes.Status200OK)]
        public IActionResult GetReleasesForProject(int id)
        {
            // todo use pagination
            var models = _ReleaseService.GetByProjectId(id);
            var dtos = _Hydrator.HydrateList<ReleaseDto>(models);
            return Ok(dtos);
        }
    }
}