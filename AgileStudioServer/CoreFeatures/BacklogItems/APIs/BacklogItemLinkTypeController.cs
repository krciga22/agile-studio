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
    public class BacklogItemLinkTypeController : ControllerBase
    {
        private readonly BacklogItemLinkTypeService _BacklogItemLinkTypeService;

        private readonly Hydrator _Hydrator;

        public BacklogItemLinkTypeController(
            BacklogItemLinkTypeService dataProvider,
            Hydrator hydrator)
        {
            _BacklogItemLinkTypeService = dataProvider;
            _Hydrator = hydrator;
        }

        [HttpGet("{id}", Name = "GetBacklogItemLinkType")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BacklogItemLinkTypeDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _BacklogItemLinkTypeService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = HydrateBacklogItemLinkTypeDto(model);
            return Ok(dto);
        }

        [HttpPost(Name = "CreateBacklogItemLinkType")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemLinkTypeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(BacklogItemLinkTypePostDto backlogItemLinkTypePostDto)
        {
            BacklogItemLinkType model = HydrateBacklogItemLinkTypeModel(backlogItemLinkTypePostDto);
            model = _BacklogItemLinkTypeService.Create(model);

            string backlogItemLinkTypeUrl = "";
            if (Url != null)
            {
                backlogItemLinkTypeUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? backlogItemLinkTypeUrl;
            }

            var dto = HydrateBacklogItemLinkTypeDto(model);

            return Created(backlogItemLinkTypeUrl, dto);
        }

        [HttpPatch("{id}", Name = "UpdateBacklogItemLinkType")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemLinkTypeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, BacklogItemLinkTypePatchDto backlogItemLinkTypePatchDto)
        {
            if (id != backlogItemLinkTypePatchDto.ID)
            {
                return BadRequest();
            }

            BacklogItemLinkTypeDto dto;
            try
            {
                BacklogItemLinkType model = HydrateBacklogItemLinkTypeModel(backlogItemLinkTypePatchDto);
                model = _BacklogItemLinkTypeService.Update(model);
                dto = HydrateBacklogItemLinkTypeDto(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(BacklogItemLinkType)))
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

        [HttpDelete("{id}", Name = "DeleteBacklogItemLinkType")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            BacklogItemLinkType? model = _BacklogItemLinkTypeService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _BacklogItemLinkTypeService.Delete(model);

            return new OkResult();
        }

        private List<BacklogItemLinkTypeDto> HydrateBacklogItemLinkTypeDtos(List<BacklogItemLinkType> backlogItemLinkTypes, int depth = 1)
        {
            List<BacklogItemLinkTypeDto> dtos = new();

            backlogItemLinkTypes.ForEach(backlogItemLinkType =>
            {
                BacklogItemLinkTypeDto dto = HydrateBacklogItemLinkTypeDto(backlogItemLinkType, depth);
                dtos.Add(dto);
            });

            return dtos;
        }

        private BacklogItemLinkTypeDto HydrateBacklogItemLinkTypeDto(BacklogItemLinkType backlogItemLinkType, int depth = 1)
        {
            return (BacklogItemLinkTypeDto)_Hydrator.Hydrate(
                backlogItemLinkType, typeof(BacklogItemLinkTypeDto), depth
            );
        }

        private BacklogItemLinkType HydrateBacklogItemLinkTypeModel(BacklogItemLinkTypePostDto backlogItemLinkTypePostDto, int depth = 3)
        {
            return (BacklogItemLinkType)_Hydrator.Hydrate(
                backlogItemLinkTypePostDto, typeof(BacklogItemLinkType), depth
            );
        }

        private BacklogItemLinkType HydrateBacklogItemLinkTypeModel(BacklogItemLinkTypePatchDto backlogItemLinkTypePatchDto, int depth = 3)
        {
            return (BacklogItemLinkType)_Hydrator.Hydrate(
                backlogItemLinkTypePatchDto, typeof(BacklogItemLinkType), depth
            );
        }
    }
}