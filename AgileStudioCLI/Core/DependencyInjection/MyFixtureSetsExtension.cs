using AgileStudioCLI.Commands;
using AgileStudioCLI.FixtureSets;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyFixtureSetsExtension
    {
        public static IServiceCollection AddMyFixtureSets(
             this IServiceCollection services)
        {
            services.AddScoped<BaseFixtureSet>();

            return services;
        }
    }
}