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
    [MapSubResourceGetCollection(
        ResourceTypes.AccountsAccount,
        ResourceTypes.AccountsBacklogItemTypeSchema,
        "BacklogItemTypeSchemas")]
    [MapSubResourcePost(
        ResourceTypes.AccountsAccount,
        ResourceTypes.AccountsBacklogItemTypeSchema,
        "BacklogItemTypeSchemas")]
    [MapSubResourceGetCollection(
        ResourceTypes.AccountsAccount,
        ResourceTypes.AccountsWorkflow,
        "Workflows")]
    [MapSubResourcePost(
        ResourceTypes.AccountsAccount,
        ResourceTypes.AccountsWorkflow,
        "Workflows")]
    [Authorize]
    public class AccountController : ControllerBase
    {
    }
}