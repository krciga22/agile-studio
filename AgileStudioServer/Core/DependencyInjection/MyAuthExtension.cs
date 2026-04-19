using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyAuthExtension
    {
        public static IServiceCollection AddMyAuth(
             this IServiceCollection services, IConfiguration config)
        {
            string auth0Domain = config.GetValue<string>("AUTH0_DOMAIN") ?? "";
            string auth0ClientId = config.GetValue<string>("AUTH0_CLIENT_ID") ?? "";
            string auth0ClientSecret = config.GetValue<string>("AUTH0_CLIENT_SECRET") ?? "";
            string auth0Audience = config.GetValue<string>("AUTH0_AUDIENCE") ?? "";

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://{auth0Domain}";
                    options.Audience = auth0Audience;
                });

            services.AddAuthorization(options =>
            {
                var policyBuilder = new AuthorizationPolicyBuilder();
                policyBuilder.AddAuthenticationSchemes(new string[] {
                        JwtBearerDefaults.AuthenticationScheme
                    })
                    .RequireAuthenticatedUser();
                options.DefaultPolicy = policyBuilder.Build();
            });

            return services;
        }
    }
}