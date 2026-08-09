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
    [MapSubResourceGetCollection(
        ResourceTypes.BacklogItemsBacklogItem,
        ResourceTypes.ProjectsBacklogItemStatus,
        "Statuses")]
    [MapSubResourcePost(
        ResourceTypes.BacklogItemsBacklogItem,
        ResourceTypes.ProjectsBacklogItemStatus,
        "Statuses")]
    [Authorize]
    public class BacklogItemController : ControllerBase
    {

    }
}