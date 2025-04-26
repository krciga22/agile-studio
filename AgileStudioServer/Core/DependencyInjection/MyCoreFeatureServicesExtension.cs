using AgileStudioServer.Core.Services;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyCoreFeatureServicesExtension
    {
        public static IServiceCollection AddMyCoreFeatureServices(
             this IServiceCollection services)
        {
            var classCollection = Assembly.GetExecutingAssembly()
                            .DefinedTypes.Where(t =>
                                t.IsSubclassOf(typeof(AbstractService)) &&
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