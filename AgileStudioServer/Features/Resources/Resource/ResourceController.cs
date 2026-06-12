using AgileStudioServer.Core.APIs;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Resources;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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

        public IResult GetCollection(
            HttpContext httpContext, string type, 
            [FromQuery] GetCollectionQueryParams queryParams)
        {
            try
            {
                IResourceMap resourceMap = ResourceUtil.GetResourceMap(_ResourceMaps, type);
                IModelService resourceService = ResourceUtil.GetModelService(_ModelServices, resourceMap);

                object result = InvokeResourceServiceMethod(
                    resourceService, "GetCollection", []);

                PaginationResults<object> paginationResults = ToGenericPaginatedResults(result, resourceMap);

                return Results.Ok(paginationResults);
            }
            catch(UnsupportedResourceTypeException)
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

                var identifier = InvokeResourceServiceMethod(
                    resourceService, "ToIdentifier", [id]);

                if (resourceMap.IsPermissionedResource())
                {
                    _PermissionCheckerService.ValidatePermissions(
                        RoleSubjectTypes.USER,
                        _ServiceContext.GetCurrentUserIdStrict().ToString(), 
                        resourceMap.GetResourcePermissionScope(),
                        identifier.ToString(),
                        PermissionKeys.READ
                    );
                }

                object resourceModel = InvokeResourceServiceMethodNullable(
                    resourceService, "Get", [identifier]) ??
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

        public IResult Post(
            HttpContext httpContext, string type, 
            object data, IUrlHelper url)
        {
            try
            {
                IResourceMap resourceMap = ResourceUtil.GetResourceMap(
                    _ResourceMaps, type);
                IModelService resourceService = ResourceUtil.GetModelService(
                    _ModelServices, resourceMap);

                var createDto = ApiUtilities.GetDtoFromData(data,
                    resourceMap.GetResourceDtoCreateType());

                var createModel = _Hydrator.Hydrate(createDto,
                    resourceMap.GetResourceModelType(),
                    _ServiceContext.HydratorDepth);

                object resourceModel = InvokeResourceServiceMethod(
                    resourceService, "Create", [createModel]);

                var resourceDto = _Hydrator.Hydrate(resourceModel,
                    resourceMap.GetResourceDtoType(),
                    _ServiceContext.HydratorDepth);

                string resourceUrl = GetResourceUrl(type, ((dynamic)resourceDto).ID, url);

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

        public IResult Patch(
            HttpContext httpContext, string type, 
            object[] id, [FromBody] object data)
        {
            try
            {
                IResourceMap resourceMap = ResourceUtil.GetResourceMap(
                    _ResourceMaps, type);
                IModelService resourceService = ResourceUtil.GetModelService(
                    _ModelServices, resourceMap);

                var identifier = InvokeResourceServiceMethod(
                    resourceService, "ToIdentifier", [id]);

                if (resourceMap.IsPermissionedResource())
                {
                    _PermissionCheckerService.ValidatePermissions(
                        RoleSubjectTypes.USER,
                        _ServiceContext.GetCurrentUserIdStrict().ToString(),
                        resourceMap.GetResourcePermissionScope(),
                        identifier.ToString(),
                        PermissionKeys.UPDATE
                    );
                }

                object updateModel = InvokeResourceServiceMethodNullable(
                    resourceService, "Get", [identifier]) ??
                    throw new ResourceNotFoundException(type, id);

                var patchDto = ApiUtilities.GetDtoFromData(data,
                    resourceMap.GetResourceDtoUpdateType());

                _Hydrator.Hydrate(patchDto, updateModel, _ServiceContext.HydratorDepth);

                var updatedIdentifier = InvokeResourceServiceMethod(
                    resourceService, "GetIdentifier", [updateModel]);
                if (!updatedIdentifier.Equals(identifier)){
                    throw new ResourceIdentifierMismatchException(id);
                }

                object resourceModel = InvokeResourceServiceMethod(
                    resourceService, "Update", [updateModel]);

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
                IResourceMap resourceMap = ResourceUtil.GetResourceMap(
                    _ResourceMaps, type);
                IModelService resourceService = ResourceUtil.GetModelService(
                    _ModelServices, resourceMap);

                var identifier = InvokeResourceServiceMethod(
                    resourceService, "ToIdentifier", [id]);

                if (resourceMap.IsPermissionedResource())
                {
                    _PermissionCheckerService.ValidatePermissions(
                        RoleSubjectTypes.USER,
                        _ServiceContext.GetCurrentUserIdStrict().ToString(),
                        resourceMap.GetResourcePermissionScope(),
                        identifier.ToString(),
                        PermissionKeys.DELETE
                    );
                }

                object model = InvokeResourceServiceMethodNullable(
                    resourceService, "Get", [identifier]) ??
                    throw new ResourceNotFoundException(type, id);

                InvokeResourceServiceAction(resourceService, "Delete", [model]);

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

        public IResult GetSubCollection(
            HttpContext httpContext, string childType,
            string parentType, object[] parentId,
            [FromQuery] GetCollectionQueryParams queryParams)
        {
            try
            {
                IResourceMap parentTypeResourceMap = ResourceUtil.GetResourceMap(
                    _ResourceMaps, parentType);
                IModelService parentTypeResourceService = ResourceUtil.GetModelService(
                    _ModelServices, parentTypeResourceMap);

                IResourceMap childTypeResourceMap = ResourceUtil.GetResourceMap(
                    _ResourceMaps, childType);
                IModelService childTypeResourceService = ResourceUtil.GetModelService(
                    _ModelServices, childTypeResourceMap);

                var parentIdentifier = InvokeResourceServiceMethod(
                    parentTypeResourceService, "ToIdentifier", [parentId]);

                int userId = _ServiceContext.GetCurrentUserIdStrict();
                string parentScope = parentTypeResourceMap.GetResourcePermissionScope();
                string childScope = childTypeResourceMap.GetResourcePermissionScope();

                _PermissionCheckerService.ValidatePermissions(
                    RoleSubjectTypes.USER, userId.ToString(),
                    PermissionKeys.LIST, childScope,
                    parentScope, parentIdentifier.ToString()
                );

                object result = InvokeResourceServiceMethod(
                    childTypeResourceService, "GetSubCollection", [parentType, parentId]);

                PaginationResults<object> paginationResults = ToGenericPaginatedResults(
                    result, childTypeResourceMap);

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

        public IResult PostSub(
            HttpContext httpContext, string childType,
            object data, string parentType, 
            object[] parentId, IUrlHelper url)
        {
            try
            {
                IResourceMap parentTypeResourceMap = ResourceUtil.GetResourceMap(
                    _ResourceMaps, parentType);
                IModelService parentTypeResourceService = ResourceUtil.GetModelService(
                    _ModelServices, parentTypeResourceMap);

                IResourceMap childTypeResourceMap = ResourceUtil.GetResourceMap(
                    _ResourceMaps, childType);
                IModelService childTypeResourceService = ResourceUtil.GetModelService(
                    _ModelServices, childTypeResourceMap);

                var parentIdentifier = InvokeResourceServiceMethod(
                    parentTypeResourceService, "ToIdentifier", [parentId]);

                int userId = _ServiceContext.GetCurrentUserIdStrict();
                string parentScope = parentTypeResourceMap.GetResourcePermissionScope();
                string childScope = childTypeResourceMap.GetResourcePermissionScope();

                _PermissionCheckerService.ValidatePermissions(
                    RoleSubjectTypes.USER, userId.ToString(),
                    PermissionKeys.CREATE, childScope,
                    parentScope, parentIdentifier.ToString()
                );

                var createDto = ApiUtilities.GetDtoFromData(data,
                    childTypeResourceMap.GetResourceDtoCreateType());

                var createModel = _Hydrator.Hydrate(createDto,
                    childTypeResourceMap.GetResourceModelType(),
                    _ServiceContext.HydratorDepth);

                ParentScope expectedParentScope = childTypeResourceMap.GetParentResourceScope(
                    createModel);
                if(parentScope != expectedParentScope.Scope ||
                    parentIdentifier.ToString() != expectedParentScope.ScopeId){
                    throw new ParentResourceIdentifierMismatchException(parentId);
                }

                object resourceModel = InvokeResourceServiceMethod(
                    childTypeResourceService, "Create", [createModel]);

                var resourceDto = _Hydrator.Hydrate(resourceModel,
                    childTypeResourceMap.GetResourceDtoType(),
                    _ServiceContext.HydratorDepth);

                string resourceUrl = GetResourceUrl(childType, ((dynamic)resourceDto).ID, url);

                return Results.Created(resourceUrl, resourceDto);
            }
            catch (UnsupportedResourceTypeException)
            {
                return Results.BadRequest();
            }
            catch (ParentResourceIdentifierMismatchException)
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
            catch (Exception)
            {
                return Results.Problem();
            }
        }

        private static object InvokeResourceServiceMethod(
            IModelService resourceService, string method,
            object?[]? parameters)
        {
            return GetResourceServiceMethod(resourceService, method)
                    .Invoke(resourceService, parameters) ??
                        throw new Exception(
                            $"Method {method} did not return a value for resource service " +
                            $"{nameof(resourceService)}."
                        );
        }

        private static object? InvokeResourceServiceMethodNullable(
            IModelService resourceService, string method,
            object?[]? parameters)
        {
            return GetResourceServiceMethod(resourceService, method)
                .Invoke(resourceService, parameters);
        }

        private static void InvokeResourceServiceAction(
            IModelService resourceService, string method,
            object?[]? parameters)
        {
            GetResourceServiceMethod(resourceService, method)
                    .Invoke(resourceService, parameters);
        }

        private static MethodInfo GetResourceServiceMethod(
            IModelService resourceService, string method)
        {
            return resourceService.GetType().GetMethod(method) ??
                    throw new NotImplementedException(
                        $"Method {method} is not yet implemented for resource service" +
                        $"{nameof(resourceService)}."
                    );
        }

        /// <summary>
        /// Get the resource url for a given resource type and id.
        /// </summary>
        private static string GetResourceUrl(string type, object id, IUrlHelper url)
        {
            string resourceUrl = "";
            string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == null || !env.Equals("Testing"))
            {
                if (url != null)
                {
                    resourceUrl = url.Action(
                        $"GetResource/{type}",
                        new { id = id }
                    ) ?? resourceUrl;
                }
            }
            return resourceUrl;
        }

        private PaginationResults<object> ToGenericPaginatedResults(
            Object result, IResourceMap resourceMap)
        {
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

           return paginationResults;
        }
    }
}