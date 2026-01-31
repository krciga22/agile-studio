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

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT Access Token (without the 'Bearer' prefix)."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}