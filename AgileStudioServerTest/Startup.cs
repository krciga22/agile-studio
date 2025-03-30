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

namespace AgileStudioServerTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DBContext>(optionsBuilder => {
                DBContextFactory.ConfigureDefaultOptions(ref optionsBuilder);
            });

            services.AddScoped<BacklogItems.APIs.BacklogItemController>();
            services.AddScoped<BacklogItems.APIs.BacklogItemTypeController>();
            services.AddScoped<BacklogItemTypeSchemaController>();
            services.AddScoped<BacklogItemLinkTypeController>();
            services.AddScoped<BacklogItemLinkTypeSchemaController>();
            services.AddScoped<Projects.APIs.ProjectController>();
            services.AddScoped<Releases.APIs.ReleaseController>();
            services.AddScoped<Sprints.APIs.SprintController>();
            services.AddScoped<Workflows.APIs.WorkflowController>();
            services.AddScoped<Workflows.APIs.WorkflowStateController>();

            services.AddScoped<ModelFixtures>();
            services.AddScoped<EntityFixtures>();

            services.AddScoped<Hydrator>();
            services.AddScoped<HydratorRegistry>();

            services.AddScoped<IHydrator, BacklogItems.APIs.DTOs.Hydrators.BacklogItemDtoHydrator>();
            services.AddScoped<IHydrator, BacklogItems.APIs.DTOs.Hydrators.BacklogItemSummaryDtoHydrator>();
            services.AddScoped<IHydrator, BacklogItems.APIs.DTOs.Hydrators.BacklogItemTypeDtoHydrator>();
            services.AddScoped<IHydrator, BacklogItems.APIs.DTOs.Hydrators.BacklogItemTypeSummaryDtoHydrator>();
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
            services.AddScoped<IHydrator, Sprints.APIs.DTOs.Hydrators.SprintDtoHydrator>();
            services.AddScoped<IHydrator, Sprints.APIs.DTOs.Hydrators.SprintSummaryDtoHydrator>();
            services.AddScoped<IHydrator, Users.APIs.DTOs.Hydrators.UserSummaryDtoHydrator>();
            services.AddScoped<IHydrator, Workflows.APIs.DTOs.Hydrators.WorkflowDtoHydrator>();
            services.AddScoped<IHydrator, Workflows.APIs.DTOs.Hydrators.WorkflowSummaryDtoHydrator>();
            services.AddScoped<IHydrator, Workflows.APIs.DTOs.Hydrators.WorkflowStateDtoHydrator>();
            services.AddScoped<IHydrator, Workflows.APIs.DTOs.Hydrators.WorkflowStateSummaryDtoHydrator>();
            
            services.AddScoped<IHydrator, BacklogItems.Services.Models.Hydrators.BacklogItemHydrator>();
            services.AddScoped<IHydrator, BacklogItems.Services.Models.Hydrators.BacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, BacklogItemTypeSchemaModelHydrator>();
            services.AddScoped<IHydrator, BacklogItems.Services.Models.Hydrators.ChildBacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeModelHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeSchemaModelHydrator>();
            services.AddScoped<IHydrator, Projects.Services.Models.Hydrators.ProjectHydrator>();
            services.AddScoped<IHydrator, Releases.Services.Models.Hydrators.ReleaseHydrator>();
            services.AddScoped<IHydrator, Sprints.Services.Models.Hydrators.SprintHydrator>();
            services.AddScoped<IHydrator, Users.Services.Models.Hydrators.UserHydrator>();
            services.AddScoped<IHydrator, Workflows.Services.Models.Hydrators.WorkflowHydrator>();
            services.AddScoped<IHydrator, Workflows.Services.Models.Hydrators.WorkflowStateHydrator>();

            services.AddScoped<IHydrator, BacklogItems.Repositories.Entities.Hydrators.BacklogItemHydrator>();
            services.AddScoped<IHydrator, BacklogItems.Repositories.Entities.Hydrators.BacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, BacklogItemTypeSchemaHydrator>();
            services.AddScoped<IHydrator, BacklogItems.Repositories.Entities.Hydrators.ChildBacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeHydrator>();
            services.AddScoped<IHydrator, BacklogItemLinkTypeSchemaHydrator>();
            services.AddScoped<IHydrator, Projects.Repositories.Entities.Hydrators.ProjectHydrator>();
            services.AddScoped<IHydrator, Releases.Repositories.Entities.Hydrators.ReleaseHydrator>();
            services.AddScoped<IHydrator, Sprints.Repositories.Entities.Hydrators.SprintHydrator>();
            services.AddScoped<IHydrator, Users.Repositories.Entities.Hydrators.UserHydrator>();
            services.AddScoped<IHydrator, Workflows.Repositories.Entities.Hydrators.WorkflowHydrator>();
            services.AddScoped<IHydrator, Workflows.Repositories.Entities.Hydrators.WorkflowStateHydrator>();

            services.AddScoped<BacklogItems.Services.BacklogItemService>();
            services.AddScoped<BacklogItems.Services.BacklogItemTypeService>();
            services.AddScoped<BacklogItemTypeSchemaService>();
            services.AddScoped<BacklogItems.Services.ChildBacklogItemTypeService>();
            services.AddScoped<BacklogItemLinkTypeService>();
            services.AddScoped<BacklogItemLinkTypeSchemaService>();
            services.AddScoped<Projects.Services.ProjectService>();
            services.AddScoped<Releases.Services.ReleaseService>();
            services.AddScoped<Sprints.Services.SprintService>();
            services.AddScoped<Users.Services.UserService>();
            services.AddScoped<Workflows.Services.WorkflowService>();
            services.AddScoped<Workflows.Services.WorkflowStateService>();
        }
    }
}
