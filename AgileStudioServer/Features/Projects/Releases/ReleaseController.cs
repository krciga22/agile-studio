using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Projects.Releases
{
    [ApiController]
    [Route("Projects/Releases")]
    [ApiExplorerSettings(GroupName = "projects")]
    [MapResourceGet(ResourceTypes.ReleasesRelease)]
    [MapResourcePatch(ResourceTypes.ReleasesRelease)]
    [MapResourceDelete(ResourceTypes.ReleasesRelease)]
    [Authorize]
    public class ReleaseController : ControllerBase
    {

    }
}