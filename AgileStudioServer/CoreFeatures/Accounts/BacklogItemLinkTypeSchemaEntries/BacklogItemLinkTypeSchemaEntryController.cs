using AgileStudioServer.Core.Hydrator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypeSchemaEntries
{
    [ApiController]
    [Route("Accounts/[controller]")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [Authorize]
    public class BacklogItemLinkTypeSchemaEntryController : ControllerBase
    {
        private readonly BacklogItemLinkTypeSchemaEntryService _BacklogItemLinkTypeSchemaEntryService;

        private readonly Hydrator _Hydrator;

        public BacklogItemLinkTypeSchemaEntryController(
            BacklogItemLinkTypeSchemaEntryService dataProvider,
            Hydrator hydrator)
        {
            _BacklogItemLinkTypeSchemaEntryService = dataProvider;
            _Hydrator = hydrator;
        }

        [HttpGet("{id}", Name = "GetBacklogItemLinkTypeSchemaEntry")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BacklogItemLinkTypeSchemaEntryDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _BacklogItemLinkTypeSchemaEntryService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = _Hydrator.Hydrate<BacklogItemLinkTypeSchemaEntryDto>(model);
            return Ok(dto);
        }

        [HttpPost(Name = "CreateBacklogItemLinkTypeSchemaEntry")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemLinkTypeSchemaEntryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(BacklogItemLinkTypeSchemaEntryPostDto backlogItemLinkTypeSchemaEntryPostDto)
        {
            BacklogItemLinkTypeSchemaEntryModel model = _Hydrator.Hydrate<BacklogItemLinkTypeSchemaEntryModel>(backlogItemLinkTypeSchemaEntryPostDto);
            model = _BacklogItemLinkTypeSchemaEntryService.Create(model);

            string backlogItemLinkTypeSchemaEntryUrl = "";
            if (Url != null)
            {
                backlogItemLinkTypeSchemaEntryUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? backlogItemLinkTypeSchemaEntryUrl;
            }

            var dto = _Hydrator.Hydrate<BacklogItemLinkTypeSchemaEntryDto>(model);

            return Created(backlogItemLinkTypeSchemaEntryUrl, dto);
        }

        [HttpDelete("{id}", Name = "DeleteBacklogItemLinkTypeSchemaEntry")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            BacklogItemLinkTypeSchemaEntryModel? model = _BacklogItemLinkTypeSchemaEntryService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _BacklogItemLinkTypeSchemaEntryService.Delete(model);

            return new OkResult();
        }
    }
}