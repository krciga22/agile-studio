using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemaEntries
{
    [ApiController]
    [Route("Accounts/BacklogItemLinkTypeSchemaEntries")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [MapResourceGet(ResourceTypes.AccountsBacklogItemLinkTypeSchemaEntry)]
    [MapResourceDelete(ResourceTypes.AccountsBacklogItemLinkTypeSchemaEntry)]
    [Authorize]
    public class BacklogItemLinkTypeSchemaEntryController : ControllerBase
    {
        private readonly BacklogItemLinkTypeSchemaEntryService _BacklogItemLinkTypeSchemaEntryService;

        private readonly Hydrator _Hydrator;

        public BacklogItemLinkTypeSchemaEntryController(
            BacklogItemLinkTypeSchemaEntryService dataProvider,
            Hydrator hydrator)
        {
            _BacklogItemLinkTypeSchemaEntryService = dataProvider;
            _Hydrator = hydrator;
        }
    }
}