using Microsoft.Extensions.DependencyInjection;

namespace AgileStudioServerTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMyDB();
            services.AddMyRepositories();
            services.AddMyCoreServices();
            services.AddMyControllers();
            services.AddMyCoreFeatureServices();
            services.AddMyDtoHydrators();
            services.AddMyModelHydrators();
            services.AddMyEntityHydrators();
            services.AddMyFixtures();
        }
    }
}
