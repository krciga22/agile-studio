using AgileStudioServer.Core.Hydrator;
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

            return services;
        }
    }
}