using AgileStudioCLI.Commands;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyCommandsExtension
    {
        public static IServiceCollection AddMyCommands(
             this IServiceCollection services)
        {
            services.AddScoped<ClearFixturesCommand>();
            services.AddScoped<LoadFixturesCommand>();

            return services;
        }
    }
}