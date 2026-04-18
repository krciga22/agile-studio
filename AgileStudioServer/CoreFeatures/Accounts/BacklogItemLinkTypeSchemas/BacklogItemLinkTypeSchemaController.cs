using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypeSchemas
{
    [ApiController]
    [Route("Accounts/[controller]")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [Authorize]
    public class BacklogItemLinkTypeSchemaController : ControllerBase
    {
        private readonly BacklogItemLinkTypeSchemaService _BacklogItemLinkTypeSchemaService;

        private readonly Hydrator _Hydrator;

        public BacklogItemLinkTypeSchemaController(
            BacklogItemLinkTypeSchemaService dataProvider,
            Hydrator hydrator)
        {
            _BacklogItemLinkTypeSchemaService = dataProvider;
            _Hydrator = hydrator;
        }

        [HttpGet(Name = "GetBacklogItemLinkTypeSchemas")]
        [ProducesResponseType(typeof(List<BacklogItemLinkTypeSchemaDto>), StatusCodes.Status200OK)]
        public IActionResult List()
        {
            var models = _BacklogItemLinkTypeSchemaService.GetAll();
            var dtos = _Hydrator.HydrateList<BacklogItemLinkTypeSchemaDto>(models);
            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetBacklogItemLinkTypeSchema")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BacklogItemLinkTypeSchemaDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _BacklogItemLinkTypeSchemaService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = _Hydrator.Hydrate<BacklogItemLinkTypeSchemaDto>(model);
            return Ok(dto);
        }

        [HttpPost(Name = "CreateBacklogItemLinkTypeSchema")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemLinkTypeSchemaDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(BacklogItemLinkTypeSchemaPostDto backlogItemLinkTypeSchemaPostDto)
        {
            BacklogItemLinkTypeSchemaModel model = _Hydrator.Hydrate<BacklogItemLinkTypeSchemaModel>(backlogItemLinkTypeSchemaPostDto);
            model = _BacklogItemLinkTypeSchemaService.Create(model);

            string backlogItemLinkTypeSchemaUrl = "";
            if (Url != null)
            {
                backlogItemLinkTypeSchemaUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? backlogItemLinkTypeSchemaUrl;
            }

            var dto = _Hydrator.Hydrate<BacklogItemLinkTypeSchemaDto>(model);

            return Created(backlogItemLinkTypeSchemaUrl, dto);
        }

        [HttpPatch("{id}", Name = "UpdateBacklogItemLinkTypeSchema")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemLinkTypeSchemaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, BacklogItemLinkTypeSchemaPatchDto backlogItemLinkTypeSchemaPatchDto)
        {
            if (id != backlogItemLinkTypeSchemaPatchDto.ID)
            {
                return BadRequest();
            }

            BacklogItemLinkTypeSchemaDto dto;
            try
            {
                BacklogItemLinkTypeSchemaModel model = _Hydrator.Hydrate<BacklogItemLinkTypeSchemaModel>(backlogItemLinkTypeSchemaPatchDto);
                model = _BacklogItemLinkTypeSchemaService.Update(model);
                dto = _Hydrator.Hydrate<BacklogItemLinkTypeSchemaDto>(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(BacklogItemLinkTypeSchemaModel)))
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

        [HttpDelete("{id}", Name = "DeleteBacklogItemLinkTypeSchema")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            BacklogItemLinkTypeSchemaModel? model = _BacklogItemLinkTypeSchemaService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _BacklogItemLinkTypeSchemaService.Delete(model);

            return new OkResult();
        }
    }
}