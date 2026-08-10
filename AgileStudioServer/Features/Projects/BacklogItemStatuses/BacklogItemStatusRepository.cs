using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgileStudioServer.Features.Projects.BacklogItemStatuses
{
    public class BacklogItemStatusRepository : EntityRepository<DBContext, BacklogItemStatusModel, BacklogItemStatus, int>
    {
        private readonly ServiceContext _serviceContext;

        public BacklogItemStatusRepository(
            DBContext dbContext, 
            Hydrator hydrator,
            ServiceContext serviceContext
            ) : base(dbContext, hydrator)
        {
            _serviceContext = serviceContext;
        }

        public override int GetIdentifier(BacklogItemStatusModel model)
        {
            return model.ID;
        }

        public virtual PaginationResults<BacklogItemStatusModel> GetByBacklogItemId(int backlogItemId)
        {
            var query =
                (from status in _DBContext.BacklogItemStatus
                 where status.BacklogItemID == backlogItemId
                 select status)
                .Distinct();

            query = ApplySearchToQuery(query);

            int total = query.Count();

            query = ApplySortToQuery(query);

            return GetPaginationResultsFromQuery(query, total);
        }

        public virtual BacklogItemStatusModel GetLatestForBacklogItemId(int backlogItemId)
        {
            var query =
                (from status in _DBContext.BacklogItemStatus
                 where status.BacklogItemID == backlogItemId
                 orderby status.ID descending
                 select status);

            return _Hydrator.Hydrate<BacklogItemStatusModel>(query.First());
        }        

        protected override DbSet<BacklogItemStatus> GetDbSet()
        {
            return _DBContext.BacklogItemStatus;
        }

        private IQueryable<BacklogItemStatus> ApplySearchToQuery(
            IQueryable<BacklogItemStatus> query)
        {
            if (!string.IsNullOrWhiteSpace(_serviceContext.SearchQuery))
            {
                string searchLower = _serviceContext.SearchQuery.ToLower();
                query = query.Where(status =>
                    (status.Comment ?? string.Empty).ToLower().Contains(searchLower) ||
                    (status.WorkflowState.Title ?? string.Empty).ToLower().Contains(searchLower));
            }

            return query;
        }

        private IOrderedQueryable<BacklogItemStatus> ApplySortToQuery(
            IQueryable<BacklogItemStatus> query)
        {
            IOrderedQueryable<BacklogItemStatus>? result = null;

            if (!string.IsNullOrWhiteSpace(_serviceContext.Sort))
            {
                string[] sorts = _serviceContext.Sort.Split(',', StringSplitOptions.RemoveEmptyEntries);
                int sortedFieldsCount = 0;
                foreach (string sort in sorts)
                {
                    sortedFieldsCount++;

                    string[] sortParts = sort.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    string sortField = sortParts[0];
                    bool descending = sortParts.Length > 1 && sortParts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

                    Expression<Func<BacklogItemStatus, string>> sortKeySelector = item =>
                        ((DateTimeOffset)item.CreatedOn).ToUnixTimeSeconds().ToString();

                    switch (sortField)
                    {
                        case "createdOn":
                            sortKeySelector = item => ((DateTimeOffset)item.CreatedOn).ToUnixTimeSeconds().ToString();
                            break;
                        case "id":
                            sortKeySelector = item => item.ID.ToString();
                            break;
                        default:
                            sortedFieldsCount--;
                            break;
                    }

                    if (result == null)
                    {
                        result = descending ?
                            query.OrderByDescending(sortKeySelector) :
                            query.OrderBy(sortKeySelector);
                    }
                    else
                    {
                        result = descending ?
                            result.ThenByDescending(sortKeySelector) :
                            result.ThenBy(sortKeySelector);
                    }
                }
            }

            if (result == null)
            {
                Expression<Func<BacklogItemStatus, string>> sortKeySelector = item => item.ID.ToString();
                result = query.OrderByDescending(sortKeySelector);
            }

            return result;
        }

        private PaginationResults<BacklogItemStatusModel> GetPaginationResultsFromQuery(
            IQueryable<BacklogItemStatus> query, int total)
        {
            int page = _serviceContext.Page;
            int pageSize = _serviceContext.ItemsPerPage;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<BacklogItemStatus> entities = query.ToList();
            List<BacklogItemStatusModel> models = HydrateModels(entities);
            return new PaginationResults<BacklogItemStatusModel>(models, total, page, pageSize);
        }
    }
}