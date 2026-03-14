
/// <summary>
/// Standard query parameters for collection endpoints.
/// </summary>
public class GetCollectionQueryParams
{
    public int? Page { get; set; }
    public int? ItemsPerPage { get; set; }
    public string? Sort { get; set; }
    public string? SearchQuery { get; set; }
}