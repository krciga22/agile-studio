using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas
{
    [ApiController]
    [Route("Accounts/BacklogItemTypeSchemas")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [Authorize]
    public class BacklogItemTypeSchemaController : ControllerBase
    {
        private readonly BacklogItemTypeSchemaService _BacklogItemTypeSchemaService;

        private readonly BacklogItemTypeService _BacklogItemTypeService;

        private readonly Hydrator _Hydrator;

        public BacklogItemTypeSchemaController(
            BacklogItemTypeSchemaService dataProvider,
            BacklogItemTypeService backlogItemTypeDataProvider,
            Hydrator hydrator)
        {
            _BacklogItemTypeSchemaService = dataProvider;
            _BacklogItemTypeService = backlogItemTypeDataProvider;
            _Hydrator = hydrator;
        }

        [HttpGet("{id}", Name = "GetBacklogItemTypeSchema")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BacklogItemTypeSchemaDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _BacklogItemTypeSchemaService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = _Hydrator.Hydrate<BacklogItemTypeSchemaDto>(model);
            return Ok(dto);
        }

        [HttpPatch("{id}", Name = "UpdateBacklogItemTypeSchema")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemTypeSchemaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, BacklogItemTypeSchemaPatchDto backlogItemTypeSchemaPatchDto)
        {
            if (id != backlogItemTypeSchemaPatchDto.ID)
            {
                return BadRequest();
            }

            BacklogItemTypeSchemaDto dto;
            try
            {
                BacklogItemTypeSchemaModel model = _Hydrator.Hydrate<BacklogItemTypeSchemaModel>(backlogItemTypeSchemaPatchDto);
                model = _BacklogItemTypeSchemaService.Update(model);
                dto = _Hydrator.Hydrate<BacklogItemTypeSchemaDto>(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(BacklogItemTypeSchemaModel)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new OkObjectResult(dto);
        }

        [HttpDelete("{id}", Name = "DeleteBacklogItemTypeSchema")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            try
            {
                BacklogItemTypeSchemaModel? model = _BacklogItemTypeSchemaService.Get(id);
                _BacklogItemTypeSchemaService.Delete(model);
                return new OkResult();
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(BacklogItemTypeSchemaModel)))
                {
                    return NotFound();
                }
                else
                {
                    return Problem();
                }
            }
            catch(Exception)
            {
                return Problem();
            }
        }
    }
}