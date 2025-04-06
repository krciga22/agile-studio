using AgileStudioServer.Data;
using AgileStudioServerTest.IntegrationTests;
using Microsoft.Extensions.DependencyInjection;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.ChildBacklogItemTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItems;
using AgileStudioServer.CoreFeatures.Sprints.Sprints;
using AgileStudioServer.CoreFeatures.Releases.Releases;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.CoreFeatures.Workflows.WorkflowStates;
using AgileStudioServer.CoreFeatures.Workflows.Workflows;
using AgileStudioServer.CoreFeatures.Projects.Projects;

namespace AgileStudioServerTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DBContext>(optionsBuilder => {
                DBContextFactory.ConfigureDefaultOptions(ref optionsBuilder);
            });

            services.AddScoped<BacklogItemController>();
            services.AddScoped<BacklogItemTypeController>();
            services.AddScoped<BacklogItemTypeSchemaController>();
            services.AddScoped<BacklogItemLinkTypeController>();
            services.AddScoped<BacklogItemLinkTypeSchemaController>();
            services.AddScoped<ProjectController>();
            services.AddScoped<ReleaseController>();
            services.AddScoped<SprintController>();
            services.AddScoped<WorkflowController>();
            services.AddScoped<WorkflowStateController>();

            services.AddScoped<ModelFixtures>();
            services.AddScoped<EntityFixtures>();

            services.AddScoped<Hydrator>();
            services.AddScoped<HydratorRegistry>();

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

            services.AddScoped<IHydrator, BacklogItemHydrator>();
            services.AddScoped<IHydrator, BacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, BacklogItemTypeSchemaHydrator>();
            services.AddScoped<IHydrator, ChildBacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeSchemaHydrator>();
            services.AddScoped<IHydrator, ProjectHydrator>();
            services.AddScoped<IHydrator, ReleaseHydrator>();
            services.AddScoped<IHydrator, SprintHydrator>();
            services.AddScoped<IHydrator, UserHydrator>();
            services.AddScoped<IHydrator, WorkflowHydrator>();
            services.AddScoped<IHydrator, WorkflowStateHydrator>();

            services.AddScoped<BacklogItemService>();
            services.AddScoped<BacklogItemTypeService>();
            services.AddScoped<BacklogItemTypeSchemaService>();
            services.AddScoped<ChildBacklogItemTypeService>();
            services.AddScoped<BacklogItemLinkTypeService>();
            services.AddScoped<BacklogItemLinkTypeSchemaService>();
            services.AddScoped<ProjectService>();
            services.AddScoped<ReleaseService>();
            services.AddScoped<SprintService>();
            services.AddScoped<UserService>();
            services.AddScoped<WorkflowService>();
            services.AddScoped<WorkflowStateService>();
        }
    }
}
