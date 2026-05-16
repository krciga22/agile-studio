using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Projects.BacklogItems;
using AgileStudioServer.Features.Projects.Sprints;
using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Projects.Projects
{
    [ApiController]
    [Route("Projects/Projects")]
    [ApiExplorerSettings(GroupName = "projects")]
    [MapResourceGetCollection(ResourceTypes.ProjectsProject)]
    [MapResourceGet(ResourceTypes.ProjectsProject)]
    [MapResourcePost(ResourceTypes.ProjectsProject)]
    [MapResourcePatch(ResourceTypes.ProjectsProject)]
    [MapResourceDelete(ResourceTypes.ProjectsProject)]
    [MapSubResourceGetCollection(
        ResourceTypes.ProjectsProject,
        ResourceTypes.ReleasesRelease,
        "Releases")]
    [MapSubResourcePost(
        ResourceTypes.ProjectsProject,
        ResourceTypes.ReleasesRelease,
        "Releases")]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly BacklogItemService _BacklogItemService;

        private readonly SprintService _SprintService;
        private readonly PermissionCheckerService _PermissionCheckerService;
        private readonly ServiceContext _ServiceContext;
        private readonly Hydrator _Hydrator;

        public ProjectController(
            ProjectService projectService,
            BacklogItemService backlogItemDataProvider,
            SprintService sprintDataProvider,
            PermissionCheckerService permissionCheckerService,
            ServiceContext serviceContext,
            Hydrator Hydrator)
        {
            _BacklogItemService = backlogItemDataProvider;
            _SprintService = sprintDataProvider;
            _PermissionCheckerService = permissionCheckerService;
            _ServiceContext = serviceContext;
            _Hydrator = Hydrator;
        }

        [HttpGet("{id}/BacklogItems", Name = "GetProjectBacklogItems")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<BacklogItemDto>), StatusCodes.Status200OK)]
        public IActionResult GetBacklogItemsForProject(int id)
        {
            try
            {
                ValidateCanReadProject(id);

                var models = _BacklogItemService.GetByProjectId(id);

                var dtos = _Hydrator.HydrateList<BacklogItemDto>(models);
                return Ok(dtos);
            }
            catch(UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpGet("{id}/Sprints", Name = "GetProjectSprints")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<SprintSummaryDto>), StatusCodes.Status200OK)]
        public IActionResult GetSprintsForProject(int id)
        {
            try
            {
                ValidateCanReadProject(id);

                var models = _SprintService.GetByProjectId(id);

                var dtos = _Hydrator.HydrateList<SprintSummaryDto>(models);
                return Ok(dtos);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        private void ValidateCanReadProject(int projectId)
        {
            int currentUserId = _ServiceContext.GetCurrentUserIdStrict();

            _PermissionCheckerService.ValidatePermissions(
                RoleSubjectTypes.USER,
                currentUserId.ToString(),
                Scopes.PROJECT,
                projectId.ToString(),
                PermissionKeys.READ
            );
        }
    }
}