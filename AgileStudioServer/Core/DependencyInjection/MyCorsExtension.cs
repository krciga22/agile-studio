
using AgileStudioServer;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyCorsExtension
    {
        public static IServiceCollection AddMyCors(
             this IServiceCollection services, IConfiguration config)
        {
            string[] corsOrigins = config.GetSection("CORS_ORIGINS")
                .Get<string[]>() ?? Array.Empty<string>();

            services.AddCors(options =>
            {
                options.AddPolicy(Constants.CorsPolicyDefault, policy =>
                {
                    policy.WithOrigins(corsOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            return services;
        }
    }
}