using AgileStudioServerTest.IntegrationTests;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyFixturesExtension
    {
        public static IServiceCollection AddMyFixtures(
             this IServiceCollection services)
        {
            services.AddScoped<ModelFixtures>();
            services.AddScoped<EntityFixtures>();

            return services;
        }
    }
}