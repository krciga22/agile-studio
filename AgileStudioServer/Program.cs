using DtoHydrators = AgileStudioServer.API.Dtos.Hydrators;
using ModelHydrators = AgileStudioServer.Application.Models.Hydrators;
using EntityHydrators = AgileStudioServer.Data.Entities.Hydrators;

using AgileStudioServer.Application.Services;
using AgileStudioServer.Data;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.CoreFeatures.Sprints.APIs.DTOs.Hydrators;
using AgileStudioServer.CoreFeatures.Sprints.Services;
using AgileStudioServer.CoreFeatures.Sprints.Services.Models.Hydrators;
using AgileStudioServer.CoreFeatures.Sprints.Repositories.Entities.Hydrators;
using AgileStudioServer.CoreFeatures.Releases.APIs.DTOs.Hydrators;
using AgileStudioServer.CoreFeatures.Releases.Services;
using AgileStudioServer.CoreFeatures.Releases.Services.Models.Hydrators;
using AgileStudioServer.CoreFeatures.Releases.Repositories.Entities.Hydrators;
using AgileStudioServer.CoreFeatures.Projects.APIs.DTOs.Hydrators;

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

            builder.Services.AddScoped<IHydrator, DtoHydrators.BacklogItemDtoHydrator>();
            builder.Services.AddScoped<IHydrator, DtoHydrators.BacklogItemSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, DtoHydrators.BacklogItemTypeDtoHydrator>();
            builder.Services.AddScoped<IHydrator, DtoHydrators.BacklogItemTypeSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, DtoHydrators.BacklogItemTypeSchemaDtoHydrator>();
            builder.Services.AddScoped<IHydrator, DtoHydrators.BacklogItemTypeSchemaSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, ProjectDtoHydrator>();
            builder.Services.AddScoped<IHydrator, ProjectSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, ReleaseDtoHydrator>();
            builder.Services.AddScoped<IHydrator, ReleaseSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, SprintDtoHydrator>();
            builder.Services.AddScoped<IHydrator, SprintSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, DtoHydrators.UserSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, DtoHydrators.WorkflowDtoHydrator>();
            builder.Services.AddScoped<IHydrator, DtoHydrators.WorkflowSummaryDtoHydrator>();
            builder.Services.AddScoped<IHydrator, DtoHydrators.WorkflowStateDtoHydrator>();
            builder.Services.AddScoped<IHydrator, DtoHydrators.WorkflowStateSummaryDtoHydrator>();

            builder.Services.AddScoped<IHydrator, ModelHydrators.BacklogItemHydrator>();
            builder.Services.AddScoped<IHydrator, ModelHydrators.BacklogItemTypeHydrator>();
            builder.Services.AddScoped<IHydrator, ModelHydrators.BacklogItemTypeSchemaHydrator>();
            builder.Services.AddScoped<IHydrator, ModelHydrators.ChildBacklogItemTypeHydrator>();
            builder.Services.AddScoped<IHydrator, ModelHydrators.ProjectHydrator>();
            builder.Services.AddScoped<IHydrator, CoreFeatures.Releases.Services.Models.Hydrators.ReleaseHydrator>();
            builder.Services.AddScoped<IHydrator, CoreFeatures.Sprints.Services.Models.Hydrators.SprintHydrator>();
            builder.Services.AddScoped<IHydrator, ModelHydrators.UserHydrator>();
            builder.Services.AddScoped<IHydrator, ModelHydrators.WorkflowHydrator>();
            builder.Services.AddScoped<IHydrator, ModelHydrators.WorkflowStateHydrator>();

            builder.Services.AddScoped<IHydrator, EntityHydrators.BacklogItemHydrator>();
            builder.Services.AddScoped<IHydrator, EntityHydrators.BacklogItemTypeHydrator>();
            builder.Services.AddScoped<IHydrator, EntityHydrators.BacklogItemTypeSchemaHydrator>();
            builder.Services.AddScoped<IHydrator, EntityHydrators.ChildBacklogItemTypeHydrator>();
            builder.Services.AddScoped<IHydrator, EntityHydrators.ProjectHydrator>();
            builder.Services.AddScoped<IHydrator, CoreFeatures.Releases.Repositories.Entities.Hydrators.ReleaseHydrator>();
            builder.Services.AddScoped<IHydrator, CoreFeatures.Sprints.Repositories.Entities.Hydrators.SprintHydrator>();
            builder.Services.AddScoped<IHydrator, EntityHydrators.UserHydrator>();
            builder.Services.AddScoped<IHydrator, EntityHydrators.WorkflowHydrator>();
            builder.Services.AddScoped<IHydrator, EntityHydrators.WorkflowStateHydrator>();

            builder.Services.AddScoped<BacklogItemService>();
            builder.Services.AddScoped<BacklogItemTypeService>();
            builder.Services.AddScoped<BacklogItemTypeSchemaService>();
            builder.Services.AddScoped<ChildBacklogItemTypeService>();
            builder.Services.AddScoped<ProjectService>();
            builder.Services.AddScoped<ReleaseService>();
            builder.Services.AddScoped<SprintService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<WorkflowService>();
            builder.Services.AddScoped<WorkflowStateService>();

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