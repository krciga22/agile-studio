using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges
{
    [ApiController]
    [Route("Accounts/BacklogItemTypeSchemaEdges")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [MapResourceGet(ResourceTypes.AccountsBacklogItemTypeSchemaEdge)]
    [MapResourceDelete(ResourceTypes.AccountsBacklogItemTypeSchemaEdge)]
    [Authorize]
    public class BacklogItemTypeSchemaEdgeController : ControllerBase
    {
        
    }
}