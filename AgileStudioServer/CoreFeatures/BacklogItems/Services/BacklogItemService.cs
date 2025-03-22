using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using Entities = AgileStudioServer.CoreFeatures.BacklogItems.Repositories.Entities;
using AgileStudioServer.CoreFeatures.BacklogItems.Services.Models;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.BacklogItems.Services
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

        public virtual List<BacklogItem> GetByProjectId(int projectId)
        {
            List<Entities.BacklogItem> entities = _DBContext.BacklogItem.Where(backlogItem =>
                backlogItem.Project.ID == projectId).ToList();

            return HydrateBacklogItemModels(entities);
        }

        public virtual List<BacklogItem> GetByProjectIdAndBacklogItemTypeId(int projectId, int backlogItemTypeId)
        {
            List<Entities.BacklogItem> entities = _DBContext.BacklogItem.Where(backlogItem =>
                backlogItem.Project.ID == projectId && backlogItem.BacklogItemType.ID == backlogItemTypeId).ToList();

            return HydrateBacklogItemModels(entities);
        }

        public virtual PaginationResults<BacklogItem> GetChildBacklogItems(int parentBacklogItemId, PaginationDetails? paginationDetails = null)
        {
            if (paginationDetails is null)
            {
                paginationDetails = new PaginationDetails();
            }

            IQueryable<Entities.BacklogItem> query = _DBContext.BacklogItem
                .Where(backlogItem => backlogItem.ParentBacklogItemId == parentBacklogItemId)
                .OrderByDescending(backlogItem => backlogItem.CreatedOn)
                .ThenByDescending(backlogItem => backlogItem.ID);

            int total = query.Count();

            query = query.Skip(paginationDetails.ItemsPerPage * (paginationDetails.Page - 1)).Take(paginationDetails.ItemsPerPage);

            List<Entities.BacklogItem> entities = query.ToList();

            PaginationResults<BacklogItem> results = new(
                HydrateBacklogItemModels(entities),
                total,
                paginationDetails.Page,
                paginationDetails.ItemsPerPage
            );

            return results;
        }

        public virtual BacklogItem? GetParentBacklogItem(int id)
        {
            Entities.BacklogItem? entity = _DBContext.BacklogItem.Find(id);
            if (entity is null || entity.ParentBacklogItemId is null)
            {
                return null;
            }

            Entities.BacklogItem? parentEntity =
                _DBContext.BacklogItem.Find(entity.ParentBacklogItemId);
            if (parentEntity is null)
            {
                return null;
            }

            return HydrateBacklogItemModel(parentEntity);
        }

        public virtual BacklogItem? Get(int id)
        {
            Entities.BacklogItem? entity = _DBContext.BacklogItem.Find(id);
            if (entity is null)
            {
                return null;
            }

            return HydrateBacklogItemModel(entity);
        }

        public virtual BacklogItem Create(BacklogItem backlogItem)
        {
            Entities.BacklogItem entity = HydrateBacklogItemEntity(backlogItem);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemModel(entity);
        }

        public virtual BacklogItem Update(BacklogItem backlogItem)
        {
            Entities.BacklogItem entity = HydrateBacklogItemEntity(backlogItem);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemModel(entity);
        }

        public virtual void Delete(BacklogItem backlogItem)
        {
            Entities.BacklogItem entity = HydrateBacklogItemEntity(backlogItem);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<BacklogItem> HydrateBacklogItemModels(List<Entities.BacklogItem> entities, int depth = 3)
        {
            List<BacklogItem> models = new();

            entities.ForEach(entity =>
            {
                BacklogItem model = HydrateBacklogItemModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private BacklogItem HydrateBacklogItemModel(Entities.BacklogItem backlogItem, int depth = 3)
        {
            return (BacklogItem)_Hydrator.Hydrate(
                backlogItem, typeof(BacklogItem), depth
            );
        }

        private Entities.BacklogItem HydrateBacklogItemEntity(BacklogItem backlogItem, int depth = 3)
        {
            return (Entities.BacklogItem)_Hydrator.Hydrate(
                backlogItem, typeof(Entities.BacklogItem), depth
            );
        }
    }
}
