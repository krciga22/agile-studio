using AgileStudioCLI;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

IHostBuilder builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) => {
    IConfiguration configuration = context.Configuration;

    services.AddMyDB(configuration);
    services.AddMyCoreServices();
    services.AddMyRepositories();
    services.AddScoped<AgileStudioConsoleApp>();
    services.AddMyFixtureSets();
    services.AddMyFixtures();
    services.AddMyCommands();
    services.AddMyEntityHydrators();
    services.AddMyModelHydrators();
});

IHost host = builder.Build();

var scope = host.Services.CreateScope();
var svc = ActivatorUtilities.CreateInstance<AgileStudioConsoleApp>(scope.ServiceProvider);
svc.Start();