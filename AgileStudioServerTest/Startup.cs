using AgileStudioServer.Data;
using AgileStudioServerTest.IntegrationTests;
using Microsoft.Extensions.DependencyInjection;
using AgileStudioServer.Core.Hydrator;

using BacklogItems = AgileStudioServer.CoreFeatures.BacklogItems;
using Projects = AgileStudioServer.CoreFeatures.Projects;
using Releases = AgileStudioServer.CoreFeatures.Releases;
using Sprints = AgileStudioServer.CoreFeatures.Sprints;
using Users = AgileStudioServer.CoreFeatures.Users;
using Workflows = AgileStudioServer.CoreFeatures.Workflows;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.ChildBacklogItemTypes;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItems;
using AgileStudioServer.CoreFeatures.Sprints.Sprints;

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
            services.AddScoped<Projects.APIs.ProjectController>();
            services.AddScoped<Releases.APIs.ReleaseController>();
            services.AddScoped<SprintController>();
            services.AddScoped<Workflows.APIs.WorkflowController>();
            services.AddScoped<Workflows.APIs.WorkflowStateController>();

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

            services.AddScoped<IHydrator, Projects.APIs.DTOs.Hydrators.ProjectDtoHydrator>();
            services.AddScoped<IHydrator, Projects.APIs.DTOs.Hydrators.ProjectSummaryDtoHydrator>();
            services.AddScoped<IHydrator, Releases.APIs.DTOs.Hydrators.ReleaseDtoHydrator>();
            services.AddScoped<IHydrator, Releases.APIs.DTOs.Hydrators.ReleaseSummaryDtoHydrator>();
            services.AddScoped<IHydrator, SprintDtoHydrator>();
            services.AddScoped<IHydrator, SprintSummaryDtoHydrator>();
            services.AddScoped<IHydrator, Users.APIs.DTOs.Hydrators.UserSummaryDtoHydrator>();
            services.AddScoped<IHydrator, Workflows.APIs.DTOs.Hydrators.WorkflowDtoHydrator>();
            services.AddScoped<IHydrator, Workflows.APIs.DTOs.Hydrators.WorkflowSummaryDtoHydrator>();
            services.AddScoped<IHydrator, Workflows.APIs.DTOs.Hydrators.WorkflowStateDtoHydrator>();
            services.AddScoped<IHydrator, Workflows.APIs.DTOs.Hydrators.WorkflowStateSummaryDtoHydrator>();
            
            services.AddScoped<IHydrator, BacklogItemModelHydrator>();
            services.AddScoped<IHydrator, BacklogItemTypeModelHydrator>();
            services.AddScoped<IHydrator, BacklogItemTypeSchemaModelHydrator>();
            services.AddScoped<IHydrator, ChildBacklogItemTypeModelHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeModelHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeSchemaModelHydrator>();
            services.AddScoped<IHydrator, Projects.Services.Models.Hydrators.ProjectHydrator>();
            services.AddScoped<IHydrator, Releases.Services.Models.Hydrators.ReleaseHydrator>();
            services.AddScoped<IHydrator, SprintModelHydrator>();
            services.AddScoped<IHydrator, Users.Services.Models.Hydrators.UserHydrator>();
            services.AddScoped<IHydrator, Workflows.Services.Models.Hydrators.WorkflowHydrator>();
            services.AddScoped<IHydrator, Workflows.Services.Models.Hydrators.WorkflowStateHydrator>();

            services.AddScoped<IHydrator, BacklogItemHydrator>();
            services.AddScoped<IHydrator, BacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, BacklogItemTypeSchemaHydrator>();
            services.AddScoped<IHydrator, ChildBacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeSchemaHydrator>();
            services.AddScoped<IHydrator, Projects.Repositories.Entities.Hydrators.ProjectHydrator>();
            services.AddScoped<IHydrator, Releases.Repositories.Entities.Hydrators.ReleaseHydrator>();
            services.AddScoped<IHydrator, SprintHydrator>();
            services.AddScoped<IHydrator, Users.Repositories.Entities.Hydrators.UserHydrator>();
            services.AddScoped<IHydrator, Workflows.Repositories.Entities.Hydrators.WorkflowHydrator>();
            services.AddScoped<IHydrator, Workflows.Repositories.Entities.Hydrators.WorkflowStateHydrator>();

            services.AddScoped<BacklogItemService>();
            services.AddScoped<BacklogItemTypeService>();
            services.AddScoped<BacklogItemTypeSchemaService>();
            services.AddScoped<ChildBacklogItemTypeService>();
            services.AddScoped<BacklogItemLinkTypeService>();
            services.AddScoped<BacklogItemLinkTypeSchemaService>();
            services.AddScoped<Projects.Services.ProjectService>();
            services.AddScoped<Releases.Services.ReleaseService>();
            services.AddScoped<SprintService>();
            services.AddScoped<Users.Services.UserService>();
            services.AddScoped<Workflows.Services.WorkflowService>();
            services.AddScoped<Workflows.Services.WorkflowStateService>();
        }
    }
}
