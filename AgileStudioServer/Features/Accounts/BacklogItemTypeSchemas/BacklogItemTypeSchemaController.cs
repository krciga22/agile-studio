using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas
{
    [ApiController]
    [Route("Accounts/BacklogItemTypeSchemas")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [MapResourceGet(ResourceTypes.AccountsBacklogItemTypeSchema)]
    [MapResourcePatch(ResourceTypes.AccountsBacklogItemTypeSchema)]
    [MapResourceDelete(ResourceTypes.AccountsBacklogItemTypeSchema)]
    [MapSubResourceGetCollection(
        ResourceTypes.AccountsBacklogItemTypeSchema,
        ResourceTypes.AccountsBacklogItemTypeSchemaNode,
        "Nodes")]
    [Authorize]
    public class BacklogItemTypeSchemaController : ControllerBase
    {
        
    }
}