using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgileStudioServer.Features.Projects.Projects
{
    public class ProjectRepository : EntityRepository<DBContext, ProjectModel, Project, int>
    {
        public ProjectRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(ProjectModel model)
        {
            return model.ID;
        }

        public virtual PaginationResults<ProjectModel> GetAll(ServiceContext serviceContext)
        {
            IQueryable<Project> query = _DBContext.Project;

            query = ApplySearchToQuery(query, serviceContext);

            // todo apply filters to query

            int total = query.Count();

            query = ApplySortToQuery(query, serviceContext);

            return GetPaginationResultsFromQuery(query, serviceContext, total);
        }

        /// <summary>
        /// Get projects readable by the current user.
        /// </summary>
        public virtual PaginationResults<ProjectModel> GetProjectsForCurrentUser(ServiceContext serviceContext)
        {
            var currentUserId = serviceContext.GetCurrentUserIdStrict();

            var query =
                from project in _DBContext.Project
                join grant in _DBContext.RoleGrant on project.ID.ToString() equals grant.ScopeID
                join rolePerm in _DBContext.RolePermission on grant.RoleKey equals rolePerm.RoleKey
                where grant.SubjectType == RoleSubjectTypes.USER
                    && grant.SubjectID == currentUserId.ToString()
                    && grant.Scope == PermissionScopes.PROJECTS_PROJECT
                    && rolePerm.PermissionKey == PermissionKeys.PROJECTS_PROJECT_READ
                select project;

            query = ApplySearchToQuery(query, serviceContext);

            // todo apply filters to query

            int total = query.Count();

            query = ApplySortToQuery(query, serviceContext);

            return GetPaginationResultsFromQuery(query, serviceContext, total);
        }

        protected override DbSet<Project> GetDbSet()
        {
            return _DBContext.Project;
        }

        private static IQueryable<Project> ApplySearchToQuery(IQueryable<Project> query, ServiceContext serviceContext)
        {
            if (!string.IsNullOrWhiteSpace(serviceContext.SearchQuery)){
                string searchLower = serviceContext.SearchQuery.ToLower();
                query = query.Where(p => p.Title.ToLower().Contains(searchLower));
            }

            return query;
        }

        private static IOrderedQueryable<Project> ApplySortToQuery(IQueryable<Project> query, ServiceContext serviceContext)
        {
            IOrderedQueryable<Project>? result = null;

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

                    Expression<Func<Project, string>> sortKeySelector = p => ((DateTimeOffset)p.CreatedOn).ToUnixTimeSeconds().ToString();
                    switch (sortField)
                    {
                        case "title":
                            sortKeySelector = p => p.Title;
                            break;
                        case "id":
                            sortKeySelector = p => p.ID.ToString();
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
                Expression<Func<Project, string>> sortKeySelector = p => p.ID.ToString();
                result = query.OrderByDescending(sortKeySelector);
            }

            return result;
        }

        private PaginationResults<ProjectModel> GetPaginationResultsFromQuery(IQueryable<Project> query, ServiceContext serviceContext, int total)
        {
            int page = serviceContext.Page;
            int pageSize = serviceContext.ItemsPerPage;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<Project> entities = query.ToList();
            List<ProjectModel> models = HydrateModels(entities);
            return new PaginationResults<ProjectModel>(models, total, page, pageSize);
        }
    }
}
