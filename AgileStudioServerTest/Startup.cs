using Microsoft.Extensions.DependencyInjection;

namespace AgileStudioServerTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMyDB();
            services.AddMyCoreServices();
            services.AddMyControllers();
            services.AddMyCoreFeatureServices();
            services.AddDtoHydrators();
            services.AddModelHydrators();
            services.AddEntityHydrators();
            services.AddMyFixtures();
        }
    }
}
