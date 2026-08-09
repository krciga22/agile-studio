using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Projects.BacklogItemStatuses
{
    [ApiController]
    [Route("Projects/BacklogItemStatuses")]
    [ApiExplorerSettings(GroupName = "projects")]
    [MapResourceGet(ResourceTypes.ProjectsBacklogItemStatus)]
    [Authorize]
    public class BacklogItemStatusController : ControllerBase
    {

    }
}