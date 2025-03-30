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

        public virtual List<BacklogItemTypeModel> GetByBacklogItemTypeSchemaId(int backlogItemTypeSchemaId)
        {
            List<Entities.BacklogItemType> entities = _DBContext.BacklogItemType.Where(backlogItemType =>
                backlogItemType.BacklogItemTypeSchema.ID == backlogItemTypeSchemaId)
                .Include(b => b.CreatedBy)
                .Include(b => b.BacklogItemTypeSchema)
                .Include(b => b.Workflow)
                .ToList();

            return HydrateBacklogItemTypeModels(entities);
        }

        public virtual BacklogItemTypeModel? Get(int id)
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

        public virtual BacklogItemTypeModel Create(BacklogItemTypeModel backlogItemType)
        {
            Entities.BacklogItemType entity = HydrateBacklogItemTypeEntity(backlogItemType);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemTypeModel(entity);
        }

        public virtual BacklogItemTypeModel Update(BacklogItemTypeModel backlogItemType)
        {
            Entities.BacklogItemType entity = HydrateBacklogItemTypeEntity(backlogItemType);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemTypeModel(entity);
        }

        public virtual void Delete(BacklogItemTypeModel backlogItemType)
        {
            Entities.BacklogItemType entity = HydrateBacklogItemTypeEntity(backlogItemType);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<BacklogItemTypeModel> HydrateBacklogItemTypeModels(List<Entities.BacklogItemType> entities, int depth = 3)
        {
            List<BacklogItemTypeModel> models = new();

            entities.ForEach(entity =>
            {
                BacklogItemTypeModel model = HydrateBacklogItemTypeModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private BacklogItemTypeModel HydrateBacklogItemTypeModel(Entities.BacklogItemType backlogItemType, int depth = 3)
        {
            return (BacklogItemTypeModel)_Hydrator.Hydrate(
                backlogItemType, typeof(BacklogItemTypeModel), depth
            );
        }

        private Entities.BacklogItemType HydrateBacklogItemTypeEntity(BacklogItemTypeModel backlogItemType, int depth = 3)
        {
            return (Entities.BacklogItemType)_Hydrator.Hydrate(
                backlogItemType, typeof(Entities.BacklogItemType), depth
            );
        }
    }
}
