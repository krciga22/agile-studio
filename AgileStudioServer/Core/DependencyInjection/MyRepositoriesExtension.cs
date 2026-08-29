using AgileStudioServer.Core.Data;
using AgileStudioServer.Core.Repositories;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyRepositoriesExtension
    {
        public static IServiceCollection AddMyRepositories(
             this IServiceCollection services)
        {
            var classCollection = Assembly.GetExecutingAssembly()
                            .DefinedTypes.Where(t =>
                                t.IsSubclassOf(typeof(Repository)) &&
                                t.IsPublic &&
                                !t.IsAbstract);

            foreach (var classType in classCollection)
            {
                services.AddScoped(classType);
                services.AddScoped(typeof(Repository), classType);
            }

            services.AddScoped(typeof(TransactionService));

            return services;
        }
    }
}