

using AgileStudioServer.Data;
using AgileStudioServerTest.Data;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyTestDBExtension
    {
        public static IServiceCollection AddMyTestDB(
             this IServiceCollection services)
        {
            services.AddDbContext<DBContext>(optionsBuilder => {
                TestDBContextFactory.ConfigureDefaultOptions(ref optionsBuilder);
            });

            return services;
        }
    }
}