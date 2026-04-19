using AgileStudioServer.Core.Services;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyFeatureServicesExtension
    {
        public static IServiceCollection AddMyFeatureServices(
             this IServiceCollection services)
        {
            var classCollection = Assembly.GetExecutingAssembly()
                            .DefinedTypes.Where(t =>
                                t.IsSubclassOf(typeof(AbstractService)) &&
                                t.IsPublic && !t.IsAbstract);

            foreach (var classType in classCollection){
                services.AddScoped(classType);
            }

            classCollection = Assembly.GetExecutingAssembly()
                            .DefinedTypes.Where(t =>
                                t.ImplementedInterfaces.Contains(typeof(IModelService)) &&
                                t.IsPublic && !t.IsAbstract);

            foreach (var classType in classCollection){
                services.AddScoped(typeof(IModelService), classType);
            }

            return services;
        }
    }
}