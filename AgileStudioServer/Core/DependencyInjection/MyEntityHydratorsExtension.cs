using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemaEntries;
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
    public static class MyEntityHydratorsExtension
    {
        public static IServiceCollection AddMyEntityHydrators(
             this IServiceCollection services)
        {
            services.AddScoped<IHydrator, BacklogItemHydrator>();
            services.AddScoped<IHydrator, BacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, BacklogItemTypeSchemaHydrator>();
            services.AddScoped<IHydrator, ChildBacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeSchemaHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeSchemaEntryHydrator>();
            services.AddScoped<IHydrator, ProjectHydrator>();
            services.AddScoped<IHydrator, ReleaseHydrator>();
            services.AddScoped<IHydrator, SprintHydrator>();
            services.AddScoped<IHydrator, UserHydrator>();
            services.AddScoped<IHydrator, WorkflowHydrator>();
            services.AddScoped<IHydrator, WorkflowStateHydrator>();

            return services;
        }
    }
}