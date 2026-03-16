using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.CoreFeatures.Resources.Resource;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.Projects.Projects
{
    public class ProjectRepository : EntityRepository<DBContext, ProjectModel, Project, int>, IResourceRepository
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

            if (!string.IsNullOrWhiteSpace(serviceContext.SearchQuery))
            {
                string searchLower = serviceContext.SearchQuery.ToLower();
                query = query.Where(p => p.Title.ToLower().Contains(searchLower));
            }

            int total = query.Count();

            int sortedFieldsCount = 0;
            if (!string.IsNullOrWhiteSpace(serviceContext.Sort))
            {
                var sorts = serviceContext.Sort.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var sort in sorts)
                {
                    sortedFieldsCount++;

                    string[] sortParts = sort.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    string sortField = sortParts[0];
                    bool descending = sortParts.Length > 1 && sortParts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);
                    switch (sortField)
                    {
                        case "title":
                            query = descending ? 
                                query.OrderByDescending(p => p.Title) : 
                                query.OrderBy(p => p.Title);
                            break;
                        case "id":
                            query = descending ? 
                                query.OrderByDescending(p => p.ID) : 
                                query.OrderBy(p => p.ID);
                            break;
                        default:
                            sortedFieldsCount--;
                            break;
                    }
                }
            }

            if(sortedFieldsCount == 0)
            {
                query = query.OrderByDescending(p => p.ID);
            }

            int page = serviceContext.Page;
            int pageSize = serviceContext.ItemsPerPage;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<Project> entities = query.ToList();
            List<ProjectModel> models = HydrateModels(entities);
            return new PaginationResults<ProjectModel>(models, total, page, pageSize);
        }

        public virtual List<ProjectModel> GetByCreatedByUserId(int userId)
        {
            List<Project> entities = _DBContext.Project.Where(project =>
                project.CreatedBy != null && project.CreatedBy.ID == userId).ToList();

            return HydrateModels(entities);
        }

        protected override DbSet<Project> GetDbSet()
        {
            return _DBContext.Project;
        }

        public bool IsTypeSupported(string type)
        {
            return type.Equals("projects.project", StringComparison.OrdinalIgnoreCase);
        }

        public PaginationResults<object> GetAllResources(ServiceContext serviceContext)
        {
            var result = GetAll(serviceContext);
            var dtos = _Hydrator.HydrateList<ProjectDto>(result.Items);
            return new PaginationResults<object>(
                [.. dtos.Cast<object>()],
                result.Total,
                result.Page,
                result.ItemsPerPage
            );
        }

        public object? GetResource(int id, ServiceContext serviceContext)
        {
            var model = Get(id);
            return model != null ? _Hydrator.Hydrate<ProjectDto>(model) : null;
        }
    }
}
