using AgileStudioServer.Core.APIs;
using AgileStudioServer.Core.APIs.DTOs;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services;
using AgileStudioServer.CoreFeatures.Resources.Resource.Exceptions;
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
        private readonly Hydrator _Hydrator;

        public ResourceController(ResourceService resourceService, Hydrator hydrator)
        {
            _ResourceService = resourceService;
            _Hydrator = hydrator;
        }

        [HttpGet("{type}", Name = "GetResources")]
        [ProducesResponseType(typeof(PaginatedResults2Dto<ResourceDto>), StatusCodes.Status200OK)]
        public IActionResult Get(string type, [FromQuery] GetCollectionQueryParams queryParams)
        {
            try
            {
                var serviceContext = new ServiceContext();
                serviceContext.WithGetCollectionQueryParams(queryParams);

                IResourceRepository repository = _ResourceService.GetResourceRepository(type);

                var paginationResults = _ResourceService.GetAll(type, serviceContext);
                paginationResults.Items = _Hydrator.HydrateList(
                    paginationResults.Items,
                    repository.GetResourceDtoType(),
                    serviceContext.HydratorDepth);

                var paginatedResultsDto = new PaginatedResults2Dto<object>(paginationResults);

                return Ok(paginatedResultsDto);
            }
            catch(UnsupportedResourceTypeException)
            {
                return NotFound();
            }
        }

        [HttpGet("{type}/{id}", Name = "GetResource")]
        [ProducesResponseType(typeof(ResourceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Get(string type, int id)
        {
            try
            {
                var resourceModel = _ResourceService.Get(type, id);

                IResourceRepository repository = _ResourceService.GetResourceRepository(type);

                var serviceContext = new ServiceContext();

                var resourceDto = _Hydrator.Hydrate(resourceModel,
                    repository.GetResourceDtoType(),
                    serviceContext.HydratorDepth);

                return Ok(resourceDto);
            }
            catch (UnsupportedResourceTypeException)
            {
                return NotFound();
            }
            catch (ResourceNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred.");
            }
        }

        [HttpPost("{type}", Name = "PostResource")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResourceDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Post(string type, [FromBody] object data)
        {
            try
            {
                IResourceRepository repository = _ResourceService.GetResourceRepository(type);

                var createDto = ApiUtilities.GetDtoFromData(data, 
                    repository.GetCreateResourceDtoType());

                var serviceContext = new ServiceContext();

                var createModel = _Hydrator.Hydrate(createDto, 
                    repository.GetResourceModelType(), 
                    serviceContext.HydratorDepth);

                var resourceModel = _ResourceService.Create(type, createModel);

                var resourceDto = _Hydrator.Hydrate(resourceModel,
                    repository.GetResourceDtoType(),
                    serviceContext.HydratorDepth);

                string resourceUrl = "";
                if (Url != null && resourceDto != null)
                {
                    resourceUrl = Url.Action(nameof(Get), new { type = type, id = ((dynamic)resourceDto).ID }) ?? resourceUrl;
                }

                return Created(resourceUrl, resourceDto);
            }
            catch (UnsupportedResourceTypeException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "An unexpected error occurred.");
            }
        }
    }
}