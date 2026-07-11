using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Accounts.WorkflowStates
{
    [ApiController]
    [Route("Accounts/WorkflowStates")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [MapResourceGet(ResourceTypes.AccountsWorkflowState)]
    [MapResourcePatch(ResourceTypes.AccountsWorkflowState)]
    [MapResourceDelete(ResourceTypes.AccountsWorkflowState)]
    [Authorize]
    public class WorkflowStateController : ControllerBase
    {
        
    }
}