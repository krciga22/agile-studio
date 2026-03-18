using AgileStudioServer.Core.APIs.DTOs;
using AgileStudioServer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.CoreFeatures.Resources.Resource
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ResourceController : ControllerBase
    {
        private readonly ResourceService _ResourceService;

        public ResourceController(ResourceService resourceService)
        {
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

        [HttpGet("{type}/{id}", Name = "GetResource")]
        [ProducesResponseType(typeof(ResourceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Get(string type, int id)
        {
            var serviceContext = new ServiceContext();

            var dto = _ResourceService.Get(type, id, serviceContext);
            if(dto == null){
                return NotFound();
            }

            return Ok(dto);
        }

        [HttpPost("{type}", Name = "PostResource")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResourceDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Post(string type, [FromBody] object resourcePostDto)
        {
            try
            {
                var serviceContext = new ServiceContext();
                var dto = _ResourceService.Create(type, resourcePostDto, serviceContext);

                string resourceUrl = "";
                if (Url != null && dto != null)
                {
                    resourceUrl = Url.Action(nameof(Get), new { type = type, id = ((dynamic)dto).ID }) ?? resourceUrl;
                }

                return Created(resourceUrl, dto);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "An unexpected error occurred.");
            }
        }
    }
}