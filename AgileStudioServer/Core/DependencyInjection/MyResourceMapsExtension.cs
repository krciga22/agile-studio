using AgileStudioServer.Core.Resources;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyResourceMapsExtension
    {
        public static IServiceCollection AddMyResourceMaps(
             this IServiceCollection services)
        {
            var classCollection = Assembly.GetExecutingAssembly()
                .DefinedTypes.Where(t =>
                    t.ImplementedInterfaces.Contains(typeof(IResourceMap)) &&
                    t.IsPublic && !t.IsAbstract);

            foreach (var classType in classCollection)
            {
                if (typeof(IResourceMap).IsAssignableFrom(classType)){
                    services.AddSingleton(typeof(IResourceMap), classType);
                }
            }

            return services;
        }
    }
}