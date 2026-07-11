using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Accounts.Workflows
{
    [ApiController]
    [Route("Accounts/Workflows")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [MapResourceGet(ResourceTypes.AccountsWorkflow)]
    [MapResourcePatch(ResourceTypes.AccountsWorkflow)]
    [MapResourceDelete(ResourceTypes.AccountsWorkflow)]
    [MapSubResourceGetCollection(
        ResourceTypes.AccountsWorkflow,
        ResourceTypes.AccountsWorkflowState,
        "WorkflowStates")]  
    [MapSubResourcePost(
        ResourceTypes.AccountsWorkflow,
        ResourceTypes.AccountsWorkflowState,
        "WorkflowStates")]
    [Authorize]
    public class WorkflowController : ControllerBase
    {
        
    }
}