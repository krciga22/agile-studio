using AgileStudioServer.Features.Users.Users;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Features.Auth.Auth
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration Config;
        private readonly UserService _UserService;

        public IHostEnvironment HostEnvironment { get; }

        public AuthController(IHostEnvironment hostEnvironment, IConfiguration config, UserService userService)
        {
            HostEnvironment = hostEnvironment;
            Config = config;
            _UserService = userService;
        }

        /// <summary>
        /// Get the currently authenticated user's 
        /// basic details.
        /// </summary>
        [HttpGet("CurrentUser", Name = "AuthGetCurrentUser")]
        [ProducesResponseType(typeof(ForbidResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(CurrentUserDto), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            if (HttpContext.User.Identity == null || 
                !HttpContext.User.Identity.IsAuthenticated)
            {
                return Forbid();
            }

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if(accessToken == null)
            {
                return Forbid();
            }

            try
            {
                string auth0Domain = Config.GetValue<string>("AUTH0_DOMAIN") ?? "";
                var client = new AuthenticationApiClient(new Uri($"https://{auth0Domain}/"));

                UserInfo userInfo = await client.GetUserInfoAsync(accessToken);

                UserModel? user = null;
                if (userInfo.UserId != null){
                    user = _UserService.GetByAuthServerUserId(userInfo.UserId);
                }
                else if (userInfo.Email != null)
                {
                    user = _UserService.GetByEmail(userInfo.Email);
                }
                else
                {
                    return Forbid();
                }

                user ??= _UserService.Create(
                    new UserModel(userInfo.Email, userInfo.FirstName, userInfo.LastName)
                    {
                        AuthServerUserID = userInfo.UserId
                    }
                );

                var currentUserDto = new CurrentUserDto(userInfo.FullName, user.FirstName, user.LastName);
                return Ok(currentUserDto);
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}