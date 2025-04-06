using AgileStudioServerTest.IntegrationTests;
using Microsoft.Extensions.DependencyInjection;

namespace AgileStudioServerTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMyDB();

            services.AddMyFixtures();

            services.AddMyCoreServices();

            services.AddDtoHydrators();
            services.AddModelHydrators();
            services.AddEntityHydrators();
            services.AddMyCoreFeatureServices();
            services.AddMyControllers();
        }
    }
}
