using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyControllersExtension
    {
        public static IServiceCollection AddMyControllers(
             this IServiceCollection services)
        {
            var classCollection = Assembly.GetExecutingAssembly()
                            .DefinedTypes.Where(t => 
                                t.IsSubclassOf(typeof(ControllerBase)) &&
                                t.IsPublic && 
                                !t.IsAbstract);

            foreach (var classType in classCollection)
            {
                if (services.Any(s => 
                    s.ServiceType == classType && 
                    s.Lifetime == ServiceLifetime.Scoped)){
                    continue;
                }

                services.AddScoped(classType);
            }

            return services;
        }
    }
}