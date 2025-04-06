using AgileStudioServer.Core.Hydrator;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyCoreServicesExtension
    {
        public static IServiceCollection AddMyCoreServices(
             this IServiceCollection services)
        {
            services.AddScoped<Hydrator>();
            services.AddScoped<HydratorRegistry>();

            return services;
        }
    }
}