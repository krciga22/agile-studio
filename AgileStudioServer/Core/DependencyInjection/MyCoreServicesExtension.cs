using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Resources.Resource;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyCoreServicesExtension
    {
        public static IServiceCollection AddMyCoreServices(
             this IServiceCollection services)
        {
            services.AddScoped<Hydrator>();
            services.AddScoped<HydratorRegistry>();
            services.AddScoped<ResourceController>();
            services.AddScoped<ServiceContext>();
            services.AddScoped<PermissionCheckerService>();

            return services;
        }
    }
}