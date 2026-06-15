using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Accounts.Accounts
{
    [ApiController]
    [Route("Accounts/Accounts")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [MapResourceGetCollection(ResourceTypes.AccountsAccount)]
    [MapResourceGet(ResourceTypes.AccountsAccount)]
    [MapSubResourceGetCollection(
        ResourceTypes.AccountsAccount,
        ResourceTypes.ProjectsProject,
        "Projects")]
    [MapSubResourcePost(
        ResourceTypes.AccountsAccount,
        ResourceTypes.ProjectsProject,
        "Projects")]
    [MapSubResourceGetCollection(
        ResourceTypes.AccountsAccount,
        ResourceTypes.AccountsBacklogItemType,
        "BacklogItemTypes")]
    [MapSubResourcePost(
        ResourceTypes.AccountsAccount,
        ResourceTypes.AccountsBacklogItemType,
        "BacklogItemTypes")]
    [Authorize]
    public class AccountController : ControllerBase
    {
    }
}