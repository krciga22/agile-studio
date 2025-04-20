using AgileStudioCLI;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

IHostBuilder builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((services) => {
    services.AddMyDB();
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

var svc = ActivatorUtilities.CreateInstance<AgileStudioConsoleApp>(host.Services);
svc.Start();