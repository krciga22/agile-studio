using AgileStudioServer.Core.APIs;
using AgileStudioServer.Core.Middleware;

namespace AgileStudioServer.Core.Services;

/// <summary>
/// Populates the default service context.
/// </summary>
public class ServiceContextMiddleware(RequestDelegate next) : AbstractMiddleware
{
    public async Task InvokeAsync(HttpContext httpContext, ServiceContext serviceContext)
    {
        GetCollectionQueryParams getCollectionQueryParams = 
            GetCollectionQueryParams.FromHttpRequest(httpContext.Request);

        if(getCollectionQueryParams != null){
            serviceContext.WithGetCollectionQueryParams(getCollectionQueryParams);
        }

        // add user
        if (httpContext.User?.Identity?.IsAuthenticated == true){
            serviceContext.currentUser = httpContext.User;
        }

        await next(httpContext);
    }
}
