using AgileStudioServer.CoreFeatures.Auth.APIs.DTOs;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.CoreFeatures.Auth.APIs
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration Config;

        public IHostEnvironment HostEnvironment { get; }

        public AuthController(IHostEnvironment hostEnvironment, IConfiguration config)
        {
            HostEnvironment = hostEnvironment;
            Config = config;
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
                var name = userInfo.FullName;
                var currentUserDto = new CurrentUserDto(name);
                return Ok(currentUserDto);
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }
    }
}