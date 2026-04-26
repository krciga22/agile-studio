using AgileStudioServer.Features.Auth.Auth;
using System.Security.Claims;

namespace AgileStudioServerTest.IntegrationTests
{
    internal class IntegrationTestsUtil
    {
        /// <summary>
        /// Generates a claims principal with a CurrentUserClaimsIdentity for testing.
        /// </summary>
        public static ClaimsPrincipal GenerateCurrentUserClaimsPrincipal(
            int userId, string authenticationType = "testing")
        {
            CurrentUserClaimsIdentity currentUserClaimsIdentity = new(authenticationType);
            currentUserClaimsIdentity.AddUserIdClaim(userId);

            IEnumerable<ClaimsIdentity> identities = [
                currentUserClaimsIdentity
            ];

            return new ClaimsPrincipal(identities);
        }
    }
}
