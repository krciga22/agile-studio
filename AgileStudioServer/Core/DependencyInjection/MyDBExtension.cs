

using AgileStudioServer.Data;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyDBExtension
    {
        public static IServiceCollection AddMyDB(
             this IServiceCollection services,
             IConfiguration configuration)
        {
            services.AddDbContext<DBContext>(optionsBuilder => {
                DBContextFactory.ConfigureDefaultOptions(ref optionsBuilder, configuration);
            });

            return services;
        }
    }
}