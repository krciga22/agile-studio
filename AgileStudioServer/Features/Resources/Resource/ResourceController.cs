using AgileStudioServer.Core.APIs;
using AgileStudioServer.Core.APIs.DTOs;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Resources;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Resources.Resource.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Resources.Resource
{
    public class ResourceController(
        Hydrator hydrator,
        IEnumerable<IResourceMap> resourceMaps,
        IEnumerable<IModelService> modelServices,
        ServiceContext serviceContext,
        PermissionCheckerService permissionCheckerService
        ) : ControllerBase
    {
        private readonly Hydrator _Hydrator = hydrator;
        private readonly IEnumerable<IResourceMap> _ResourceMaps = resourceMaps;
        private readonly IEnumerable<IModelService> _ModelServices = modelServices;
        private readonly ServiceContext _ServiceContext = serviceContext;
        private readonly PermissionCheckerService _PermissionCheckerService = permissionCheckerService;

        public IResult GetCollection(HttpContext httpContext, string type, [FromQuery] GetCollectionQueryParams queryParams)
        {
            try
            {
                IResourceMap resourceMap = ResourceUtil.GetResourceMap(_ResourceMaps, type);
                IModelService resourceService = ResourceUtil.GetModelService(_ModelServices, resourceMap);

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
                return Results.BadRequest();
            }
        }

        public IResult GetSubCollection(
            HttpContext httpContext,
            string childType, 
            string parentType, 
            object[] parentId, 
            [FromQuery] GetCollectionQueryParams queryParams)
        {
            try
            {
                IResourceMap parentTypeResourceMap = ResourceUtil.GetResourceMap(_ResourceMaps, parentType);
                IModelService parentTypeResourceService = ResourceUtil.GetModelService(_ModelServices, parentTypeResourceMap);

                IResourceMap childTypeResourceMap = ResourceUtil.GetResourceMap(_ResourceMaps, childType);
                IModelService childTypeResourceService = ResourceUtil.GetModelService(_ModelServices, childTypeResourceMap);

                var parentIdentifier = parentTypeResourceService.GetType().GetMethod("ToIdentifier")?.Invoke(parentTypeResourceService, [parentId]) ??
                    throw new Exception($"Failed to convert identifier for resource of type {parentType}.");

                int userId = _ServiceContext.GetCurrentUserIdStrict();
                string parentScope = parentTypeResourceMap.GetResourcePermissionScope();
                string childScope = childTypeResourceMap.GetResourcePermissionScope();

                _PermissionCheckerService.ValidatePermissions(
                    RoleSubjectTypes.USER, userId.ToString(),
                    PermissionKeys.LIST, childScope,
                    parentScope, parentIdentifier.ToString()
                );

                object? result = (childTypeResourceService.GetType().GetMethod("GetSubCollection")?.Invoke(childTypeResourceService, [parentType, parentId])) ??
                    throw new Exception($"Failed to get resource sub collection of type {childType} by parent type {parentType} and parent id {parentId}.");

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
                    childTypeResourceMap.GetResourceDtoType(),
                    _ServiceContext.HydratorDepth);

                var paginatedResultsDto = new PaginatedResults2Dto<object>(paginationResults);

                return Results.Ok(paginationResults);
            }
            catch (UnauthorizedAccessException)
            {
                return Results.Forbid();
            }
            catch (ModelNotFoundException)
            {
                return Results.NotFound();
            }
            catch (UnsupportedResourceTypeException)
            {
                return Results.BadRequest();
            }
        }

        public IResult Get(HttpContext httpContext, string type, object[] id)
        {
            try
            {
                IResourceMap resourceMap = ResourceUtil.GetResourceMap(_ResourceMaps, type);
                IModelService resourceService = ResourceUtil.GetModelService(_ModelServices, resourceMap);

                var identifier = resourceService.GetType().GetMethod("ToIdentifier")?.Invoke(resourceService, [id]) ??
                    throw new Exception($"Failed to convert identifier for resource of type {type}.");

                _PermissionCheckerService.ValidatePermissions(
                    RoleSubjectTypes.USER,
                    _ServiceContext.GetCurrentUserIdStrict().ToString(), 
                    resourceMap.GetResourcePermissionScope(),
                    identifier.ToString(),
                    PermissionKeys.READ
                );

                object? resourceModel = (resourceService.GetType().GetMethod("Get")?.Invoke(resourceService, [identifier])) ??
                    throw new ResourceNotFoundException(type, id);

                var resourceDto = _Hydrator.Hydrate(resourceModel,
                    resourceMap.GetResourceDtoType(),
                    _ServiceContext.HydratorDepth);

                return Results.Ok(resourceDto);
            }
            catch (UnsupportedResourceTypeException)
            {
                return Results.BadRequest();
            }
            catch (UnauthorizedAccessException)
            {
                return Results.Forbid();
            }
            catch (ModelNotFoundException)
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
                IResourceMap resourceMap = ResourceUtil.GetResourceMap(_ResourceMaps, type);
                IModelService resourceService = ResourceUtil.GetModelService(_ModelServices, resourceMap);

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
                return Results.BadRequest();
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
                IResourceMap resourceMap = ResourceUtil.GetResourceMap(_ResourceMaps, type);
                IModelService resourceService = ResourceUtil.GetModelService(_ModelServices, resourceMap);

                var identifier = resourceService.GetType().GetMethod("ToIdentifier")?.Invoke(resourceService, [id]) ??
                    throw new Exception($"Failed to convert identifier for resource of type {type}.");

                _PermissionCheckerService.ValidatePermissions(
                    RoleSubjectTypes.USER,
                    _ServiceContext.GetCurrentUserIdStrict().ToString(),
                    resourceMap.GetResourcePermissionScope(),
                    identifier.ToString(),
                    PermissionKeys.UPDATE
                );

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
            catch (UnauthorizedAccessException)
            {
                return Results.Forbid();
            }
            catch (ModelNotFoundException)
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

        public IResult Delete(HttpContext httpContext, string type, object[] id)
        {
            try
            {
                IResourceMap resourceMap = ResourceUtil.GetResourceMap(_ResourceMaps, type);
                IModelService resourceService = ResourceUtil.GetModelService(_ModelServices, resourceMap);

                var identifier = resourceService.GetType().GetMethod("ToIdentifier")?.Invoke(resourceService, [id]) ??
                    throw new Exception($"Failed to convert identifier for resource of type {type}.");

                _PermissionCheckerService.ValidatePermissions(
                    RoleSubjectTypes.USER,
                    _ServiceContext.GetCurrentUserIdStrict().ToString(),
                    resourceMap.GetResourcePermissionScope(),
                    identifier.ToString(),
                    PermissionKeys.DELETE
                );

                object? model = (resourceService.GetType().GetMethod("Get")?.Invoke(resourceService, [identifier])) ??
                    throw new ResourceNotFoundException(type, id);

                resourceService.GetType().GetMethod("Delete")?.Invoke(resourceService, [model]);

                return Results.Ok();
            }
            catch (UnsupportedResourceTypeException)
            {
                return Results.BadRequest();
            }
            catch (UnauthorizedAccessException)
            {
                return Results.Forbid();
            }
            catch (ModelNotFoundException)
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
    }
}