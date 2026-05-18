using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgileStudioServer.Features.Projects.Sprints
{
    public class SprintRepository : EntityRepository<DBContext, SprintModel, Sprint, int>
    {
        public SprintRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {
        }

        public override int GetIdentifier(SprintModel model)
        {
            return model.ID;
        }

        public virtual PaginationResults<SprintModel> GetByProjectId(int projectId, ServiceContext serviceContext)
        {
            var query =
                (from project in _DBContext.Project
                 join sprint in _DBContext.Sprint on project.ID equals sprint.ProjectID
                 where project.ID == projectId
                 select sprint)
                .Distinct();

            query = ApplySearchToQuery(query, serviceContext);

            int total = query.Count();

            query = ApplySortToQuery(query, serviceContext);

            return GetPaginationResultsFromQuery(query, serviceContext, total);
        }

        public int GetNextSprintNumber()
        {
            return GetLastSprintNumber() + 1;
        }

        protected override DbSet<Sprint> GetDbSet()
        {
            return _DBContext.Sprint;
        }

        private int GetLastSprintNumber()
        {
            var lastSprint = _DBContext.Sprint
                .OrderByDescending(sprint => sprint.SprintNumber)
                .FirstOrDefault();

            return lastSprint?.SprintNumber ?? 0;
        }

        private static IQueryable<Sprint> ApplySearchToQuery(IQueryable<Sprint> query, ServiceContext serviceContext)
        {
            if (!string.IsNullOrWhiteSpace(serviceContext.SearchQuery))
            {
                string searchLower = serviceContext.SearchQuery.ToLower();
                query = query.Where(sprint => sprint.SprintNumber.ToString().Equals(searchLower));
            }

            return query;
        }

        private static IOrderedQueryable<Sprint> ApplySortToQuery(IQueryable<Sprint> query, ServiceContext serviceContext)
        {
            IOrderedQueryable<Sprint>? result = null;

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

                    Expression<Func<Sprint, dynamic>> sortKeySelector = item =>
                        ((DateTimeOffset)item.CreatedOn).ToUnixTimeSeconds().ToString();
                    switch (sortField)
                    {
                        case "number":
                            sortKeySelector = item => item.SprintNumber;
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
                Expression<Func<Sprint, string>> sortKeySelector = item => item.ID.ToString();
                result = query.OrderByDescending(sortKeySelector);
            }

            return result;
        }

        private PaginationResults<SprintModel> GetPaginationResultsFromQuery(IQueryable<Sprint> query, ServiceContext serviceContext, int total)
        {
            int page = serviceContext.Page;
            int pageSize = serviceContext.ItemsPerPage;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<Sprint> entities = query.ToList();
            List<SprintModel> models = HydrateModels(entities);
            return new PaginationResults<SprintModel>(models, total, page, pageSize);
        }
    }
}
