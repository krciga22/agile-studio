using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItems;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.Projects.Projects;
using AgileStudioServer.CoreFeatures.Releases.Releases;
using AgileStudioServer.CoreFeatures.Sprints.Sprints;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.CoreFeatures.Workflows.Workflows;
using AgileStudioServer.CoreFeatures.Workflows.WorkflowStates;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyDtoHydratorsExtension
    {
        public static IServiceCollection AddMyDtoHydrators(
             this IServiceCollection services)
        {
            services.AddScoped<IHydrator, BacklogItemDtoHydrator>();
            services.AddScoped<IHydrator, BacklogItemSummaryDtoHydrator>();
            services.AddScoped<IHydrator, BacklogItemTypeDtoHydrator>();
            services.AddScoped<IHydrator, BacklogItemTypeSummaryDtoHydrator>();
            services.AddScoped<IHydrator, BacklogItemTypeSchemaDtoHydrator>();
            services.AddScoped<IHydrator, BacklogItemTypeSchemaSummaryDtoHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeDtoHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeSummaryDtoHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeSchemaDtoHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeSchemaSummaryDtoHydrator>();
            services.AddScoped<IHydrator, ProjectDtoHydrator>();
            services.AddScoped<IHydrator, ProjectSummaryDtoHydrator>();
            services.AddScoped<IHydrator, ReleaseDtoHydrator>();
            services.AddScoped<IHydrator, ReleaseSummaryDtoHydrator>();
            services.AddScoped<IHydrator, SprintDtoHydrator>();
            services.AddScoped<IHydrator, SprintSummaryDtoHydrator>();
            services.AddScoped<IHydrator, UserSummaryDtoHydrator>();
            services.AddScoped<IHydrator, WorkflowDtoHydrator>();
            services.AddScoped<IHydrator, WorkflowSummaryDtoHydrator>();
            services.AddScoped<IHydrator, WorkflowStateDtoHydrator>();
            services.AddScoped<IHydrator, WorkflowStateSummaryDtoHydrator>();

            return services;
        }
    }
}