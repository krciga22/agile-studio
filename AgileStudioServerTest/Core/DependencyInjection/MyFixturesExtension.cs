using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemLinkTypes;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemaEntries;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItems;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypes;
using AgileStudioServerTest.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServerTest.CoreFeatures.BacklogItems.ChildBacklogItemTypes;
using AgileStudioServerTest.CoreFeatures.Projects.Projects;
using AgileStudioServerTest.CoreFeatures.Releases.Releases;
using AgileStudioServerTest.CoreFeatures.Sprints.Sprints;
using AgileStudioServerTest.CoreFeatures.Users.Users;
using AgileStudioServerTest.CoreFeatures.Workflows.Workflows;
using AgileStudioServerTest.CoreFeatures.Workflows.WorkflowStates;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyFixturesExtension
    {
        public static IServiceCollection AddMyFixtures(
             this IServiceCollection services)
        {
            services.AddScoped<BacklogItemLinkTypeFixture>();
            services.AddScoped<BacklogItemLinkTypeSchemaEntryFixture>();
            services.AddScoped<BacklogItemLinkTypeSchemaFixture>();
            services.AddScoped<BacklogItemFixture>();
            services.AddScoped<BacklogItemTypeFixture>();
            services.AddScoped<BacklogItemTypeSchemaFixture>();
            services.AddScoped<ChildBacklogItemTypeFixture>();
            services.AddScoped<ProjectFixture>();
            services.AddScoped<ReleaseFixture>();
            services.AddScoped<SprintFixture>();
            services.AddScoped<UserFixture>();
            services.AddScoped<WorkflowFixture>();
            services.AddScoped<WorkflowStateFixture>();

            return services;
        }
    }
}