using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItems;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.Projects.Projects;
using AgileStudioServer.CoreFeatures.Releases.Releases;
using AgileStudioServer.CoreFeatures.Sprints.Sprints;
using AgileStudioServer.CoreFeatures.Workflows.Workflows;
using AgileStudioServer.CoreFeatures.Workflows.WorkflowStates;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyControllersExtension
    {
        public static IServiceCollection AddMyControllers(
             this IServiceCollection services)
        {
            services.AddScoped<BacklogItemController>();
            services.AddScoped<BacklogItemTypeController>();
            services.AddScoped<BacklogItemTypeSchemaController>();
            services.AddScoped<BacklogItemLinkTypeController>();
            services.AddScoped<BacklogItemLinkTypeSchemaController>();
            services.AddScoped<BacklogItemLinkTypeSchemaEntryController>();
            services.AddScoped<ProjectController>();
            services.AddScoped<ReleaseController>();
            services.AddScoped<SprintController>();
            services.AddScoped<WorkflowController>();
            services.AddScoped<WorkflowStateController>();

            return services;
        }
    }
}