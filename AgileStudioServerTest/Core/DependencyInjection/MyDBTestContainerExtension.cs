using AgileStudioServerTest.IntegrationTests;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyDBTestContainerExtension
    {
        public static IServiceCollection AddMyDBTestContainer(
             this IServiceCollection services)
        {
            IConfiguration configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            DBTestContainer dbTestContainer = DBTestContainer.GetInstance(configuration);
            dbTestContainer.Start();
            services.AddSingleton(dbTestContainer);
            return services;
        }
    }
}