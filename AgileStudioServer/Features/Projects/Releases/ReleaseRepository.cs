using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Projects.Projects;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgileStudioServer.Features.Projects.Releases
{
    public class ReleaseRepository : EntityRepository<DBContext, ReleaseModel, Release, int>
    {
        public ReleaseRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(ReleaseModel model)
        {
            return model.ID;
        }

        public virtual PaginationResults<ReleaseModel> GetByProjectId(int projectId, ServiceContext serviceContext)
        {
            var query =
                (from project in _DBContext.Project
                 join release in _DBContext.Release on project.ID equals release.ProjectID
                 where project.ID == projectId
                 select release)
                .Distinct();

            query = ApplySearchToQuery(query, serviceContext);

            int total = query.Count();

            query = ApplySortToQuery(query, serviceContext);

            return GetPaginationResultsFromQuery(query, serviceContext, total);
        }

        protected override DbSet<Release> GetDbSet()
        {
            return _DBContext.Release;
        }

        private static IQueryable<Release> ApplySearchToQuery(IQueryable<Release> query, ServiceContext serviceContext)
        {
            if (!string.IsNullOrWhiteSpace(serviceContext.SearchQuery))
            {
                string searchLower = serviceContext.SearchQuery.ToLower();
                query = query.Where(release => release.Title.ToLower().Contains(searchLower));
            }

            return query;
        }

        private static IOrderedQueryable<Release> ApplySortToQuery(IQueryable<Release> query, ServiceContext serviceContext)
        {
            IOrderedQueryable<Release>? result = null;

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

                    Expression<Func<Release, string>> sortKeySelector = item => 
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
                Expression<Func<Release, string>> sortKeySelector = item => item.ID.ToString();
                result = query.OrderByDescending(sortKeySelector);
            }

            return result;
        }

        private PaginationResults<ReleaseModel> GetPaginationResultsFromQuery(IQueryable<Release> query, ServiceContext serviceContext, int total)
        {
            int page = serviceContext.Page;
            int pageSize = serviceContext.ItemsPerPage;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<Release> entities = query.ToList();
            List<ReleaseModel> models = HydrateModels(entities);
            return new PaginationResults<ReleaseModel>(models, total, page, pageSize);
        }
    }
}
