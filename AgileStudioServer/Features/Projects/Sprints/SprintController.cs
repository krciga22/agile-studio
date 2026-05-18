using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Projects.Sprints
{
    [ApiController]
    [Route("Projects/Sprints")]
    [ApiExplorerSettings(GroupName = "projects")]
    [MapResourceGet(ResourceTypes.SprintsSprint)]
    [MapResourcePatch(ResourceTypes.SprintsSprint)]
    [MapResourceDelete(ResourceTypes.SprintsSprint)]
    [Authorize]
    public class SprintController : ControllerBase
    {
        
    }
}