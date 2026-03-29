using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.Auth.Roles
{
    public class RoleRepository(DBContext dbContext, Hydrator hydrator) : 
        EntityRepository<DBContext, RoleModel, Role, int>(dbContext, hydrator)
    {
        public override int GetIdentifier(RoleModel model)
        {
            return model.ID;
        }

        public RoleModel? GetByUUID(string uuid)
        {
            var entity = GetDbSet().Where(p => p.UUID == uuid).FirstOrDefault();
            if (entity == null){
                return null;
            }

            return HydrateModel(entity);
        }

        public virtual PaginationResults<RoleModel> GetAll(ServiceContext serviceContext)
        {
            IQueryable<Role> query = _DBContext.Role;

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

            List<Role> entities = query.ToList();
            List<RoleModel> models = HydrateModels(entities);
            return new PaginationResults<RoleModel>(models, total, page, pageSize);
        }

        protected override DbSet<Role> GetDbSet()
        {
            return _DBContext.Role;
        }
    }
}
