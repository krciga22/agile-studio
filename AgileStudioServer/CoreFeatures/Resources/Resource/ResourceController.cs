using AgileStudioServer.Core.APIs;
using AgileStudioServer.Core.APIs.DTOs;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Resources;
using AgileStudioServer.Core.Services;
using AgileStudioServer.CoreFeatures.Resources.Resource.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AgileStudioServer.CoreFeatures.Resources.Resource
{
    public class ResourceController(
        Hydrator hydrator,
        IEnumerable<IResourceMap> resourceMaps,
        IEnumerable<IModelService> modelServices,
        ServiceContext serviceContext
        ) : ControllerBase
    {
        private readonly Hydrator _Hydrator = hydrator;
        private readonly IEnumerable<IResourceMap> _ResourceMaps = resourceMaps;
        private readonly IEnumerable<IModelService> _ModelServices = modelServices;
        private readonly ServiceContext _ServiceContext = serviceContext;

        public IResult GetCollection(HttpContext httpContext, string type, [FromQuery] GetCollectionQueryParams queryParams)
        {
            try
            {
                IResourceMap resourceMap = GetResourceMap(type);
                IModelService resourceService = GetResourceService(type);

                object? result = (resourceService.GetType().GetMethod("GetCollection")?.Invoke(resourceService, [])) ??
                    throw new Exception($"Failed to get resource collection of type {type}.");

                var resultType = result.GetType();
                var itemsProp = resultType.GetProperty("Items");
                var totalProp = resultType.GetProperty("Total");
                var pageProp = resultType.GetProperty("Page");
                var itemsPerPageProp = resultType.GetProperty("ItemsPerPage");

                var items = itemsProp == null ? [] : 
                    ((IEnumerable<object>)itemsProp.GetValue(result)!).Cast<object>().ToList();

                int total = totalProp == null ? 0 : 
                    (int)totalProp.GetValue(result)!;

                int page = pageProp == null ? 0 : 
                    (int)pageProp.GetValue(result)!;

                int itemsPerPage = itemsPerPageProp == null ? 
                    Constants.ItemsPerPage : (int)itemsPerPageProp.GetValue(result)!;

                var paginationResults = new PaginationResults<object>(items, total, page, itemsPerPage);

                paginationResults.Items = _Hydrator.HydrateList(
                    paginationResults.Items,
                    resourceMap.GetResourceDtoType(),
                    _ServiceContext.HydratorDepth);

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
                IResourceMap resourceMap = GetResourceMap(type);
                IModelService resourceService = GetResourceService(type);

                var identifier = resourceService.GetType().GetMethod("ToIdentifier")?.Invoke(resourceService, [id]) ??
                    throw new Exception($"Failed to convert identifier for resource of type {type}.");

                object? resourceModel = (resourceService.GetType().GetMethod("Get")?.Invoke(resourceService, [identifier])) ??
                    throw new ResourceNotFoundException(type, id);

                var resourceDto = _Hydrator.Hydrate(resourceModel,
                    resourceMap.GetResourceDtoType(),
                    _ServiceContext.HydratorDepth);

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

        public IResult Post(HttpContext httpContext, string type, object data, IUrlHelper url)
        {
            try
            {
                IModelService resourceService = GetResourceService(type);
                IResourceMap resourceMap = GetResourceMap(type);

                var createDto = ApiUtilities.GetDtoFromData(data,
                    resourceMap.GetResourceDtoCreateType());

                var createModel = _Hydrator.Hydrate(createDto,
                    resourceMap.GetResourceModelType(),
                    _ServiceContext.HydratorDepth);

                object? resourceModel = (resourceService.GetType().GetMethod("Create")?.Invoke(resourceService, [createModel])) ??
                    throw new Exception($"Failed to create resource of type {type}.");

                var resourceDto = _Hydrator.Hydrate(resourceModel,
                    resourceMap.GetResourceDtoType(),
                    _ServiceContext.HydratorDepth);

                // todo get resource url working
                string resourceUrl = "";
                if (url != null && resourceDto != null){
                    resourceUrl = url.Action($"GetResource/{type}", new { id = ((dynamic)resourceDto).ID }) ?? resourceUrl;
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
                IModelService resourceService = GetResourceService(type);
                IResourceMap resourceMap = GetResourceMap(type);

                var identifier = resourceService.GetType().GetMethod("ToIdentifier")?.Invoke(resourceService, [id]) ??
                    throw new Exception($"Failed to convert identifier for resource of type {type}.");

                object? updateModel = (resourceService.GetType().GetMethod("Get")?.Invoke(resourceService, [identifier])) ??
                    throw new ResourceNotFoundException(type, id);

                var patchDto = ApiUtilities.GetDtoFromData(data,
                    resourceMap.GetResourceDtoUpdateType());

                _Hydrator.Hydrate(patchDto, updateModel, _ServiceContext.HydratorDepth);

                object? resourceModel = (resourceService.GetType().GetMethod("Update")?.Invoke(resourceService, [updateModel])) ??
                    throw new Exception($"Failed to update resource of type {type}.");

                var resourceDto = _Hydrator.Hydrate(resourceModel,
                    resourceMap.GetResourceDtoType(),
                    _ServiceContext.HydratorDepth);

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
                IModelService resourceService = GetResourceService(type);

                var identifier = resourceService.GetType().GetMethod("ToIdentifier")?.Invoke(resourceService, [id]) ??
                    throw new Exception($"Failed to convert identifier for resource of type {type}.");

                object? model = (resourceService.GetType().GetMethod("Get")?.Invoke(resourceService, [identifier])) ??
                    throw new ResourceNotFoundException(type, id);

                resourceService.GetType().GetMethod("Delete")?.Invoke(resourceService, [model]);

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

        public IModelService GetResourceService(string type)
        {
            var resourceMap = GetResourceMap(type);

            IModelService? modelService = _ModelServices.FirstOrDefault(
                repo => repo.GetType() == resourceMap.GetResourceServiceType());

            if (modelService == null){
                throw new Exception("");
            }

            return modelService;
        }
    }
}