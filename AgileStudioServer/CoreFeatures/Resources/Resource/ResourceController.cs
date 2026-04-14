using AgileStudioServer.Core.APIs;
using AgileStudioServer.Core.APIs.DTOs;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Resources;
using AgileStudioServer.Core.Services;
using AgileStudioServer.CoreFeatures.Resources.Resource.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.CoreFeatures.Resources.Resource
{
    public class ResourceController(
        Hydrator hydrator,
        IEnumerable<IResourceMap> resourceMaps,
        ResourceService resourceService
        ) : ControllerBase
    {
        private readonly Hydrator _Hydrator = hydrator;
        private readonly IEnumerable<IResourceMap> _ResourceMaps = resourceMaps;
        private readonly ResourceService _ResourceService = resourceService;

        public IResult GetCollection(HttpContext httpContext, string type, [FromQuery] GetCollectionQueryParams queryParams)
        {
            try
            {
                IResourceMap resourceMap = GetResourceMap(type);

                var serviceContext = GetServiceContext(httpContext);
                serviceContext.WithGetCollectionQueryParams(queryParams);

                var paginationResults = _ResourceService.GetCollection(type, serviceContext);

                paginationResults.Items = _Hydrator.HydrateList(
                    paginationResults.Items,
                    resourceMap.GetResourceDtoType(),
                    serviceContext.HydratorDepth);

                var paginatedResultsDto = new PaginatedResults2Dto<object>(paginationResults);

                return Results.Ok(paginationResults);
            }
            catch(UnsupportedResourceTypeException)
            {
                return Results.NotFound();
            }
        }

        public IResult Get(HttpContext httpContext, string type, object[] id)
        {
            try
            {
                var serviceContext = GetServiceContext(httpContext);

                var resourceModel = _ResourceService.Get(type, id, serviceContext);

                IResourceMap resourceMap = GetResourceMap(type);

                var resourceDto = _Hydrator.Hydrate(resourceModel,
                    resourceMap.GetResourceDtoType(),
                    serviceContext.HydratorDepth);

                return Results.Ok(resourceDto);
            }
            catch (UnsupportedResourceTypeException)
            {
                return Results.NotFound();
            }
            catch (ResourceNotFoundException)
            {
                return Results.NotFound();
            }
            catch (Exception)
            {
                return Results.Problem();
            }
        }

        public IResult Post(HttpContext httpContext, string type, object data)
        {
            try
            {
                IResourceMap resourceMap = GetResourceMap(type);

                var createDto = ApiUtilities.GetDtoFromData(data,
                    resourceMap.GetResourceDtoCreateType());

                var serviceContext = GetServiceContext(httpContext);

                var createModel = _Hydrator.Hydrate(createDto,
                    resourceMap.GetResourceModelType(), 
                    serviceContext.HydratorDepth);

                var resourceModel = _ResourceService.Create(type, createModel, serviceContext);

                var resourceDto = _Hydrator.Hydrate(resourceModel,
                    resourceMap.GetResourceDtoType(),
                    serviceContext.HydratorDepth);

                string resourceUrl = "";
                if (Url != null && resourceDto != null)
                {
                    resourceUrl = Url.Action(nameof(Get), new { type = type, id = ((dynamic)resourceDto).ID }) ?? resourceUrl;
                }

                return Results.Created(resourceUrl, resourceDto);
            }
            catch (UnsupportedResourceTypeException)
            {
                return Results.NotFound();
            }
            catch (Exception)
            {
                return Results.Problem();
            }
        }

        public IResult Patch(HttpContext httpContext, string type, object[] id, [FromBody] object data)
        {
            try
            {
                IResourceMap resourceMap = GetResourceMap(type);

                var patchDto = ApiUtilities.GetDtoFromData(data,
                    resourceMap.GetResourceDtoUpdateType());

                var serviceContext = GetServiceContext(httpContext);

                var patchModel = _Hydrator.Hydrate(patchDto,
                    resourceMap.GetResourceModelType(),
                    serviceContext.HydratorDepth);

                var resourceModel = _ResourceService.Update(type, id, patchModel, serviceContext);

                var resourceDto = _Hydrator.Hydrate(resourceModel,
                    resourceMap.GetResourceDtoType(),
                    serviceContext.HydratorDepth);

                return Results.Ok(resourceDto);
            }
            catch (UnsupportedResourceTypeException)
            {
                return Results.BadRequest();
            }
            catch (ResourceIdentifierMismatchException)
            {
                return Results.BadRequest();
            }
            catch (ResourceNotFoundException)
            {
                return Results.NotFound();
            }
            catch (Exception)
            {
                return Results.Problem();
            }
        }

        public IResult Delete(HttpContext httpContext, string type, object[] id)
        {
            try
            {
                var serviceContext = GetServiceContext(httpContext);

                _ResourceService.Delete(type, id, serviceContext);

                return Results.Ok();
            }
            catch (UnsupportedResourceTypeException)
            {
                return Results.NotFound();
            }
            catch (ResourceNotFoundException)
            {
                return Results.NotFound();
            }
            catch (Exception)
            {
                return Results.Problem();
            }
        }

        private ServiceContext GetServiceContext(HttpContext httpContext)
        {
            return new ServiceContext()
            {
                currentUser = httpContext?.User
            };
        }

        /// <exception cref="UnsupportedResourceTypeException"></exception>
        private IResourceMap GetResourceMap(string type)
        {
            var resourceMap = _ResourceMaps.FirstOrDefault(r =>
                    r.GetResourceType() == type);
            if (resourceMap == null)
            {
                throw new UnsupportedResourceTypeException(type);
            }

            return resourceMap;
        }
    }
}