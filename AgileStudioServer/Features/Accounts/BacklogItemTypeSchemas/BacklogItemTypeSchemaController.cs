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

        [HttpGet(Name = "GetBacklogItemTypeSchemas")]
        [ProducesResponseType(typeof(List<BacklogItemTypeSchemaDto>), StatusCodes.Status200OK)]
        public IActionResult List()
        {
            var models = _BacklogItemTypeSchemaService.GetAll();
            var dtos = _Hydrator.HydrateList<BacklogItemTypeSchemaDto>(models);
            return Ok(dtos);
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

        [HttpGet("{id}/BacklogItemTypes", Name = "ListBacklogItemTypeSchema_BacklogItemTypes")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<BacklogItemTypeSummaryDto>), StatusCodes.Status200OK)]
        public IActionResult ListBacklogItemTypes(int id)
        {
            var models = _BacklogItemTypeService.GetByBacklogItemTypeSchemaId(id);
            var dtos = _Hydrator.HydrateList<BacklogItemTypeSummaryDto>(models);
            return Ok(dtos);
        }

        [HttpPost(Name = "CreateBacklogItemTypeSchema")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemTypeSchemaDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(BacklogItemTypeSchemaPostDto backlogItemTypeSchemaPostDto)
        {
            BacklogItemTypeSchemaModel model = _Hydrator.Hydrate<BacklogItemTypeSchemaModel>(
                backlogItemTypeSchemaPostDto
            );
            model = _BacklogItemTypeSchemaService.Create(model);

            string backlogItemTypeSchemaUrl = "";
            if (Url != null)
            {
                backlogItemTypeSchemaUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? backlogItemTypeSchemaUrl;
            }

            var dto = _Hydrator.Hydrate<BacklogItemTypeSchemaDto>(model);

            return Created(backlogItemTypeSchemaUrl, dto);
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
            BacklogItemTypeSchemaModel? model = _BacklogItemTypeSchemaService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _BacklogItemTypeSchemaService.Delete(model);

            return new OkResult();
        }
    }
}