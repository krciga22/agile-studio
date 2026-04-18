using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypes
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "accounts")]
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

            var dto = _Hydrator.Hydrate<BacklogItemLinkTypeDto>(model);
            return Ok(dto);
        }

        [HttpPost(Name = "CreateBacklogItemLinkType")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemLinkTypeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(BacklogItemLinkTypePostDto backlogItemLinkTypePostDto)
        {
            BacklogItemLinkTypeModel model = _Hydrator.Hydrate<BacklogItemLinkTypeModel>(backlogItemLinkTypePostDto);
            model = _BacklogItemLinkTypeService.Create(model);

            string backlogItemLinkTypeUrl = "";
            if (Url != null)
            {
                backlogItemLinkTypeUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? backlogItemLinkTypeUrl;
            }

            var dto = _Hydrator.Hydrate<BacklogItemLinkTypeDto>(model);

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
                BacklogItemLinkTypeModel model = _Hydrator.Hydrate<BacklogItemLinkTypeModel>(backlogItemLinkTypePatchDto);
                model = _BacklogItemLinkTypeService.Update(model);
                dto = _Hydrator.Hydrate<BacklogItemLinkTypeDto>(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(BacklogItemLinkTypeModel)))
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
            BacklogItemLinkTypeModel? model = _BacklogItemLinkTypeService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _BacklogItemLinkTypeService.Delete(model);

            return new OkResult();
        }
    }
}