using ModelHydrators = AgileStudioServer.Application.Models.Hydrators;
using EntityHydrators = AgileStudioServer.Data.Entities.Hydrators;
using AgileStudioServer.Application.Services;
using AgileStudioServer.Data;
using AgileStudioServerTest.IntegrationTests;
using Microsoft.Extensions.DependencyInjection;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.CoreFeatures.Sprints.APIs;
using AgileStudioServer.CoreFeatures.Sprints.APIs.DTOs.Hydrators;
using AgileStudioServer.CoreFeatures.Sprints.Services;
using AgileStudioServer.CoreFeatures.Releases.APIs;
using AgileStudioServer.CoreFeatures.Releases.APIs.DTOs.Hydrators;
using AgileStudioServer.CoreFeatures.Releases.Services;
using AgileStudioServer.CoreFeatures.Releases.Services.Models.Hydrators;
using AgileStudioServer.CoreFeatures.Releases.Repositories.Entities.Hydrators;
using AgileStudioServer.CoreFeatures.Workflows.APIs;
using AgileStudioServer.CoreFeatures.Projects.APIs;
using AgileStudioServer.CoreFeatures.Projects.APIs.DTOs.Hydrators;
using AgileStudioServer.CoreFeatures.Workflows.APIs.DTOs.Hydrators;
using AgileStudioServer.CoreFeatures.BacklogItems.APIs;
using AgileStudioServer.CoreFeatures.BacklogItems.APIs.DTOs.Hydrators;
using AgileStudioServer.CoreFeatures.Users.APIs.DTOs.Hydrators;
using AgileStudioServer.CoreFeatures.Workflows.Services;
using AgileStudioServer.CoreFeatures.Workflows.Services.Models.Hydrators;
using AgileStudioServer.CoreFeatures.Projects.Services;

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
            
            services.AddScoped<IHydrator, ModelHydrators.BacklogItemHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.BacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.BacklogItemTypeSchemaHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.ChildBacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.ProjectHydrator>();
            services.AddScoped<IHydrator, AgileStudioServer.CoreFeatures.Releases.Services.Models.Hydrators.ReleaseHydrator>();
            services.AddScoped<IHydrator, AgileStudioServer.CoreFeatures.Sprints.Services.Models.Hydrators.SprintHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.UserHydrator>();
            services.AddScoped<IHydrator, WorkflowHydrator>();
            services.AddScoped<IHydrator, WorkflowStateHydrator>();

            services.AddScoped<IHydrator, EntityHydrators.BacklogItemHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.BacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.BacklogItemTypeSchemaHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.ChildBacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.ProjectHydrator>();
            services.AddScoped<IHydrator, AgileStudioServer.CoreFeatures.Releases.Repositories.Entities.Hydrators.ReleaseHydrator>();
            services.AddScoped<IHydrator, AgileStudioServer.CoreFeatures.Sprints.Repositories.Entities.Hydrators.SprintHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.UserHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.WorkflowHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.WorkflowStateHydrator>();

            services.AddScoped<BacklogItemService>();
            services.AddScoped<BacklogItemTypeService>();
            services.AddScoped<BacklogItemTypeSchemaService>();
            services.AddScoped<ChildBacklogItemTypeService>();
            services.AddScoped<ProjectService>();
            services.AddScoped<ReleaseService>();
            services.AddScoped<SprintService>();
            services.AddScoped<UserService>();
            services.AddScoped<WorkflowService>();
            services.AddScoped<WorkflowStateService>();
        }
    }
}
