using AgileStudioServer.Features.Auth.Scopes;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyParentScopeResolversExtension
    {
        public static IServiceCollection AddMyParentScopeResolvers(
             this IServiceCollection services)
        {
            var classCollection = Assembly.GetExecutingAssembly()
                            .DefinedTypes.Where(t =>
                                t.ImplementedInterfaces.Contains(typeof(IParentScopeResolver)) &&
                                t.IsPublic && !t.IsAbstract);

            foreach (var classType in classCollection){
                services.AddScoped(typeof(IParentScopeResolver), classType);
            }

            return services;
        }
    }
}