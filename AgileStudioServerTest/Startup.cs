using AgileStudioServerTest.IntegrationTests;
using Microsoft.Extensions.DependencyInjection;

namespace AgileStudioServerTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            DBTestContainer dbTestContainer = DBTestContainer.GetInstance();
            dbTestContainer.Start();

            services.AddMyTestDB();
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
