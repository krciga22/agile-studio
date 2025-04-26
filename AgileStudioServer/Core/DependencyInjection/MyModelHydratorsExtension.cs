using AgileStudioServer.Core.Hydrator;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyModelHydratorsExtension
    {
        public static IServiceCollection AddMyModelHydrators(
             this IServiceCollection services)
        {
            var classCollection = Assembly.GetExecutingAssembly()
                            .DefinedTypes.Where(t =>
                                t.IsSubclassOf(typeof(AbstractModelHydrator)) &&
                                t.IsPublic &&
                                !t.IsAbstract);

            foreach (var classType in classCollection)
            {
                services.AddScoped(typeof(IHydrator), classType);
            }

            return services;
        }
    }
}