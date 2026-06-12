using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Accounts.AccountTypes
{
    [ApiController]
    [Route("Accounts/AccountTypes")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [MapResourceGetCollection(ResourceTypes.AccountsAccountType)]
    [MapResourceGet(ResourceTypes.AccountsAccountType)]
    [Authorize]
    public class AccountTypeController : ControllerBase
    {
    }
}