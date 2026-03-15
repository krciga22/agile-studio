using AgileStudioServer.Core.Pagination;

namespace AgileStudioServer.Core.APIs.DTOs
{
    public class PaginatedResults2Dto<T>
        where T : class
    {
        public List<T> Items { get; set; }

        public int Total { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }

        public int? PrevPage { get; set; }

        public int? NextPage { get; set; }

        public static PaginatedResults2Dto<T> FromPaginatedResults(PaginationResults<T> paginationResults)
        {
            return new PaginatedResults2Dto<T>(paginationResults.Items)
            {
                Total = paginationResults.Total,
                Page = paginationResults.Page,
                TotalPages = paginationResults.TotalPages,
                PrevPage = paginationResults.PrevPage,
                NextPage = paginationResults.NextPage
            };
        }

        public PaginatedResults2Dto(List<T> items)
        {
            Items = items;
        }
    }
}
