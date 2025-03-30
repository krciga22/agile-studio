using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.BacklogItems.ChildBacklogItemTypes
{
    public class ChildBacklogItemTypeService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public ChildBacklogItemTypeService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<ChildBacklogItemTypeModel> GetByParentTypeId(int parentTypeId)
        {
            List<ChildBacklogItemType> entities =
                _DBContext.ChildBacklogItemType.Where(childBacklogItemType =>
                    childBacklogItemType.ParentType.ID == parentTypeId
                )
                .Include(b => b.ChildType)
                .Include(b => b.ParentType)
                .Include(b => b.Schema)
                .Include(b => b.CreatedBy)
                .ToList();

            return HydrateChildBacklogItemTypeModels(entities, 2);
        }

        public virtual List<ChildBacklogItemTypeModel> GetByChildTypeId(int childTypeId)
        {
            List<ChildBacklogItemType> entities =
                _DBContext.ChildBacklogItemType.Where(childBacklogItemType =>
                    childBacklogItemType.ChildType.ID == childTypeId
                )
                .Include(b => b.ChildType)
                .Include(b => b.ParentType)
                .Include(b => b.Schema)
                .Include(b => b.CreatedBy)
                .ToList();

            return HydrateChildBacklogItemTypeModels(entities);
        }

        public virtual ChildBacklogItemTypeModel? Get(int id)
        {
            ChildBacklogItemType? entity = _DBContext.ChildBacklogItemType.Find(id);
            if (entity is null)
            {
                return null;
            }

            return HydrateChildBacklogItemTypeModel(entity);
        }

        public virtual ChildBacklogItemTypeModel? Get(int parentTypeId, int childTypeId)
        {
            List<ChildBacklogItemType> entities =
                _DBContext.ChildBacklogItemType.Where(childBacklogItemType =>
                    childBacklogItemType.ParentType.ID == parentTypeId &&
                    childBacklogItemType.ChildType.ID == childTypeId
                )
                .Include(b => b.ChildType)
                .Include(b => b.ParentType)
                .Include(b => b.Schema)
                .Include(b => b.CreatedBy)
                .ToList();

            return entities.Count() == 1 ? HydrateChildBacklogItemTypeModel(entities[0]) : null;
        }

        public virtual ChildBacklogItemTypeModel Create(ChildBacklogItemTypeModel childBacklogItemType)
        {
            ChildBacklogItemType entity = HydrateChildBacklogItemTypeEntity(childBacklogItemType);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateChildBacklogItemTypeModel(entity);
        }

        public virtual ChildBacklogItemTypeModel Update(ChildBacklogItemTypeModel childBacklogItemType)
        {
            ChildBacklogItemType entity = HydrateChildBacklogItemTypeEntity(childBacklogItemType);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateChildBacklogItemTypeModel(entity);
        }

        public virtual void Delete(ChildBacklogItemTypeModel childBacklogItemType)
        {
            ChildBacklogItemType entity = HydrateChildBacklogItemTypeEntity(childBacklogItemType);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<ChildBacklogItemTypeModel> HydrateChildBacklogItemTypeModels(List<ChildBacklogItemType> entities, int depth = 3)
        {
            List<ChildBacklogItemTypeModel> models = new();

            entities.ForEach(entity =>
            {
                ChildBacklogItemTypeModel model = HydrateChildBacklogItemTypeModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private ChildBacklogItemTypeModel HydrateChildBacklogItemTypeModel(ChildBacklogItemType childBacklogItemType, int depth = 3)
        {
            return (ChildBacklogItemTypeModel)_Hydrator.Hydrate(
                childBacklogItemType, typeof(ChildBacklogItemTypeModel), depth
            );
        }

        private ChildBacklogItemType HydrateChildBacklogItemTypeEntity(ChildBacklogItemTypeModel childBacklogItemType, int depth = 3)
        {
            return (ChildBacklogItemType)_Hydrator.Hydrate(
                childBacklogItemType, typeof(ChildBacklogItemType), depth
            );
        }
    }
}
