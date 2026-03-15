using AgileStudioServer.Core.APIs.DTOs;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.CoreFeatures.Projects.Projects
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ResourceController : ControllerBase
    {
        private readonly Hydrator _Hydrator;

        private readonly ResourceService _ResourceService;

        public ResourceController(Hydrator Hydrator, ResourceService resourceService)
        {
            _Hydrator = Hydrator;
            _ResourceService = resourceService;
        }

        [HttpGet("{type}", Name = "GetResources")]
        [ProducesResponseType(typeof(PaginatedResults2Dto<ResourceDto>), StatusCodes.Status200OK)]
        public IActionResult Get(string type, [FromQuery] GetCollectionQueryParams queryParams)
        {
            var serviceContext = new ServiceContext();
            serviceContext.WithGetCollectionQueryParams(queryParams);

            var paginationResults = _ResourceService.GetAll(type, serviceContext);

            PaginatedResults2Dto<object> paginatedResultsDto = PaginatedResults2Dto<object>.FromPaginatedResults(paginationResults);

            return Ok(paginatedResultsDto);
        }
    }
}