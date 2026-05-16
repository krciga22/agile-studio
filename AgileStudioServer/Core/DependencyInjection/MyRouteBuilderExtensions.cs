using AgileStudioServer.Core.APIs;
using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Resources.Resource.Exceptions;
using AgileStudioServer.Features.Resources.Resource;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;

public static class MyRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapResourceControllers(this IEndpointRouteBuilder app)
    {
        IEnumerable<IResourceMap> resourceMaps = app.ServiceProvider.GetServices<IResourceMap>();

        var controllersWithGetAttribute = Assembly.GetExecutingAssembly()
            .DefinedTypes.Where(t =>
                t.IsSubclassOf(typeof(ControllerBase)) &&
                t.IsPublic && !t.IsAbstract);

        foreach (Type controller in controllersWithGetAttribute)
        {
            var getCollectionAttribute = controller.GetCustomAttribute<MapResourceGetCollectionAttribute>();
            if (getCollectionAttribute != null){
                MapResourceGetCollection(app, controller, getCollectionAttribute);
            }

            IEnumerable<MapSubResourceGetCollectionAttribute> getSubCollectionAttributes = 
                controller.GetCustomAttributes<MapSubResourceGetCollectionAttribute>();
            if (getSubCollectionAttributes.Any()){
                foreach (var getSubCollectionAttribute in getSubCollectionAttributes){
                    MapSubResourceGetCollection(app, controller, getSubCollectionAttribute);
                }
            }

            var getAttribute = controller.GetCustomAttribute<MapResourceGetAttribute>();
            if (getAttribute != null){
                MapResourceGet(app, controller, getAttribute);
            }

            var postAttribute = controller.GetCustomAttribute<MapResourcePostAttribute>();
            if (postAttribute != null){
                MapResourcePost(app, controller, postAttribute);
            }

            var patchAttribute = controller.GetCustomAttribute<MapResourcePatchAttribute>();
            if (patchAttribute != null){
                MapResourcePatch(app, controller, patchAttribute);
            }

            var deleteAttribute = controller.GetCustomAttribute<MapResourceDeleteAttribute>();
            if (deleteAttribute != null){
                MapResourceDelete(app, controller, deleteAttribute);
            }

            IEnumerable<MapSubResourcePostAttribute> subResourcePostAttributes =
                controller.GetCustomAttributes<MapSubResourcePostAttribute>();
            if (subResourcePostAttributes.Any()){
                foreach (var subResourcePostAttribute in subResourcePostAttributes){
                    MapSubResourcePost(app, controller, subResourcePostAttribute);
                }
            }
        }

        return app;
    }

    private static void MapResourceGetCollection(
        IEndpointRouteBuilder app, Type controller, 
        MapResourceGetCollectionAttribute attribute)
    {
        IEnumerable<IResourceMap> resourceMaps = app.ServiceProvider.GetServices<IResourceMap>();
        IResourceMap resourceMap = ResourceUtil.GetResourceMap(resourceMaps, attribute.Type);
        string basePath = GetBasePath(controller);
        string resourceName = GetResourceName(controller);
        string groupName = GetGroupName(controller);
        Type resourceDtoType = resourceMap.GetResourceDtoType();

        var routeHandlerBuilder = app.MapGet(basePath, (
            [FromServices] ResourceController resourceController,
            [AsParameters] GetCollectionQueryParams queryParams,
            HttpContext httpContext) =>
        {
            return resourceController.GetCollection(httpContext, attribute.Type, queryParams);
        })
        .Produces(200, typeof(IEnumerable<>).MakeGenericType(resourceDtoType))
        .Produces(400, typeof(ProblemDetails))
        .WithTags(resourceName)
        .WithName($"GetResourceCollection/{attribute.Type}")
        .WithGroupName(groupName);

        RequireAuthorizationIfSpecified(routeHandlerBuilder, controller);
    }

    private static void MapSubResourceGetCollection(
        IEndpointRouteBuilder app, Type controller,
        MapSubResourceGetCollectionAttribute attribute)
    {
        IEnumerable<IResourceMap> resourceMaps = app.ServiceProvider.GetServices<IResourceMap>();
        IResourceMap subResourceMap = ResourceUtil.GetResourceMap(resourceMaps, attribute.SubType);

        string path = GetBasePath(controller) + "/{id}/" + attribute.Path;
        string resourceName = GetResourceName(controller);
        string groupName = GetGroupName(controller);
        Type subResourceDtoType = subResourceMap.GetResourceDtoType();

        var routeHandlerBuilder = app.MapGet(path, (
            [FromServices] ResourceController resourceController,
            [AsParameters] GetCollectionQueryParams queryParams,
            HttpContext httpContext,
            string id) =>
        {
            string[] compositeId = id.Split(',');
            return resourceController.GetSubCollection(httpContext, attribute.SubType, attribute.Type, compositeId, queryParams);
        })
        .Produces(200, typeof(IEnumerable<>).MakeGenericType(subResourceDtoType))
        .Produces(400, typeof(ProblemDetails))
        .Produces(403, typeof(ProblemDetails))
        .Produces(404, typeof(ProblemDetails))
        .WithTags(resourceName)
        .WithName($"GetSubResourceCollection/{attribute.Type}/{attribute.SubType}")
        .WithGroupName(groupName);

        RequireAuthorizationIfSpecified(routeHandlerBuilder, controller);
    }

    private static void MapResourceGet(
        IEndpointRouteBuilder app, Type controller, 
        MapResourceGetAttribute attribute)
    {
        IEnumerable<IResourceMap> resourceMaps = app.ServiceProvider.GetServices<IResourceMap>();
        IResourceMap resourceMap = ResourceUtil.GetResourceMap(resourceMaps, attribute.Type);
        string basePath = GetBasePath(controller);
        string resourceName = GetResourceName(controller);
        string groupName = GetGroupName(controller);
        Type resourceDtoType = resourceMap.GetResourceDtoType();

        var routeHandlerBuilder = app.MapGet(basePath + "/{id}", (
            [FromServices] ResourceController resourceController,
            HttpContext httpContext,
            string id) =>
        {
            var ids = id.Split(',');

            return resourceController.Get(httpContext, attribute.Type, ids);
        })
        .Produces(200, resourceDtoType)
        .Produces(404, typeof(ProblemDetails))
        .WithTags(resourceName)
        .WithName($"GetResource/{attribute.Type}")
        .WithGroupName(groupName);

        RequireAuthorizationIfSpecified(routeHandlerBuilder, controller);
    }

    private static void MapResourcePost(
        IEndpointRouteBuilder app, Type controller, 
        MapResourcePostAttribute attribute)
    {
        IEnumerable<IResourceMap> resourceMaps = app.ServiceProvider.GetServices<IResourceMap>();
        IResourceMap resourceMap = ResourceUtil.GetResourceMap(resourceMaps, attribute.Type);
        string basePath = GetBasePath(controller);
        string resourceName = GetResourceName(controller);
        string groupName = GetGroupName(controller);
        Type resourceDtoType = resourceMap.GetResourceDtoType();
        Type resourceDtoCreateType = resourceMap.GetResourceDtoCreateType();

        var routeHandlerBuilder = app.MapPost(basePath, async (
            [FromServices] ResourceController resourceController,
            [FromServices] IUrlHelperFactory urlHelperFactory,
            HttpContext httpContext
        ) =>
        {
            JsonNode body = await JsonNode.ParseAsync(httpContext.Request.Body)
                    ?? throw new Exception("Failed to parse request body.");

            var urlHelper = urlHelperFactory.GetUrlHelper(new ActionContext
            {
                HttpContext = httpContext,
                RouteData = httpContext.GetRouteData(),
                ActionDescriptor = new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()
            });

            return resourceController.Post(httpContext, attribute.Type, body, urlHelper);
        })
        .Accepts(resourceDtoCreateType, "application/json")
        .Produces(201, resourceDtoType)
        .Produces(400, typeof(ProblemDetails))
        .WithTags(resourceName)
        .WithName($"PostResource/{attribute.Type}")
        .WithGroupName(groupName);

        RequireAuthorizationIfSpecified(routeHandlerBuilder, controller);
    }

    private static void MapResourcePatch(
        IEndpointRouteBuilder app, Type controller,
        MapResourcePatchAttribute attribute)
    {
        IEnumerable<IResourceMap> resourceMaps = app.ServiceProvider.GetServices<IResourceMap>();
        IResourceMap resourceMap = ResourceUtil.GetResourceMap(resourceMaps, attribute.Type);
        string basePath = GetBasePath(controller);
        string resourceName = GetResourceName(controller);
        string groupName = GetGroupName(controller);
        Type resourceDtoType = resourceMap.GetResourceDtoType();
        Type resourceDtoUpdateType = resourceMap.GetResourceDtoUpdateType();

        var routeHandlerBuilder = app.MapPatch(basePath + "/{id}", async (
            [FromServices] ResourceController resourceController,
            HttpContext httpContext,
            string id) => {
                var ids = id.Split(',');

                JsonNode body = await JsonNode.ParseAsync(httpContext.Request.Body) 
                    ?? throw new Exception("Failed to parse request body.");

                return resourceController.Patch(httpContext, attribute.Type, ids, body);
            })
        .Accepts(resourceDtoUpdateType, "application/json")
        .Produces(200, resourceDtoType)
        .Produces(400, typeof(ProblemDetails))
        .Produces(404, typeof(ProblemDetails))
        .WithTags(resourceName)
        .WithName($"PatchResource/{attribute.Type}")
        .WithGroupName(groupName);

        RequireAuthorizationIfSpecified(routeHandlerBuilder, controller);
    }

    private static void MapResourceDelete(
        IEndpointRouteBuilder app, Type controller,
        MapResourceDeleteAttribute attribute)
    {
        IEnumerable<IResourceMap> resourceMaps = app.ServiceProvider.GetServices<IResourceMap>();
        IResourceMap resourceMap = ResourceUtil.GetResourceMap(resourceMaps, attribute.Type);
        string basePath = GetBasePath(controller);
        string resourceName = GetResourceName(controller);
        string groupName = GetGroupName(controller);

        var routeHandlerBuilder = app.MapDelete(basePath + "/{id}", (
            [FromServices] ResourceController resourceController,
            HttpContext httpContext,
            string id) =>
        {
            var ids = id.Split(',');

            return resourceController.Delete(httpContext, attribute.Type, ids);
        })
        .Produces(204)
        .Produces(404, typeof(ProblemDetails))
        .WithTags(resourceName)
        .WithName($"DeleteResource/{attribute.Type}")
        .WithGroupName(groupName);

        RequireAuthorizationIfSpecified(routeHandlerBuilder, controller);
    }

    private static void MapSubResourcePost(
        IEndpointRouteBuilder app, Type controller,
        MapSubResourcePostAttribute attribute)
    {
        IEnumerable<IResourceMap> resourceMaps = app.ServiceProvider.GetServices<IResourceMap>();
        IResourceMap subResourceMap = ResourceUtil.GetResourceMap(resourceMaps, attribute.SubType);

        string path = GetBasePath(controller) + "/{id}/" + attribute.Path;
        string resourceName = GetResourceName(controller);
        string groupName = GetGroupName(controller);
        Type subResourceDtoCreateType = subResourceMap.GetResourceDtoCreateType();
        Type subResourceDtoType = subResourceMap.GetResourceDtoType();

        var routeHandlerBuilder = app.MapPost(path, async (
            [FromServices] ResourceController resourceController,
            [FromServices] IUrlHelperFactory urlHelperFactory,
            HttpContext httpContext,
            string id
        ) =>
        {
            string[] compositeId = id.Split(',');

            JsonNode body = await JsonNode.ParseAsync(httpContext.Request.Body)
                    ?? throw new Exception("Failed to parse request body.");

            var urlHelper = urlHelperFactory.GetUrlHelper(new ActionContext
            {
                HttpContext = httpContext,
                RouteData = httpContext.GetRouteData(),
                ActionDescriptor = new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()
            });

            return resourceController.PostSub(httpContext, attribute.SubType, 
                body, attribute.Type, compositeId, urlHelper);
        })
        .Accepts(subResourceDtoCreateType, "application/json")
        .Produces(201, subResourceDtoType)
        .Produces(400, typeof(ProblemDetails))
        .Produces(403, typeof(ProblemDetails))
        .Produces(404, typeof(ProblemDetails))
        .WithTags(resourceName)
        .WithName($"PostSubResource/{attribute.Type}/{attribute.SubType}")
        .WithGroupName(groupName);

        RequireAuthorizationIfSpecified(routeHandlerBuilder, controller);
    }

    /// <summary>
    /// Reuire authorization for the route if the controller 
    /// has an AuthorizeAttribute.
    /// </summary>
    private static void RequireAuthorizationIfSpecified(RouteHandlerBuilder routeHandlerBuilder, Type controller)
    {
        var authorizeAttribute = controller.GetCustomAttribute<AuthorizeAttribute>();
        if (authorizeAttribute != null){
            routeHandlerBuilder.RequireAuthorization(authorizeAttribute);
        }
    }

    private static string GetBasePath(Type controller)
    {
        var basePath = "";

        var routeAttribute = controller.GetCustomAttribute<RouteAttribute>();
        if (routeAttribute != null){
            var resourceName = GetResourceName(controller);
            basePath = routeAttribute.Template.Replace("[controller]", resourceName) ?? "";
        }

        if (String.IsNullOrEmpty(basePath)){
            basePath = "/" + GetResourceName(controller);
        }

        return basePath;
    }

    private static string GetResourceName(Type controller)
    {
        return controller.Name.Replace("Controller", "");
    }

    private static string GetGroupName(Type controller)
    {
        var apiExplorerSettingsAttribute = controller.GetCustomAttribute<ApiExplorerSettingsAttribute>();
        if (apiExplorerSettingsAttribute != null){
            return apiExplorerSettingsAttribute.GroupName ?? "";
        }

        return "";
    }
}