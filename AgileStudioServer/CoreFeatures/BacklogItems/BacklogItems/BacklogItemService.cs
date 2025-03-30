using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.BacklogItems.BacklogItems
{
    public class BacklogItemService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public BacklogItemService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<BacklogItemModel> GetByProjectId(int projectId)
        {
            List<BacklogItem> entities = _DBContext.BacklogItem.Where(backlogItem =>
                backlogItem.Project.ID == projectId).ToList();

            return HydrateBacklogItemModels(entities);
        }

        public virtual List<BacklogItemModel> GetByProjectIdAndBacklogItemTypeId(int projectId, int backlogItemTypeId)
        {
            List<BacklogItem> entities = _DBContext.BacklogItem.Where(backlogItem =>
                backlogItem.Project.ID == projectId && backlogItem.BacklogItemType.ID == backlogItemTypeId).ToList();

            return HydrateBacklogItemModels(entities);
        }

        public virtual PaginationResults<BacklogItemModel> GetChildBacklogItems(int parentBacklogItemId, PaginationDetails? paginationDetails = null)
        {
            if (paginationDetails is null)
            {
                paginationDetails = new PaginationDetails();
            }

            IQueryable<BacklogItem> query = _DBContext.BacklogItem
                .Where(backlogItem => backlogItem.ParentBacklogItemId == parentBacklogItemId)
                .OrderByDescending(backlogItem => backlogItem.CreatedOn)
                .ThenByDescending(backlogItem => backlogItem.ID);

            int total = query.Count();

            query = query.Skip(paginationDetails.ItemsPerPage * (paginationDetails.Page - 1)).Take(paginationDetails.ItemsPerPage);

            List<BacklogItem> entities = query.ToList();

            PaginationResults<BacklogItemModel> results = new(
                HydrateBacklogItemModels(entities),
                total,
                paginationDetails.Page,
                paginationDetails.ItemsPerPage
            );

            return results;
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

            return HydrateBacklogItemModel(parentEntity);
        }

        public virtual BacklogItemModel? Get(int id)
        {
            BacklogItem? entity = _DBContext.BacklogItem.Find(id);
            if (entity is null)
            {
                return null;
            }

            return HydrateBacklogItemModel(entity);
        }

        public virtual BacklogItemModel Create(BacklogItemModel backlogItem)
        {
            BacklogItem entity = HydrateBacklogItemEntity(backlogItem);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemModel(entity);
        }

        public virtual BacklogItemModel Update(BacklogItemModel backlogItem)
        {
            BacklogItem entity = HydrateBacklogItemEntity(backlogItem);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemModel(entity);
        }

        public virtual void Delete(BacklogItemModel backlogItem)
        {
            BacklogItem entity = HydrateBacklogItemEntity(backlogItem);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<BacklogItemModel> HydrateBacklogItemModels(List<BacklogItem> entities, int depth = 3)
        {
            List<BacklogItemModel> models = new();

            entities.ForEach(entity =>
            {
                BacklogItemModel model = HydrateBacklogItemModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private BacklogItemModel HydrateBacklogItemModel(BacklogItem backlogItem, int depth = 3)
        {
            return (BacklogItemModel)_Hydrator.Hydrate(
                backlogItem, typeof(BacklogItemModel), depth
            );
        }

        private BacklogItem HydrateBacklogItemEntity(BacklogItemModel backlogItem, int depth = 3)
        {
            return (BacklogItem)_Hydrator.Hydrate(
                backlogItem, typeof(BacklogItem), depth
            );
        }
    }
}
