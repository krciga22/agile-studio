using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyAuthExtension
    {
        public static IServiceCollection AddMyAuth(
             this IServiceCollection services, IConfiguration config)
        {
            string auth0Domain = config.GetValue<string>("Auth0:Domain") ?? "";
            string auth0ClientId = config.GetValue<string>("Auth0:ClientId") ?? "";
            string auth0ClientSecret = config.GetValue<string>("Auth0:ClientSecret") ?? "";
            string auth0Audience = config.GetValue<string>("Auth0:Audience") ?? "";

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://{auth0Domain}";
                    options.Audience = auth0Audience;
                })
                .AddAuth0WebAppAuthentication(options =>
                {
                    options.Domain = auth0Domain;
                    options.ClientId = auth0ClientId;
                    options.ClientSecret = auth0ClientSecret;
                    options.CallbackPath = "/Auth/Callback";
                })
                .WithAccessToken(options =>
                {
                    options.Audience = auth0Audience;
                    options.UseRefreshTokens = false;
                });

            services.AddAuthorization(options =>
            {
                var policyBuilder = new AuthorizationPolicyBuilder();
                policyBuilder.AddAuthenticationSchemes(new string[] {
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        JwtBearerDefaults.AuthenticationScheme
                    })
                    .RequireAuthenticatedUser();
                options.DefaultPolicy = policyBuilder.Build();
            });

            return services;
        }
    }
}