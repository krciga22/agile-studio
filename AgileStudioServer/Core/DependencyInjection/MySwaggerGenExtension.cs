using AgileStudioServer;
using Microsoft.OpenApi.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MySwaggerGenExtension
    {
        public static IServiceCollection AddMySwaggerGen(
             this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(options =>
            {
                string majorVersion = Constants.ProductVersion.Split('.')[0];
                options.SwaggerDoc($"v{majorVersion}", new OpenApiInfo { 
                    Title = Constants.ProductName, 
                    Version = $"v{Constants.ProductVersion}"
                });
            });

            return services;
        }
    }
}