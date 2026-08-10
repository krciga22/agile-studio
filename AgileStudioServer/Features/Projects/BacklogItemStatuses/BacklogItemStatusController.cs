using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Projects.BacklogItems;
using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AgileStudioServer.Features.Projects.BacklogItemStatuses
{
    [ApiController]
    [Route("Projects/BacklogItemStatuses")]
    [ApiExplorerSettings(GroupName = "projects")]
    [MapResourceGet(ResourceTypes.ProjectsBacklogItemStatus)]
    [Authorize]
    public class BacklogItemStatusController : ControllerBase
    {
        private readonly BacklogItemStatusService _BacklogItemStatusService;

        private readonly BacklogItemService _BacklogItemService;

        private readonly PermissionCheckerService _PermissionCheckerService;

        private readonly Hydrator _Hydrator;

        private readonly ServiceContext _ServiceContext;

        public BacklogItemStatusController(
            BacklogItemStatusService backlogItemStatusService,
            BacklogItemService backlogItemService,
            PermissionCheckerService permissionCheckerService,
            Hydrator hydrator,
            ServiceContext serviceContext)
        {
            _BacklogItemStatusService = backlogItemStatusService;
            _BacklogItemService = backlogItemService;
            _PermissionCheckerService = permissionCheckerService;
            _Hydrator = hydrator;
            _ServiceContext = serviceContext;
        }

        [HttpGet("/Projects/BacklogItems/{id}/Statuses/Current",
            Name = "GetBacklogItemStatusForProject")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BacklogItemStatusDto), StatusCodes.Status200OK)]
        [SwaggerOperation(Tags = new[] { "BacklogItem" })]
        public IActionResult GetBacklogItemStatusForProject(int id)
        {
            try
            {
                BacklogItemModel backlogItemModel = _BacklogItemService.Get(id);

                _PermissionCheckerService.ValidatePermissions(
                    RoleSubjectTypes.USER,
                    _ServiceContext.GetCurrentUserIdStrict().ToString(),
                    PermissionKeys.READ,
                    Scopes.PROJECT_BACKLOG_ITEM_STATUS,
                    Scopes.PROJECT_BACKLOG_ITEM,
                    backlogItemModel.ID.ToString());

                BacklogItemStatusModel model = _BacklogItemStatusService.GetLatestForBacklogItemId(id);

                BacklogItemStatusDto dto = _Hydrator.Hydrate<BacklogItemStatusDto>(model);

                return Ok(dto);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(BacklogItemModel)))
                {
                    return NotFound();
                }
                else
                {
                    return Problem();
                }
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}