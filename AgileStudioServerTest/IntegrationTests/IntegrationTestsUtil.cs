using AgileStudioServer.Features.Auth.Auth;
using System.Security.Claims;
using System.Text.Json;

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

        public static object ConvertDtoToObject(object someObject)
        {
            var jsonSerializerOptions = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };
            String jsonString = JsonSerializer.Serialize(someObject, jsonSerializerOptions);
            object? jsonObject = JsonSerializer.Deserialize<object>(jsonString, jsonSerializerOptions);
            if(jsonObject == null){
                throw new Exception("Deserialization resulted in null");
            }

            return jsonObject;
        }
    }
}
