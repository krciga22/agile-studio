using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes
{
    [ApiController]
    [Route("Accounts/BacklogItemTypeSchemaNodes")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [MapResourceGet(ResourceTypes.AccountsBacklogItemTypeSchemaNode)]
    [MapResourceDelete(ResourceTypes.AccountsBacklogItemTypeSchemaNode)]
    [Authorize]
    public class BacklogItemTypeSchemaNodeController : ControllerBase
    {
        
    }
}