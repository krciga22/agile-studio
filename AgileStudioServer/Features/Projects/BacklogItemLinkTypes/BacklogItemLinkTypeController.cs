using AgileStudioServer.Core.APIs;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypes;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Projects.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AgileStudioServer.Features.Projects.BacklogItemLinkTypes
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "projects")]
    [Authorize]
    public class BacklogItemLinkTypeController : ControllerBase
    {
        private readonly ProjectService _ProjectService;

        private readonly BacklogItemLinkTypeService _BacklogItemLinkTypeService;

        private readonly Hydrator _Hydrator;

        private readonly ServiceContext _ServiceContext;

        private readonly PermissionCheckerService _PermissionCheckerService;

        public BacklogItemLinkTypeController(
            ProjectService projectService,
            BacklogItemLinkTypeService backlogItemTypeSchemaNodeService,
            Hydrator hydrator,
            ServiceContext serviceContext,
            PermissionCheckerService permissionCheckerService)
        {
            _ProjectService = projectService;
            _BacklogItemLinkTypeService = backlogItemTypeSchemaNodeService;
            _Hydrator = hydrator;
            _ServiceContext = serviceContext;
            _PermissionCheckerService = permissionCheckerService;
        }

        [HttpGet("Projects/Project/{id}/BacklogItemLinkTypeSchema/Types", 
            Name = "GetBacklogItemLinkTypesForProject")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PaginationResults<BacklogItemLinkTypeDto>), StatusCodes.Status200OK)]
        [SwaggerOperation(Tags = new[] { "Project" })]
        public IActionResult GetBacklogItemLinkTypesForProject(int id, [FromQuery] GetCollectionQueryParams queryParams)
        {
            try
            {
                _ServiceContext.WithGetCollectionQueryParams(queryParams);

                ProjectModel projectModel = _ProjectService.Get(id);

                _PermissionCheckerService.ValidatePermissions(
                    RoleSubjectTypes.USER,
                    _ServiceContext.GetCurrentUserIdStrict().ToString(),
                    PermissionKeys.LIST,
                    Scopes.PROJECT_BACKLOG_ITEM_LINK_TYPE,
                    Scopes.PROJECT,
                    projectModel.ID.ToString());

                PaginationResults<BacklogItemLinkTypeModel> models = _BacklogItemLinkTypeService.GetByProjectID(
                    projectModel.ID);

                PaginationResults<BacklogItemLinkTypeDto> dtos = new PaginationResults<BacklogItemLinkTypeDto>(
                    _Hydrator.HydrateList<BacklogItemLinkTypeDto>(models.Items), 
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