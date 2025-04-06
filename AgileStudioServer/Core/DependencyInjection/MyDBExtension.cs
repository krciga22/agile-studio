

using AgileStudioServer.Data;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyDBExtension
    {
        public static IServiceCollection AddMyDB(
             this IServiceCollection services)
        {
            services.AddDbContext<DBContext>(optionsBuilder => {
                DBContextFactory.ConfigureDefaultOptions(ref optionsBuilder);
            });

            return services;
        }
    }
}