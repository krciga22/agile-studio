using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgileStudioServer.Features.Projects.BacklogItems
{
    public class BacklogItemRepository : EntityRepository<DBContext, BacklogItemModel, BacklogItem, int>
    {
        public BacklogItemRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(BacklogItemModel model)
        {
            return model.ID;
        }

        public virtual PaginationResults<BacklogItemModel> GetByProjectId(int projectId, ServiceContext serviceContext)
        {
            var query =
                (from project in _DBContext.Project
                 join backlogItem in _DBContext.BacklogItem on project.ID equals backlogItem.ProjectID
                 where project.ID == projectId
                 select backlogItem)
                .Distinct();

            query = ApplySearchToQuery(query, serviceContext);

            int total = query.Count();

            query = ApplySortToQuery(query, serviceContext);

            return GetPaginationResultsFromQuery(query, serviceContext, total);
        }

        public virtual List<BacklogItemModel> GetByProjectIdAndBacklogItemTypeId(int projectId, int backlogItemTypeId)
        {
            List<BacklogItem> entities = _DBContext.BacklogItem.Where(backlogItem =>
                backlogItem.Project.ID == projectId && backlogItem.BacklogItemType.ID == backlogItemTypeId).ToList();

            return HydrateModels(entities);
        }

        public virtual PaginationResults<BacklogItemModel> GetChildBacklogItems(int parentBacklogItemId, ServiceContext serviceContext)
        {
            var query =
                (from backlogItem in _DBContext.BacklogItem
                 join childBacklogItem in _DBContext.BacklogItem on backlogItem.ID equals childBacklogItem.ID
                 where childBacklogItem.ParentBacklogItemId == parentBacklogItemId
                 select childBacklogItem)
                .Distinct();

            query = ApplySearchToQuery(query, serviceContext);

            int total = query.Count();

            query = ApplySortToQuery(query, serviceContext);

            return GetPaginationResultsFromQuery(query, serviceContext, total);
        }

        public virtual BacklogItemModel? GetParentBacklogItem(int id)
        {
            BacklogItem? entity = _DBContext.BacklogItem.Find(id);
            if (entity is null || entity.ParentBacklogItemId is null)
            {
                return null;
            }

            BacklogItem? parentEntity =
                _DBContext.BacklogItem.Find(entity.ParentBacklogItemId);
            if (parentEntity is null)
            {
                return null;
            }

            return HydrateModel(parentEntity);
        }

        protected override DbSet<BacklogItem> GetDbSet()
        {
            return _DBContext.BacklogItem;
        }

        private static IQueryable<BacklogItem> ApplySearchToQuery(
            IQueryable<BacklogItem> query, ServiceContext serviceContext)
        {
            if (!string.IsNullOrWhiteSpace(serviceContext.SearchQuery))
            {
                string searchLower = serviceContext.SearchQuery.ToLower();
                query = query.Where(backlogItem => backlogItem.Title.ToLower().Contains(searchLower));
            }

            return query;
        }

        private static IOrderedQueryable<BacklogItem> ApplySortToQuery(
            IQueryable<BacklogItem> query, ServiceContext serviceContext)
        {
            IOrderedQueryable<BacklogItem>? result = null;

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

                    Expression<Func<BacklogItem, string>> sortKeySelector = item =>
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
                Expression<Func<BacklogItem, string>> sortKeySelector = item => item.ID.ToString();
                result = query.OrderByDescending(sortKeySelector);
            }

            return result;
        }

        private PaginationResults<BacklogItemModel> GetPaginationResultsFromQuery(
            IQueryable<BacklogItem> query, ServiceContext serviceContext, 
            int total)
        {
            int page = serviceContext.Page;
            int pageSize = serviceContext.ItemsPerPage;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<BacklogItem> entities = query.ToList();
            List<BacklogItemModel> models = HydrateModels(entities);
            return new PaginationResults<BacklogItemModel>(models, total, page, pageSize);
        }
    }
}
