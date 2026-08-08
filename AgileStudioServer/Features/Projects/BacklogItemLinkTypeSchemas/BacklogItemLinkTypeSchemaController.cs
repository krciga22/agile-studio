using AgileStudioServer.Core.APIs;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Projects.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AgileStudioServer.Features.Projects.BacklogItemLinkTypeSchemas
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "projects")]
    [Authorize]
    public class BacklogItemLinkTypeSchemaController : ControllerBase
    {
        private readonly ProjectService _ProjectService;

        private readonly BacklogItemLinkTypeSchemaService _BacklogItemLinkTypeSchemaService;

        private readonly Hydrator _Hydrator;

        private readonly ServiceContext _ServiceContext;

        private readonly PermissionCheckerService _PermissionCheckerService;

        public BacklogItemLinkTypeSchemaController(
            ProjectService projectService,
            BacklogItemLinkTypeSchemaService backlogItemTypeSchemaNodeService,
            Hydrator hydrator,
            ServiceContext serviceContext,
            PermissionCheckerService permissionCheckerService)
        {
            _ProjectService = projectService;
            _BacklogItemLinkTypeSchemaService = backlogItemTypeSchemaNodeService;
            _Hydrator = hydrator;
            _ServiceContext = serviceContext;
            _PermissionCheckerService = permissionCheckerService;
        }

        [HttpGet("Projects/Project/{id}/BacklogItemLinkTypeSchema", 
            Name = "GetBacklogItemLinkTypeSchemaForProject")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BacklogItemLinkTypeSchemaForProjectDto), StatusCodes.Status200OK)]
        [SwaggerOperation(Tags = new[] { "Project" })]
        public IActionResult GetBacklogItemLinkTypeSchemaForProject(int id, [FromQuery] GetCollectionQueryParams queryParams)
        {
            try
            {
                _ServiceContext.WithGetCollectionQueryParams(queryParams);

                ProjectModel projectModel = _ProjectService.Get(id);

                _PermissionCheckerService.ValidatePermissions(
                    RoleSubjectTypes.USER,
                    _ServiceContext.GetCurrentUserIdStrict().ToString(),
                    PermissionKeys.LIST,
                    Scopes.PROJECT_BACKLOG_ITEM_LINK_TYPE_SCHEMA,
                    Scopes.PROJECT,
                    projectModel.ID.ToString());

                BacklogItemLinkTypeSchemaModel? model = _BacklogItemLinkTypeSchemaService.Get(
                    projectModel.BacklogItemLinkTypeSchemaID) ?? throw new ModelNotFoundException(
                        nameof(BacklogItemLinkTypeSchemaModel),
                        projectModel.BacklogItemLinkTypeSchemaID.ToString());

                BacklogItemLinkTypeSchemaForProjectDto dto = 
                    _Hydrator.Hydrate<BacklogItemLinkTypeSchemaForProjectDto>(model);

                return Ok(dto);
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