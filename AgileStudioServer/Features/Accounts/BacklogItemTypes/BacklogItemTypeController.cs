using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEntries;
using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypes
{
    [ApiController]
    [Route("Accounts/BacklogItemTypes")]
    [ApiExplorerSettings(GroupName = "accounts")]
    [MapResourceGet(ResourceTypes.AccountsBacklogItemType)]
    [MapResourcePatch(ResourceTypes.AccountsBacklogItemType)]
    [MapResourceDelete(ResourceTypes.AccountsBacklogItemType)]
    [Authorize]
    public class BacklogItemTypeController : ControllerBase
    {
        private readonly BacklogItemTypeService _BacklogItemTypeService;

        private readonly BacklogItemTypeSchemaEntryService _BacklogItemTypeSchemaEntryService;

        private readonly BacklogItemTypeSchemaService _BacklogItemTypeSchemaService;

        private readonly Hydrator _Hydrator;

        public BacklogItemTypeController(
            BacklogItemTypeService dataProvider,
            Hydrator hydrator,
            BacklogItemTypeSchemaEntryService backlogItemTypeSchemaEntryService,
            BacklogItemTypeSchemaService backlogItemTypeSchemaService)
        {
            _BacklogItemTypeService = dataProvider;
            _Hydrator = hydrator;
            _BacklogItemTypeSchemaEntryService = backlogItemTypeSchemaEntryService;
            _BacklogItemTypeSchemaService = backlogItemTypeSchemaService;
        }
    }
}