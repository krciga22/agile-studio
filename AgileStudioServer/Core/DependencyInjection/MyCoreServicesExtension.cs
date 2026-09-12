using AgileStudioServer.Core.Command;
using AgileStudioServer.Core.Events;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyCoreServicesExtension
    {
        public static IServiceCollection AddMyCoreServices(
             this IServiceCollection services)
        {
            services.AddScoped<Hydrator>();
            services.AddScoped<HydratorRegistry>();
            services.AddScoped<ServiceContext>();
            services.AddScoped<CommandDispatcher>();
            services.AddScoped<EventDispatcher>();

            return services;
        }
    }
}