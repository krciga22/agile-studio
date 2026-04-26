using AgileStudioServer.Core.APIs;
using AgileStudioServer.Features.Auth.Auth;
using System.Security.Claims;

namespace AgileStudioServer.Core.Services
{
    public class ServiceContext()
    {
        public int Page { get; set; } = Constants.DefaultPage;

        public int ItemsPerPage { get; set; } = Constants.ItemsPerPage; // all items

        public string? SearchQuery { get; set; }

        public string? Sort { get; set; }

        public int HydratorDepth { get; set; } = 2;

        public ClaimsPrincipal? currentUser { get; set; } = null!;

        public void WithGetCollectionQueryParams(GetCollectionQueryParams getCollectionQueryParams)
        {
            Page = getCollectionQueryParams.Page ?? Constants.DefaultPage;

            int pageSize = getCollectionQueryParams.ItemsPerPage ?? Constants.ItemsPerPage;
            if (pageSize > Constants.MaxItemsPerPage)
            {
                pageSize = Constants.MaxItemsPerPage;
            }
            ItemsPerPage = pageSize;

            SearchQuery = getCollectionQueryParams.SearchQuery;

            Sort = getCollectionQueryParams.Sort;
        }

        public int? GetCurrentUserId()
        {
            CurrentUserClaimsIdentity? currentUserIdentity = null;
            foreach (ClaimsIdentity identity in currentUser?.Identities ?? [])
            {
                if (identity is CurrentUserClaimsIdentity claimsIdentity){
                    currentUserIdentity = claimsIdentity;
                    break;
                }
            }

            if (currentUserIdentity == null){
                return null;
            }

            return currentUserIdentity.GetUserIdClaimValue() ??
                throw new Exception("User ID claim value is null");
        }
    }
}