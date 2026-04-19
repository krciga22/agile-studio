using AgileStudioServerTest.IntegrationTests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace AgileStudioServerTest
{
    public class Startup
    {
        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            // manually setup configuration because the test
            // host doesn't do it natively
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .AddEnvironmentVariables()
                .Build();

            hostBuilder.ConfigureHostConfiguration(builder => builder.AddConfiguration(config));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMyDBTestContainer();
            services.AddMyTestDB();
            services.AddMyRepositories();
            services.AddMyCoreServices();
            services.AddMyCoreCommands();
            services.AddMyControllers();
            services.AddMyFeatureServices();
            services.AddMyDtoHydrators();
            services.AddMyModelHydrators();
            services.AddMyEntityHydrators();
            services.AddMyFixtures();
        }
    }
}
