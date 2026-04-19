using System.Security.Claims;

namespace AgileStudioServer.Features.Auth.Auth
{
    public class CurrentUserClaimsIdentity : ClaimsIdentity
    {
        public void AddUserIdClaim(int userId)
        {
            AddClaim(new Claim("userId", userId.ToString()));
        }

        public int? GetUserIdClaimValue()
        {
            var claim = FindFirst("userId");
            _ = int.TryParse(claim?.Value, out int userId);
            return userId;
        }
    }
}
