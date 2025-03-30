using AgileStudioServer.Data;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

namespace AgileStudioServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DBContext>(optionsBuilder => {
                DBContextFactory.ConfigureDefaultOptions(ref optionsBuilder);
            });

            builder.Services.AddScoped<Hydrator>();
            builder.Services.AddScoped<HydratorRegistry>();

            builder.Services.AddScoped<IHydrator, BacklogItemDtoHydrator>();
            builder.Services.AddScoped<IHydrator, BacklogItemSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, BacklogItemTypeDtoHydrator>();
            builder.Services.AddScoped<IHydrator, BacklogItemTypeSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, BacklogItemTypeSchemaDtoHydrator>();
            builder.Services.AddScoped<IHydrator, BacklogItemTypeSchemaSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, BacklogItemLinkTypeDtoHydrator>();
            builder.Services.AddScoped<IHydrator, BacklogItemLinkTypeSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, BacklogItemLinkTypeSchemaDtoHydrator>();
            builder.Services.AddScoped<IHydrator, BacklogItemLinkTypeSchemaSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, Projects.APIs.DTOs.Hydrators.ProjectDtoHydrator>();
            builder.Services.AddScoped<IHydrator, Projects.APIs.DTOs.Hydrators.ProjectSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, Releases.APIs.DTOs.Hydrators.ReleaseDtoHydrator>();
            builder.Services.AddScoped<IHydrator, Releases.APIs.DTOs.Hydrators.ReleaseSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, Sprints.APIs.DTOs.Hydrators.SprintDtoHydrator>();
            builder.Services.AddScoped<IHydrator, Sprints.APIs.DTOs.Hydrators.SprintSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, Users.APIs.DTOs.Hydrators.UserSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, Workflows.APIs.DTOs.Hydrators.WorkflowDtoHydrator>();
            builder.Services.AddScoped<IHydrator, Workflows.APIs.DTOs.Hydrators.WorkflowSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, Workflows.APIs.DTOs.Hydrators.WorkflowStateDtoHydrator>();
            builder.Services.AddScoped<IHydrator, Workflows.APIs.DTOs.Hydrators.WorkflowStateSummaryDtoHydrator>();

            builder.Services.AddScoped<IHydrator, BacklogItemModelHydrator>();
            builder.Services.AddScoped<IHydrator, BacklogItemTypeModelHydrator>();
            builder.Services.AddScoped<IHydrator, BacklogItemTypeSchemaModelHydrator>();
            builder.Services.AddScoped<IHydrator, ChildBacklogItemTypeModelHydrator>();
            builder.Services.AddScoped<IHydrator, BacklogItemLinkTypeModelHydrator>();
            builder.Services.AddScoped<IHydrator, BacklogItemLinkTypeSchemaModelHydrator>();
            builder.Services.AddScoped<IHydrator, Projects.Services.Models.Hydrators.ProjectHydrator>();
            builder.Services.AddScoped<IHydrator, Releases.Services.Models.Hydrators.ReleaseHydrator>();
            builder.Services.AddScoped<IHydrator, Sprints.Services.Models.Hydrators.SprintHydrator>();
            builder.Services.AddScoped<IHydrator, Users.Services.Models.Hydrators.UserHydrator>();
            builder.Services.AddScoped<IHydrator, Workflows.Services.Models.Hydrators.WorkflowHydrator>();
            builder.Services.AddScoped<IHydrator, Workflows.Services.Models.Hydrators.WorkflowStateHydrator>();

            builder.Services.AddScoped<IHydrator, BacklogItemHydrator>();
            builder.Services.AddScoped<IHydrator, BacklogItemTypeHydrator>();
            builder.Services.AddScoped<IHydrator, BacklogItemTypeSchemaHydrator>();
            builder.Services.AddScoped<IHydrator, ChildBacklogItemTypeHydrator>();
            builder.Services.AddScoped<IHydrator, BacklogItemLinkTypeHydrator>();
            builder.Services.AddScoped<IHydrator, BacklogItemLinkTypeSchemaHydrator>();
            builder.Services.AddScoped<IHydrator, Projects.Repositories.Entities.Hydrators.ProjectHydrator>();
            builder.Services.AddScoped<IHydrator, Releases.Repositories.Entities.Hydrators.ReleaseHydrator>();
            builder.Services.AddScoped<IHydrator, Sprints.Repositories.Entities.Hydrators.SprintHydrator>();
            builder.Services.AddScoped<IHydrator, Users.Repositories.Entities.Hydrators.UserHydrator>();
            builder.Services.AddScoped<IHydrator, Workflows.Repositories.Entities.Hydrators.WorkflowHydrator>();
            builder.Services.AddScoped<IHydrator, Workflows.Repositories.Entities.Hydrators.WorkflowStateHydrator>();

            builder.Services.AddScoped<BacklogItemService>();
            builder.Services.AddScoped<BacklogItemTypeService>();
            builder.Services.AddScoped<BacklogItemTypeSchemaService>();
            builder.Services.AddScoped<ChildBacklogItemTypeService>();
            builder.Services.AddScoped<BacklogItemLinkTypeService>();
            builder.Services.AddScoped<BacklogItemLinkTypeSchemaService>();
            builder.Services.AddScoped<Projects.Services.ProjectService>();
            builder.Services.AddScoped<Releases.Services.ReleaseService>();
            builder.Services.AddScoped<Sprints.Services.SprintService>();
            builder.Services.AddScoped<Users.Services.UserService>();
            builder.Services.AddScoped<Workflows.Services.WorkflowService>();
            builder.Services.AddScoped<Workflows.Services.WorkflowStateService>();

            string auth0Domain = builder.Configuration.GetValue<string>("Auth0:Domain");
            string auth0ClientId = builder.Configuration.GetValue<string>("Auth0:ClientId");
            string auth0ClientSecret = builder.Configuration.GetValue<string>("Auth0:ClientSecret");
            string auth0Audience = builder.Configuration.GetValue<string>("Auth0:Audience");

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://{auth0Domain}";
                    options.Audience = auth0Audience;
                })
                .AddAuth0WebAppAuthentication(options =>
                {
                    options.Domain = auth0Domain;
                    options.ClientId = auth0ClientId;
                    options.ClientSecret = auth0ClientSecret;
                    options.CallbackPath = "/Auth/Callback";
                })
                .WithAccessToken(options =>
                {
                    options.Audience = auth0Audience;
                    options.UseRefreshTokens = false;
                });

            builder.Services.AddAuthorization(options =>
            {
                var policyBuilder = new AuthorizationPolicyBuilder();
                policyBuilder.AddAuthenticationSchemes(new string[] {
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        JwtBearerDefaults.AuthenticationScheme
                    })
                    .RequireAuthenticatedUser();
                options.DefaultPolicy = policyBuilder.Build();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}