using AgileStudioServer.Core.Command;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyCoreCommandsExtension
    {
        public static IServiceCollection AddMyCoreCommands(
             this IServiceCollection services)
        {
            services.AddScoped<CommandDispatcher>();

            var classCollection = Assembly.GetExecutingAssembly()
                .DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(ICommandHandler))
                    && t.IsPublic && !t.IsAbstract);

            foreach (var classType in classCollection){
                services.AddScoped(typeof(ICommandHandler), classType);
            }

            return services;
        }
    }
}