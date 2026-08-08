using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas
{
    [ApiController]
    [Route("Accounts/BacklogItemLinkTypeSchemas")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [MapResourceGet(ResourceTypes.AccountsBacklogItemLinkTypeSchema)]
    [MapResourcePatch(ResourceTypes.AccountsBacklogItemLinkTypeSchema)]
    [MapResourceDelete(ResourceTypes.AccountsBacklogItemLinkTypeSchema)]
    [Authorize]
    public class BacklogItemLinkTypeSchemaController : ControllerBase
    {
        private readonly BacklogItemLinkTypeSchemaService _BacklogItemLinkTypeSchemaService;

        private readonly Hydrator _Hydrator;

        public BacklogItemLinkTypeSchemaController(
            BacklogItemLinkTypeSchemaService dataProvider,
            Hydrator hydrator)
        {
            _BacklogItemLinkTypeSchemaService = dataProvider;
            _Hydrator = hydrator;
        }
    }
}