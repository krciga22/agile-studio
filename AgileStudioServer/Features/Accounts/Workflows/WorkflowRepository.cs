using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgileStudioServer.Features.Accounts.Workflows
{
    public class WorkflowRepository : EntityRepository<DBContext, WorkflowModel, Workflow, int>
    {
        public WorkflowRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(WorkflowModel model)
        {
            return model.ID;
        }

        public virtual PaginationResults<WorkflowModel> GetByAccountID(
            int accountId, ServiceContext serviceContext)
        {
            var query =
                (from account in _DBContext.Account
                 join workflow in _DBContext.Workflow on account.ID equals workflow.AccountID
                 where account.ID == accountId
                 select workflow)
                .Distinct();

            query = ApplySearchToQuery(query, serviceContext);

            int total = query.Count();

            query = ApplySortToQuery(query, serviceContext);

            return GetPaginationResultsFromQuery(query, serviceContext, total);
        }

        protected override DbSet<Workflow> GetDbSet()
        {
            return _DBContext.Workflow;
        }

        private static IQueryable<Workflow> ApplySearchToQuery(IQueryable<Workflow> query, ServiceContext serviceContext)
        {
            if (!string.IsNullOrWhiteSpace(serviceContext.SearchQuery))
            {
                string searchLower = serviceContext.SearchQuery.ToLower();
                query = query.Where(workflow => workflow.Title.ToLower().Contains(searchLower));
            }

            return query;
        }

        private static IOrderedQueryable<Workflow> ApplySortToQuery(
            IQueryable<Workflow> query, ServiceContext serviceContext)
        {
            IOrderedQueryable<Workflow>? result = null;

            int sortedFieldsCount = 0;
            if (!string.IsNullOrWhiteSpace(serviceContext.Sort))
            {
                string[] sorts = serviceContext.Sort.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (string sort in sorts)
                {
                    sortedFieldsCount++;

                    string[] sortParts = sort.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    string sortField = sortParts[0];
                    bool descending = sortParts.Length > 1 && sortParts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

                    Expression<Func<Workflow, string>> sortKeySelector = item =>
                        ((DateTimeOffset)item.CreatedOn).ToUnixTimeSeconds().ToString();
                    switch (sortField)
                    {
                        case "title":
                            sortKeySelector = item => item.Title;
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
                Expression<Func<Workflow, string>> sortKeySelector = item => item.ID.ToString();
                result = query.OrderByDescending(sortKeySelector);
            }

            return result;
        }

        private PaginationResults<WorkflowModel> GetPaginationResultsFromQuery(
            IQueryable<Workflow> query, ServiceContext serviceContext, int total)
        {
            int page = serviceContext.Page;
            int pageSize = serviceContext.ItemsPerPage;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<Workflow> entities = query.ToList();
            List<WorkflowModel> models = HydrateModels(entities);
            return new PaginationResults<WorkflowModel>(models, total, page, pageSize);
        }
    }
}
