using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypes
{
    [ApiController]
    [Route("Accounts/BacklogItemLinkTypes")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [MapResourceGet(ResourceTypes.AccountsBacklogItemLinkType)]
    [MapResourcePatch(ResourceTypes.AccountsBacklogItemLinkType)]
    [MapResourceDelete(ResourceTypes.AccountsBacklogItemLinkType)]
    [Authorize]
    public class BacklogItemLinkTypeController : ControllerBase
    {
        private readonly BacklogItemLinkTypeService _BacklogItemLinkTypeService;

        private readonly Hydrator _Hydrator;

        public BacklogItemLinkTypeController(
            BacklogItemLinkTypeService dataProvider,
            Hydrator hydrator)
        {
            _BacklogItemLinkTypeService = dataProvider;
            _Hydrator = hydrator;
        }
    }
}