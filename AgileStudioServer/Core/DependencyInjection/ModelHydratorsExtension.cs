using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes;
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
    public static class ModelHydratorsExtension
    {
        public static IServiceCollection AddModelHydrators(
             this IServiceCollection services)
        {
            services.AddScoped<IHydrator, BacklogItemModelHydrator>();
            services.AddScoped<IHydrator, BacklogItemTypeModelHydrator>();
            services.AddScoped<IHydrator, BacklogItemTypeSchemaModelHydrator>();
            services.AddScoped<IHydrator, ChildBacklogItemTypeModelHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeModelHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeSchemaModelHydrator>();
            services.AddScoped<IHydrator, ProjectModelHydrator>();
            services.AddScoped<IHydrator, ReleaseModelHydrator>();
            services.AddScoped<IHydrator, SprintModelHydrator>();
            services.AddScoped<IHydrator, UserModelHydrator>();
            services.AddScoped<IHydrator, WorkflowModelHydrator>();
            services.AddScoped<IHydrator, WorkflowStateModelHydrator>();

            return services;
        }
    }
}