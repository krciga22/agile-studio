using AgileStudioServer.Core.Middleware;
using AgileStudioServer.CoreFeatures.Users.Users;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace AgileStudioServer.CoreFeatures.Auth.Auth
{
    /// <summary>
    /// Adds a ClaimsIdentity for the currently authenticated user.
    /// 
    /// Should be registered after the authentication middleware.
    /// </summary>
    public class AddCurrentUserClaimsIdentityMiddleware(RequestDelegate next) : AbstractMiddleware
    {
        public async Task InvokeAsync(HttpContext context, UserService userService, IConfiguration Config)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim)){
                    throw new Exception("Authenticated user does not have a NameIdentifier claim");
                }

                var user = userService.GetByAuthServerUserId(userIdClaim);
                if (user == null)
                {
                    string auth0Domain = Config.GetValue<string>("AUTH0_DOMAIN") ?? "";
                    var client = new AuthenticationApiClient(new Uri($"https://{auth0Domain}/"));

                    // todo determine if we can get access token differently (don't want to double authenticate)
                    var accessToken = context.GetTokenAsync("access_token").Result;
                    if (string.IsNullOrEmpty(accessToken)){
                        throw new Exception("Authenticated user does not have a NameIdentifier claim");
                    }

                    UserInfo userInfo = await client.GetUserInfoAsync(accessToken);

                    user = userService.Create(
                        new UserModel(userInfo.Email, userInfo.FirstName, userInfo.LastName)
                        {
                            AuthServerUserID = userInfo.UserId
                        }
                    );
                }

                var identity = new CurrentUserClaimsIdentity();
                identity.AddUserIdClaim(user.ID);
                context.User.AddIdentity(identity);
            }

            await next(context);
        }
    }
}
