using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.CoreFeatures.BacklogItems.APIs.DTOs;
using AgileStudioServer.CoreFeatures.BacklogItems.Services;
using AgileStudioServer.CoreFeatures.BacklogItems.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.CoreFeatures.BacklogItems.APIs
{
    [ApiController]
    [Route("[controller]")]
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

            var dto = HydrateBacklogItemLinkTypeSchemaDto(model);
            return Ok(dto);
        }

        [HttpPost(Name = "CreateBacklogItemLinkTypeSchema")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemLinkTypeSchemaDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(BacklogItemLinkTypeSchemaPostDto backlogItemLinkTypeSchemaPostDto)
        {
            BacklogItemLinkTypeSchema model = HydrateBacklogItemLinkTypeSchemaModel(backlogItemLinkTypeSchemaPostDto);
            model = _BacklogItemLinkTypeSchemaService.Create(model);

            string backlogItemLinkTypeSchemaUrl = "";
            if (Url != null)
            {
                backlogItemLinkTypeSchemaUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? backlogItemLinkTypeSchemaUrl;
            }

            var dto = HydrateBacklogItemLinkTypeSchemaDto(model);

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
                BacklogItemLinkTypeSchema model = HydrateBacklogItemLinkTypeSchemaModel(backlogItemLinkTypeSchemaPatchDto);
                model = _BacklogItemLinkTypeSchemaService.Update(model);
                dto = HydrateBacklogItemLinkTypeSchemaDto(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(BacklogItemLinkTypeSchema)))
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
            BacklogItemLinkTypeSchema? model = _BacklogItemLinkTypeSchemaService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _BacklogItemLinkTypeSchemaService.Delete(model);

            return new OkResult();
        }

        private List<BacklogItemLinkTypeSchemaDto> HydrateBacklogItemLinkTypeSchemaDtos(List<BacklogItemLinkTypeSchema> backlogItemLinkTypeSchemas, int depth = 1)
        {
            List<BacklogItemLinkTypeSchemaDto> dtos = new();

            backlogItemLinkTypeSchemas.ForEach(backlogItemLinkTypeSchema =>
            {
                BacklogItemLinkTypeSchemaDto dto = HydrateBacklogItemLinkTypeSchemaDto(backlogItemLinkTypeSchema, depth);
                dtos.Add(dto);
            });

            return dtos;
        }

        private BacklogItemLinkTypeSchemaDto HydrateBacklogItemLinkTypeSchemaDto(BacklogItemLinkTypeSchema backlogItemLinkTypeSchema, int depth = 1)
        {
            return (BacklogItemLinkTypeSchemaDto)_Hydrator.Hydrate(
                backlogItemLinkTypeSchema, typeof(BacklogItemLinkTypeSchemaDto), depth
            );
        }

        private BacklogItemLinkTypeSchema HydrateBacklogItemLinkTypeSchemaModel(BacklogItemLinkTypeSchemaPostDto backlogItemLinkTypeSchemaPostDto, int depth = 3)
        {
            return (BacklogItemLinkTypeSchema)_Hydrator.Hydrate(
                backlogItemLinkTypeSchemaPostDto, typeof(BacklogItemLinkTypeSchema), depth
            );
        }

        private BacklogItemLinkTypeSchema HydrateBacklogItemLinkTypeSchemaModel(BacklogItemLinkTypeSchemaPatchDto backlogItemLinkTypeSchemaPatchDto, int depth = 3)
        {
            return (BacklogItemLinkTypeSchema)_Hydrator.Hydrate(
                backlogItemLinkTypeSchemaPatchDto, typeof(BacklogItemLinkTypeSchema), depth
            );
        }
    }
}