using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.BacklogItems.BacklogItems
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

        public virtual List<BacklogItemModel> GetByProjectId(int projectId)
        {
            List<BacklogItem> entities = _DbContext.BacklogItem.Where(backlogItem =>
                backlogItem.Project.ID == projectId).ToList();

            return HydrateModels(entities);
        }

        public virtual List<BacklogItemModel> GetByProjectIdAndBacklogItemTypeId(int projectId, int backlogItemTypeId)
        {
            List<BacklogItem> entities = _DbContext.BacklogItem.Where(backlogItem =>
                backlogItem.Project.ID == projectId && backlogItem.BacklogItemType.ID == backlogItemTypeId).ToList();

            return HydrateModels(entities);
        }

        public virtual PaginationResults<BacklogItemModel> GetChildBacklogItems(int parentBacklogItemId, PaginationDetails? paginationDetails = null)
        {
            if (paginationDetails is null)
            {
                paginationDetails = new PaginationDetails();
            }

            IQueryable<BacklogItem> query = _DbContext.BacklogItem
                .Where(backlogItem => backlogItem.ParentBacklogItemId == parentBacklogItemId)
                .OrderByDescending(backlogItem => backlogItem.CreatedOn)
                .ThenByDescending(backlogItem => backlogItem.ID);

            int total = query.Count();

            query = query.Skip(paginationDetails.ItemsPerPage * (paginationDetails.Page - 1)).Take(paginationDetails.ItemsPerPage);

            List<BacklogItem> entities = query.ToList();

            PaginationResults<BacklogItemModel> results = new(
                HydrateModels(entities),
                total,
                paginationDetails.Page,
                paginationDetails.ItemsPerPage
            );

            return results;
        }

        public virtual BacklogItemModel? GetParentBacklogItem(int id)
        {
            BacklogItem? entity = _DbContext.BacklogItem.Find(id);
            if (entity is null || entity.ParentBacklogItemId is null)
            {
                return null;
            }

            BacklogItem? parentEntity =
                _DbContext.BacklogItem.Find(entity.ParentBacklogItemId);
            if (parentEntity is null)
            {
                return null;
            }

            return HydrateModel(parentEntity);
        }

        protected override DbSet<BacklogItem> GetDbSet()
        {
            return _DbContext.BacklogItem;
        }
    }
}
