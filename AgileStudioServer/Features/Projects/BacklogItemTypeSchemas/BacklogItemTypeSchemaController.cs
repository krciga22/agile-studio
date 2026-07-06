using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Projects.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Projects.BacklogItemTypeSchemas
{
    [ApiController]
    [Route("Projects/Projects/{id}/")]
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
        [ProducesResponseType(typeof(BacklogItemTypeSchemaForProjectDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            try
            {
                ProjectModel project = _ProjectService.Get(id);

                BacklogItemTypeSchemaModel backlogItemTypeSchema = _BacklogItemTypeSchemaService.Get(
                    project.BacklogItemTypeSchemaID);

                // todo check permissions

                var dto = _Hydrator.Hydrate<BacklogItemTypeSchemaForProjectDto>(backlogItemTypeSchema);
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