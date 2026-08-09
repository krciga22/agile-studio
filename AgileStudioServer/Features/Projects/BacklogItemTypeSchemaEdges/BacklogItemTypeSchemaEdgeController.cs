using AgileStudioServer.Core.APIs;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Projects.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AgileStudioServer.Features.Projects.BacklogItemTypeSchemaEdges
{
    [ApiController]
    [Route("Projects/Projects/{id}/BacklogItemTypeSchema/")]
    [ApiExplorerSettings(GroupName = "projects")]
    [Authorize]
    public class BacklogItemTypeSchemaEdgeController : ControllerBase
    {
        private readonly ProjectService _ProjectService;

        private readonly BacklogItemTypeSchemaNodeService _backlogItemTypeSchemaNodeService;

        private readonly BacklogItemTypeSchemaEdgeService _BacklogItemTypeSchemaEdgeService;

        private readonly Hydrator _Hydrator;

        private readonly ServiceContext _ServiceContext;

        private readonly PermissionCheckerService _PermissionCheckerService;

        public BacklogItemTypeSchemaEdgeController(
            ProjectService projectService,
            BacklogItemTypeSchemaNodeService backlogItemTypeSchemaNodeService,
            BacklogItemTypeSchemaEdgeService backlogItemTypeSchemaEdgeService,
            Hydrator hydrator,
            ServiceContext serviceContext,
            PermissionCheckerService permissionCheckerService)
        {
            _ProjectService = projectService;
            _backlogItemTypeSchemaNodeService = backlogItemTypeSchemaNodeService;
            _BacklogItemTypeSchemaEdgeService = backlogItemTypeSchemaEdgeService;
            _Hydrator = hydrator;
            _ServiceContext = serviceContext;
            _PermissionCheckerService = permissionCheckerService;
        }

        [HttpGet("Edges", Name = "GetBacklogItemTypeSchemaEdgesForProject")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PaginationResults<BacklogItemTypeSchemaEdgeDto>), StatusCodes.Status200OK)]
        [SwaggerOperation(Tags = new[] { "Project" })]
        public IActionResult Get(int id, [FromQuery] GetCollectionQueryParams queryParams)
        {
            try
            {
                _ServiceContext.WithGetCollectionQueryParams(queryParams);

                ProjectModel project = _ProjectService.Get(id);

                _PermissionCheckerService.ValidatePermissions(
                    RoleSubjectTypes.USER, 
                    _ServiceContext.GetCurrentUserIdStrict().ToString(),
                    PermissionKeys.LIST,
                    Scopes.PROJECT_BACKLOG_ITEM_TYPE_SCHEMA_EDGE,
                    Scopes.PROJECT,
                    id.ToString());

                PaginationResults<BacklogItemTypeSchemaEdgeModel> models = _BacklogItemTypeSchemaEdgeService.GetByFromTypeId(
                    null, project.BacklogItemTypeSchemaID);

                PaginationResults<BacklogItemTypeSchemaEdgeDto> dtos = new PaginationResults<BacklogItemTypeSchemaEdgeDto>(
                    _Hydrator.HydrateList<BacklogItemTypeSchemaEdgeDto>(models.Items), 
                    models.Total, models.Page, models.ItemsPerPage);

                return Ok(dtos);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(ProjectModel)))
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

        [HttpGet("Edges/{fromTypeID}", Name = "GetBacklogItemTypeSchemaEdgesByProjectAndFromType")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PaginationResults<BacklogItemTypeSchemaEdgeDto>), StatusCodes.Status200OK)]
        [SwaggerOperation(Tags = new[] { "Project" })]
        public IActionResult GetEdgesByProjectAndFromTypeID(
            int id, int fromTypeID, [FromQuery] GetCollectionQueryParams queryParams)
        {
            try
            {
                _ServiceContext.WithGetCollectionQueryParams(queryParams);

                ProjectModel project = _ProjectService.Get(id);

                BacklogItemTypeSchemaNodeModel node = _backlogItemTypeSchemaNodeService.GetBySchemaAndBacklogItemType(
                    project.BacklogItemTypeSchemaID, fromTypeID);

                _PermissionCheckerService.ValidatePermissions(
                    RoleSubjectTypes.USER,
                    _ServiceContext.GetCurrentUserIdStrict().ToString(),
                    PermissionKeys.LIST,
                    Scopes.PROJECT_BACKLOG_ITEM_TYPE_SCHEMA_EDGE,
                    Scopes.PROJECT,
                    id.ToString());

                PaginationResults<BacklogItemTypeSchemaEdgeModel> models = _BacklogItemTypeSchemaEdgeService.GetByFromTypeId(
                    node.BacklogItemTypeID, project.BacklogItemTypeSchemaID);

                PaginationResults<BacklogItemTypeSchemaEdgeDto> dtos = new PaginationResults<BacklogItemTypeSchemaEdgeDto>(
                    _Hydrator.HydrateList<BacklogItemTypeSchemaEdgeDto>(models.Items),
                    models.Total, models.Page, models.ItemsPerPage);

                return Ok(dtos);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(ProjectModel)) || 
                    e.ModelClassName.Equals(nameof(BacklogItemTypeSchemaNodeModel)))
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