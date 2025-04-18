using AgileStudioServer.CoreFeatures.Workflows.Workflows;
using AgileStudioServer.CoreFeatures.Workflows.WorkflowStates;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyRepositoriesExtension
    {
        public static IServiceCollection AddMyRepositories(
             this IServiceCollection services)
        {
            services.AddScoped<WorkflowRepository>();
            services.AddScoped<WorkflowStateRepository>();

            return services;
        }
    }
}