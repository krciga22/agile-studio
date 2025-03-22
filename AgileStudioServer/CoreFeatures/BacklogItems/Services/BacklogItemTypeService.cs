using AgileStudioServer.Core.Hydrator;
using Entities = AgileStudioServer.CoreFeatures.BacklogItems.Repositories.Entities;
using AgileStudioServer.CoreFeatures.BacklogItems.Services.Models;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.BacklogItems.Services
{
    public class BacklogItemTypeService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public BacklogItemTypeService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<BacklogItemType> GetByBacklogItemTypeSchemaId(int backlogItemTypeSchemaId)
        {
            List<Entities.BacklogItemType> entities = _DBContext.BacklogItemType.Where(backlogItemType =>
                backlogItemType.BacklogItemTypeSchema.ID == backlogItemTypeSchemaId)
                .Include(b => b.CreatedBy)
                .Include(b => b.BacklogItemTypeSchema)
                .Include(b => b.Workflow)
                .ToList();

            return HydrateBacklogItemTypeModels(entities);
        }

        public virtual BacklogItemType? Get(int id)
        {
            Entities.BacklogItemType? entity = _DBContext.BacklogItemType.Find(id);
            if (entity is null)
            {
                return null;
            }

            _DBContext.Entry(entity).Reference("CreatedBy").Load();
            _DBContext.Entry(entity).Reference("BacklogItemTypeSchema").Load();
            _DBContext.Entry(entity).Reference("Workflow").Load();

            return HydrateBacklogItemTypeModel(entity);
        }

        public virtual BacklogItemType Create(BacklogItemType backlogItemType)
        {
            Entities.BacklogItemType entity = HydrateBacklogItemTypeEntity(backlogItemType);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemTypeModel(entity);
        }

        public virtual BacklogItemType Update(BacklogItemType backlogItemType)
        {
            Entities.BacklogItemType entity = HydrateBacklogItemTypeEntity(backlogItemType);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemTypeModel(entity);
        }

        public virtual void Delete(BacklogItemType backlogItemType)
        {
            Entities.BacklogItemType entity = HydrateBacklogItemTypeEntity(backlogItemType);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<BacklogItemType> HydrateBacklogItemTypeModels(List<Entities.BacklogItemType> entities, int depth = 3)
        {
            List<BacklogItemType> models = new();

            entities.ForEach(entity =>
            {
                BacklogItemType model = HydrateBacklogItemTypeModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private BacklogItemType HydrateBacklogItemTypeModel(Entities.BacklogItemType backlogItemType, int depth = 3)
        {
            return (BacklogItemType)_Hydrator.Hydrate(
                backlogItemType, typeof(BacklogItemType), depth
            );
        }

        private Entities.BacklogItemType HydrateBacklogItemTypeEntity(BacklogItemType backlogItemType, int depth = 3)
        {
            return (Entities.BacklogItemType)_Hydrator.Hydrate(
                backlogItemType, typeof(Entities.BacklogItemType), depth
            );
        }
    }
}
