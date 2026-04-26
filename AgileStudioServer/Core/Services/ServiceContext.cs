using AgileStudioServer.Core.APIs;
using AgileStudioServer.Core.Services.Exceptions;
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

        /// <summary>
        /// Gets the current user's ID from their claims or throws an exception.
        /// </summary>
        /// <exception cref="CurrentUserNotFoundException"></exception>
        public int GetCurrentUserIdStrict()
        {
            CurrentUserClaimsIdentity? claimsIdentity = GetCurrentUserClaimsIdentity() ?? 
                throw new CurrentUserNotFoundException();

            int currentUserId = claimsIdentity.GetUserIdClaimValue() ??
                throw new CurrentUserNotFoundException();

            return currentUserId;
        }

        /// <summary>
        /// Gets the current user's ID from their claims or null.
        /// </summary>
        public int? GetCurrentUserId()
        {
            CurrentUserClaimsIdentity? claimsIdentity = GetCurrentUserClaimsIdentity();
            if (claimsIdentity == null){
                return null;
            }

            return claimsIdentity.GetUserIdClaimValue();
        }

        private CurrentUserClaimsIdentity? GetCurrentUserClaimsIdentity()
        {
            CurrentUserClaimsIdentity? currentUserClaimsIdentity = null;
            foreach (ClaimsIdentity identity in currentUser?.Identities ?? [])
            {
                if (identity is CurrentUserClaimsIdentity claimsIdentity)
                {
                    currentUserClaimsIdentity = claimsIdentity;
                    break;
                }
            }

            return currentUserClaimsIdentity;
        }
    }
}