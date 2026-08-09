using AgileStudioServer.Core.APIs;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.Workflows;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Projects.BacklogItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AgileStudioServer.Features.Projects.Workflows
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "projects")]
    [Authorize]
    public class WorkflowController : ControllerBase
    {
        private readonly BacklogItemService _BacklogItemService;

        private readonly BacklogItemTypeService _BacklogItemTypeService;

        private readonly WorkflowService _WorkflowService;

        private readonly Hydrator _Hydrator;

        private readonly ServiceContext _ServiceContext;

        private readonly PermissionCheckerService _PermissionCheckerService;

        public WorkflowController(
            BacklogItemService backlogItemService,
            BacklogItemTypeService backlogItemTypeService,
            WorkflowService workflowService,
            Hydrator hydrator,
            ServiceContext serviceContext,
            PermissionCheckerService permissionCheckerService)
        {
            _BacklogItemService = backlogItemService;
            _BacklogItemTypeService = backlogItemTypeService;
            _WorkflowService = workflowService;
            _Hydrator = hydrator;
            _ServiceContext = serviceContext;
            _PermissionCheckerService = permissionCheckerService;
        }

        [HttpGet("Projects/BacklogItems/{id}/Workflow", Name = "GetWorkflowForBacklogItem")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PaginationResults<WorkflowDto>), StatusCodes.Status200OK)]
        [SwaggerOperation(Tags = new[] { "BacklogItem" })]
        public IActionResult GetWorkflowForBacklogItem(int id, [FromQuery] GetCollectionQueryParams queryParams)
        {
            try
            {
                _ServiceContext.WithGetCollectionQueryParams(queryParams);

                BacklogItemModel backlogItemModel = _BacklogItemService.Get(id);

                BacklogItemTypeModel backlogItemTypeModel = _BacklogItemTypeService.Get(
                    backlogItemModel.BacklogItemTypeID);

                WorkflowModel workflowModel = _WorkflowService.Get(backlogItemTypeModel.WorkflowID);

                _PermissionCheckerService.ValidatePermissions(
                    RoleSubjectTypes.USER,
                    _ServiceContext.GetCurrentUserIdStrict().ToString(),
                    PermissionKeys.READ,
                    Scopes.PROJECT_BACKLOG_ITEM_WORKFLOW,
                    Scopes.PROJECT_BACKLOG_ITEM,
                    backlogItemModel.ID.ToString());

                WorkflowDto dto = _Hydrator.Hydrate<WorkflowDto>(workflowModel);

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