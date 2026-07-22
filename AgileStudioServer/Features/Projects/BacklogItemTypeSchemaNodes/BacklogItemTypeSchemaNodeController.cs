using AgileStudioServer.Core.APIs;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Projects.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AgileStudioServer.Features.Projects.BacklogItemTypeSchemaNodes
{
    [ApiController]
    [Route("Projects/Projects/{id}/BacklogItemTypeSchema/")]
    [ApiExplorerSettings(GroupName = "projects")]
    [Authorize]
    public class BacklogItemTypeSchemaNodeController : ControllerBase
    {
        private readonly ProjectService _ProjectService;

        private readonly BacklogItemTypeSchemaNodeService _BacklogItemTypeSchemaNodeService;

        private readonly Hydrator _Hydrator;

        private readonly ServiceContext _ServiceContext;

        private readonly PermissionCheckerService _PermissionCheckerService;

        public BacklogItemTypeSchemaNodeController(
            ProjectService projectService,
            BacklogItemTypeSchemaNodeService backlogItemTypeSchemaNodeService,
            Hydrator hydrator,
            ServiceContext serviceContext,
            PermissionCheckerService permissionCheckerService)
        {
            _ProjectService = projectService;
            _BacklogItemTypeSchemaNodeService = backlogItemTypeSchemaNodeService;
            _Hydrator = hydrator;
            _ServiceContext = serviceContext;
            _PermissionCheckerService = permissionCheckerService;
        }

        [HttpGet("Nodes", Name = "GetBacklogItemTypeSchemaNodesForProject")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PaginationResults<BacklogItemTypeSchemaNodeForProjectDto>), StatusCodes.Status200OK)]
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
                    Scopes.PROJECT_BACKLOG_ITEM_TYPE_SCHEMA_NODE,
                    Scopes.PROJECT,
                    id.ToString());

                PaginationResults<BacklogItemTypeSchemaNodeModel> models = _BacklogItemTypeSchemaNodeService.GetBySchemaId(
                    project.BacklogItemTypeSchemaID);

                PaginationResults<BacklogItemTypeSchemaNodeForProjectDto> dtos = new PaginationResults<BacklogItemTypeSchemaNodeForProjectDto>(
                    _Hydrator.HydrateList<BacklogItemTypeSchemaNodeForProjectDto>(models.Items), 
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
    }
}