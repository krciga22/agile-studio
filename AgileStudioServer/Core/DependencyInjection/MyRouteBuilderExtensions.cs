using AgileStudioServer.Core.Resources;
using AgileStudioServer.CoreFeatures.Resources.Resource;
using AgileStudioServer.CoreFeatures.Resources.Resource.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json.Nodes;

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
        }

        return app;
    }

    private static void MapResourceGetCollection(
        IEndpointRouteBuilder app, Type controller, 
        MapResourceGetCollectionAttribute attribute)
    {
        IResourceMap resourceMap = GetResourceMap(app, attribute.Type);
        string basePath = GetBasePath(controller);
        string resourceName = GetResourceName(controller);
        Type resourceDtoType = resourceMap.GetResourceDtoType();

        app.MapGet(basePath, (
            ResourceController resourceController,
            HttpContext httpContext,
            [AsParameters] GetCollectionQueryParams queryParams) =>
        {
            return resourceController.GetCollection(httpContext, attribute.Type, queryParams);
        })
        .Produces(200, typeof(IEnumerable<>).MakeGenericType(resourceDtoType))
        .Produces(400, typeof(ProblemDetails))
        .WithTags(resourceName);
    }

    private static void MapResourceGet(
        IEndpointRouteBuilder app, Type controller, 
        MapResourceGetAttribute attribute)
    {
        IResourceMap resourceMap = GetResourceMap(app, attribute.Type);
        string basePath = GetBasePath(controller);
        string resourceName = GetResourceName(controller);
        Type resourceDtoType = resourceMap.GetResourceDtoType();

        app.MapGet(basePath + "/{id}", (
            ResourceController resourceController,
            HttpContext httpContext,
            string id) => {
                var ids = id.Split(',');

                return resourceController.Get(httpContext, attribute.Type, ids);
            })
        .Produces(200, resourceDtoType)
        .Produces(404, typeof(ProblemDetails))
        .WithTags(resourceName);
    }

    private static void MapResourcePost(
        IEndpointRouteBuilder app, Type controller, 
        MapResourcePostAttribute attribute)
    {
        IResourceMap resourceMap = GetResourceMap(app, attribute.Type);
        string basePath = GetBasePath(controller);
        string resourceName = GetResourceName(controller);
        Type resourceDtoType = resourceMap.GetResourceDtoType();
        Type resourceDtoCreateType = resourceMap.GetResourceDtoCreateType();

        app.MapPost(basePath, async (
            [FromServices] ResourceController resourceController,
            HttpContext httpContext) =>
        {
            JsonNode body = await JsonNode.ParseAsync(httpContext.Request.Body)
                    ?? throw new Exception("Failed to parse request body.");

            return resourceController.Post(httpContext, attribute.Type, body);
        })
        .Accepts(resourceDtoCreateType, "application/json")
        .Produces(201, resourceDtoType)
        .Produces(400, typeof(ProblemDetails))
        .WithTags(resourceName);
    }

    private static void MapResourcePatch(
        IEndpointRouteBuilder app, Type controller,
        MapResourcePatchAttribute attribute)
    {
        IResourceMap resourceMap = GetResourceMap(app, attribute.Type);
        string basePath = GetBasePath(controller);
        string resourceName = GetResourceName(controller);
        Type resourceDtoType = resourceMap.GetResourceDtoType();
        Type resourceDtoUpdateType = resourceMap.GetResourceDtoUpdateType();

        app.MapPatch(basePath + "/{id}", async (
            ResourceController resourceController,
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
        .WithTags(resourceName);
    }

    private static void MapResourceDelete(
        IEndpointRouteBuilder app, Type controller,
        MapResourceDeleteAttribute attribute)
    {
        IResourceMap resourceMap = GetResourceMap(app, attribute.Type);
        string basePath = GetBasePath(controller);
        string resourceName = GetResourceName(controller);

        app.MapDelete(basePath + "/{id}", (
            ResourceController resourceController,
            HttpContext httpContext,
            string id) =>
        {
            var ids = id.Split(',');

            return resourceController.Delete(httpContext, attribute.Type, ids);
        })
        .Produces(204)
        .Produces(404, typeof(ProblemDetails))
        .WithTags(resourceName);
    }

    private static IResourceMap GetResourceMap(IEndpointRouteBuilder app, string resourceType)
    {
        IEnumerable<IResourceMap> resourceMaps = app.ServiceProvider.GetServices<IResourceMap>();

        var resourceMap = resourceMaps.FirstOrDefault(r =>
                r.GetResourceType() == resourceType);
        if (resourceMap == null){
            throw new UnsupportedResourceTypeException(resourceType);
        }

        return resourceMap;
    }

    private static string GetBasePath(Type controller)
    {
        return "/" + GetResourceName(controller);
    }

    private static string GetResourceName(Type controller)
    {
        return controller.Name.Replace("Controller", "");
    }
}