using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace AgileStudioServer.CoreFeatures.Auth.Permissions
{
    public class PermissionRepository(DBContext dbContext, Hydrator hydrator) :
        EntityRepository<DBContext, PermissionModel, Permission, int>(dbContext, hydrator)
    {
        public override int GetIdentifier(PermissionModel model)
        {
            return model.ID;
        }

        public PermissionModel? GetByPermissionKey(string permissionKey)
        {
            var entity = GetDbSet().Where(p => p.PermissionKey == permissionKey).FirstOrDefault();
            if (entity == null){
                return null;
            }

            return HydrateModel(entity);
        }

        public virtual PaginationResults<PermissionModel> GetAll(ServiceContext serviceContext)
        {
            IQueryable<Permission> query = _DBContext.Permission;

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

            if (sortedFieldsCount == 0)
            {
                query = query.OrderByDescending(p => p.ID);
            }

            int page = serviceContext.Page;
            int pageSize = serviceContext.ItemsPerPage;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<Permission> entities = query.ToList();
            List<PermissionModel> models = HydrateModels(entities);
            return new PaginationResults<PermissionModel>(models, total, page, pageSize);
        }

        protected override DbSet<Permission> GetDbSet()
        {
            return _DBContext.Permission;
        }
    }
}