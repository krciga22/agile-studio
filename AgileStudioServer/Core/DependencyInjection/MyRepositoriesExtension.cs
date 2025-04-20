using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItems;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.ChildBacklogItemTypes;
using AgileStudioServer.CoreFeatures.Projects.Projects;
using AgileStudioServer.CoreFeatures.Releases.Releases;
using AgileStudioServer.CoreFeatures.Sprints.Sprints;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.CoreFeatures.Workflows.Workflows;
using AgileStudioServer.CoreFeatures.Workflows.WorkflowStates;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyRepositoriesExtension
    {
        public static IServiceCollection AddMyRepositories(
             this IServiceCollection services)
        {
            services.AddScoped<BacklogItemLinkTypeSchemaRepository>();
            services.AddScoped<BacklogItemRepository>();
            services.AddScoped<BacklogItemTypeRepository>();
            services.AddScoped<BacklogItemTypeSchemaRepository>();
            services.AddScoped<ChildBacklogItemTypeRepository>();
            services.AddScoped<ProjectRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<ReleaseRepository>();
            services.AddScoped<SprintRepository>();
            services.AddScoped<WorkflowRepository>();
            services.AddScoped<WorkflowStateRepository>();

            return services;
        }
    }
}