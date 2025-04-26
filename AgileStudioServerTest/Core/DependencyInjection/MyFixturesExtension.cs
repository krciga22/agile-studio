using AgileStudioServerTest.Core.Fixtures;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyFixturesExtension
    {
        public static IServiceCollection AddMyFixtures(
             this IServiceCollection services)
        {
            var classCollection = Assembly.GetExecutingAssembly()
                            .DefinedTypes.Where(t =>
                                t.IsSubclassOf(typeof(AbstractFixture)) &&
                                t.IsPublic &&
                                !t.IsAbstract);

            foreach (var classType in classCollection)
            {
                services.AddScoped(classType);
            }

            return services;
        }
    }
}