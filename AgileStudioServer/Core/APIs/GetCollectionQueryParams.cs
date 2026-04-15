
namespace AgileStudioServer.Core.APIs
{
    /// <summary>
    /// Standard query parameters for collection endpoints.
    /// </summary>
    public class GetCollectionQueryParams
    {
        public int? Page { get; set; }
        public int? ItemsPerPage { get; set; }
        public string? Sort { get; set; }
        public string? SearchQuery { get; set; }

        public static GetCollectionQueryParams FromHttpRequest(HttpRequest httpRequest)
        {
            var getCollectionQueryParams = new GetCollectionQueryParams();

            var query = httpRequest.Query;
            var queryDictionary = query.ToDictionary();

            if (queryDictionary.ContainsKey("page") &&
                int.TryParse(queryDictionary["page"], out int page))
            {
                getCollectionQueryParams.Page = page;
            }

            if (queryDictionary.ContainsKey("itemsPerPage") &&
                int.TryParse(queryDictionary["itemsPerPage"], out int itemsPerPage))
            {
                getCollectionQueryParams.ItemsPerPage = itemsPerPage;
            }

            if (queryDictionary.ContainsKey("sort"))
            {
                getCollectionQueryParams.Sort = queryDictionary["sort"];
            }

            if (queryDictionary.ContainsKey("searchQuery"))
            {
                getCollectionQueryParams.SearchQuery = queryDictionary["searchQuery"];
            }

            return getCollectionQueryParams;
        }
    }
}