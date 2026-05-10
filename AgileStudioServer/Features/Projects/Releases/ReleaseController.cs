using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Scopes;
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
        private readonly ServiceContext _ServiceContext;
        private readonly PermissionCheckerService _PermissionCheckerService;

        public ReleaseController(
            ReleaseService releaseService,
            Hydrator hydrator,
            ServiceContext serviceContext,
            PermissionCheckerService permissionCheckerService)
        {
            _ReleaseService = releaseService;
            _Hydrator = hydrator;
            _ServiceContext = serviceContext;
            _PermissionCheckerService = permissionCheckerService;
        }

        [Tags("Project")]
        [HttpGet("/Projects/Project/{id}/Releases", Name = "GetProjectReleases")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<ReleaseDto>), StatusCodes.Status200OK)]
        public IActionResult GetReleasesForProject(int id)
        {
            try
            {
                int currentUserId = _ServiceContext.GetCurrentUserIdStrict();

                _PermissionCheckerService.ValidatePermissions(
                    RoleSubjectTypes.USER,
                    currentUserId.ToString(),
                    Scopes.PROJECT,
                    id.ToString(),
                    PermissionKeys.READ
                );

                // todo use pagination
                var models = _ReleaseService.GetByProjectId(id);
                var dtos = _Hydrator.HydrateList<ReleaseDto>(models);
                return Ok(dtos);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }
}