using AgileStudioServer.Core.APIs;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.Workflows;
using AgileStudioServer.Features.Accounts.WorkflowStates;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Projects.BacklogItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AgileStudioServer.Features.Projects.WorkflowStates
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "projects")]
    [Authorize]
    public class WorkflowStateController : ControllerBase
    {
        private readonly BacklogItemService _BacklogItemService;

        private readonly BacklogItemTypeService _BacklogItemTypeService;

        private readonly WorkflowService _WorkflowService;

        private readonly WorkflowStateService _WorkflowStateService;

        private readonly Hydrator _Hydrator;

        private readonly ServiceContext _ServiceContext;

        private readonly PermissionCheckerService _PermissionCheckerService;

        public WorkflowStateController(
            BacklogItemService backlogItemService,
            BacklogItemTypeService backlogItemTypeService,
            WorkflowService workflowService,
            WorkflowStateService workflowStateService,
            Hydrator hydrator,
            ServiceContext serviceContext,
            PermissionCheckerService permissionCheckerService)
        {
            _BacklogItemService = backlogItemService;
            _BacklogItemTypeService = backlogItemTypeService;
            _WorkflowService = workflowService;
            _WorkflowStateService = workflowStateService;
            _Hydrator = hydrator;
            _ServiceContext = serviceContext;
            _PermissionCheckerService = permissionCheckerService;
        }

        [HttpGet("Projects/BacklogItems/{id}/Workflow/States", Name = "GetWorkflowStatesForBacklogItemWorkflow")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PaginationResults<WorkflowStateDto>), StatusCodes.Status200OK)]
        [SwaggerOperation(Tags = new[] { "BacklogItem" })]
        public IActionResult GetWorkflowStatesForBacklogItemWorkflow(int id, [FromQuery] GetCollectionQueryParams queryParams)
        {
            try
            {
                _ServiceContext.WithGetCollectionQueryParams(queryParams);

                BacklogItemModel backlogItemModel = _BacklogItemService.Get(id);

                _PermissionCheckerService.ValidatePermissions(
                    RoleSubjectTypes.USER,
                    _ServiceContext.GetCurrentUserIdStrict().ToString(),
                    PermissionKeys.LIST,
                    Scopes.PROJECT_BACKLOG_ITEM_WORKFLOW_STATE,
                    Scopes.PROJECT_BACKLOG_ITEM,
                    backlogItemModel.ID.ToString());

                BacklogItemTypeModel backlogItemTypeModel = _BacklogItemTypeService.Get(
                    backlogItemModel.BacklogItemTypeID);

                WorkflowModel workflowModel = _WorkflowService.Get(backlogItemTypeModel.WorkflowID);

                PaginationResults<WorkflowStateModel> models =
                    _WorkflowStateService.GetByWorkflowId(workflowModel.ID);

                PaginationResults<WorkflowStateDto> results = new PaginationResults<WorkflowStateDto>(
                    _Hydrator.HydrateList<WorkflowStateDto>(models.Items),
                    models.Total, models.Page, models.ItemsPerPage);

                return Ok(results);
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