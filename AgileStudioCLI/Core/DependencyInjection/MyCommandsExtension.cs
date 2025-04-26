using AgileStudioCLI.Commands;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyCommandsExtension
    {
        public static IServiceCollection AddMyCommands(
             this IServiceCollection services)
        {
            var classCollection = Assembly.GetExecutingAssembly()
                            .DefinedTypes.Where(t =>
                                t.IsSubclassOf(typeof(AbstractCommand)) &&
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