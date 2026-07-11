using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgileStudioServer.Features.Accounts.WorkflowStates
{
    public class WorkflowStateRepository : EntityRepository<DBContext, WorkflowStateModel, WorkflowState, int>
    {
        public WorkflowStateRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(WorkflowStateModel model)
        {
            return model.ID;
        }

        public virtual PaginationResults<WorkflowStateModel> GetByWorkflowId(int workflowId, ServiceContext serviceContext)
        {
            var query =
                (from workflow in _DBContext.Workflow
                 join workflowState in _DBContext.WorkflowState on workflow.ID equals workflowState.WorkflowID
                 where workflow.ID == workflowId
                 select workflowState)
                .Distinct();

            query = ApplySearchToQuery(query, serviceContext);

            int total = query.Count();

            query = ApplySortToQuery(query, serviceContext);

            return GetPaginationResultsFromQuery(query, serviceContext, total);
        }

        protected override DbSet<WorkflowState> GetDbSet()
        {
            return _DBContext.WorkflowState;
        }

        private static IQueryable<WorkflowState> ApplySearchToQuery(IQueryable<WorkflowState> query, ServiceContext serviceContext)
        {
            if (!string.IsNullOrWhiteSpace(serviceContext.SearchQuery))
            {
                string searchLower = serviceContext.SearchQuery.ToLower();
                query = query.Where(workflowState => workflowState.Title.ToLower().Contains(searchLower));
            }

            return query;
        }

        private static IOrderedQueryable<WorkflowState> ApplySortToQuery(
            IQueryable<WorkflowState> query, ServiceContext serviceContext)
        {
            IOrderedQueryable<WorkflowState>? result = null;

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

                    Expression<Func<WorkflowState, string>> sortKeySelector = item =>
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
                Expression<Func<WorkflowState, string>> sortKeySelector = item => item.ID.ToString();
                result = query.OrderByDescending(sortKeySelector);
            }

            return result;
        }

        private PaginationResults<WorkflowStateModel> GetPaginationResultsFromQuery(
            IQueryable<WorkflowState> query, ServiceContext serviceContext, int total)
        {
            int page = serviceContext.Page;
            int pageSize = serviceContext.ItemsPerPage;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<WorkflowState> entities = query.ToList();
            List<WorkflowStateModel> models = HydrateModels(entities);
            return new PaginationResults<WorkflowStateModel>(models, total, page, pageSize);
        }
    }
}
