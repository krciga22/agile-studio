using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services;
using AgileStudioServer.CoreFeatures.Resources.Resource;

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

            return services;
        }
    }
}