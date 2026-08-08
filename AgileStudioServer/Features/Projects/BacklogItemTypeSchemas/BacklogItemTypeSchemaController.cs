using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Projects.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AgileStudioServer.Features.Projects.BacklogItemTypeSchemas
{
    [ApiController]
    [Route("Projects/Projects/{id}")]
    [ApiExplorerSettings(GroupName = "projects")]
    [Authorize]
    public class BacklogItemTypeSchemaController : ControllerBase
    {
        private readonly ProjectService _ProjectService;

        private readonly BacklogItemTypeSchemaService _BacklogItemTypeSchemaService;

        private readonly Hydrator _Hydrator;

        private readonly ServiceContext _ServiceContext;

        private readonly PermissionCheckerService _PermissionCheckerService;

        public BacklogItemTypeSchemaController(
            ProjectService projectService,
            BacklogItemTypeSchemaService backlogItemTypeSchemaService,
            Hydrator hydrator,
            ServiceContext serviceContext,
            PermissionCheckerService permissionCheckerService)
        {
            _ProjectService = projectService;
            _BacklogItemTypeSchemaService = backlogItemTypeSchemaService;
            _Hydrator = hydrator;
            _ServiceContext = serviceContext;
            _PermissionCheckerService = permissionCheckerService;
        }

        [HttpGet("BacklogItemTypeSchema", Name = "GetBacklogItemTypeSchemaForProject")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BacklogItemTypeSchemaDto), StatusCodes.Status200OK)]
        [SwaggerOperation(Tags = new[] { "Project" })]
        public IActionResult Get(int id)
        {
            try
            {
                ProjectModel project = _ProjectService.Get(id);

                _PermissionCheckerService.ValidatePermissions(
                    RoleSubjectTypes.USER,
                    _ServiceContext.GetCurrentUserIdStrict().ToString(),
                    PermissionKeys.READ,
                    Scopes.PROJECT_BACKLOG_ITEM_TYPE_SCHEMA,
                    Scopes.PROJECT,
                    id.ToString());

                BacklogItemTypeSchemaModel backlogItemTypeSchema = _BacklogItemTypeSchemaService.Get(
                    project.BacklogItemTypeSchemaID);

                var dto = _Hydrator.Hydrate<BacklogItemTypeSchemaDto>(backlogItemTypeSchema);
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