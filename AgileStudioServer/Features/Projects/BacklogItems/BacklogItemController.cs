using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Projects.BacklogItems
{
    [ApiController]
    [Route("Projects/BacklogItems")]
    [ApiExplorerSettings(GroupName = "projects")]
    [MapResourceGet(ResourceTypes.BacklogItemsBacklogItem)]
    [MapResourcePatch(ResourceTypes.BacklogItemsBacklogItem)]
    [MapResourceDelete(ResourceTypes.BacklogItemsBacklogItem)]
    [MapSubResourceGetCollection(
        ResourceTypes.BacklogItemsBacklogItem,
        ResourceTypes.BacklogItemsBacklogItem,
        "Children")]
    [Authorize]
    public class BacklogItemController : ControllerBase
    {
        private readonly BacklogItemService _BacklogItemService;

        private readonly Hydrator _Hydrator;

        public BacklogItemController(BacklogItemService dataProvider, Hydrator hydrator)
        {
            _BacklogItemService = dataProvider;
            _Hydrator = hydrator;
        }

        [HttpGet("{id}/Parent", Name = "GetParentBacklogItem")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BacklogItemDto), StatusCodes.Status200OK)]
        public IActionResult GetParentBacklogItem(int id)
        {
            var model = _BacklogItemService.GetParentBacklogItem(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = _Hydrator.Hydrate<BacklogItemDto>(model);
            return Ok(dto);
        }
    }
}