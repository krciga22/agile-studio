using AgileStudioServer.Core.Hydrator;
using Entities = AgileStudioServer.CoreFeatures.BacklogItems.Repositories.Entities;
using AgileStudioServer.CoreFeatures.BacklogItems.Services.Models;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.BacklogItems.Services
{
    public class BacklogItemTypeSchemaService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public BacklogItemTypeSchemaService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<BacklogItemTypeSchemaModel> GetAll()
        {
            List<Entities.BacklogItemTypeSchema> entities = _DBContext.BacklogItemTypeSchema.ToList();

            return HydrateBacklogItemTypeSchemaModels(entities);
        }

        public virtual BacklogItemTypeSchemaModel? Get(int id)
        {
            Entities.BacklogItemTypeSchema? entity = _DBContext.BacklogItemTypeSchema.Find(id);
            if (entity is null)
            {
                return null;
            }

            return HydrateBacklogItemTypeSchemaModel(entity);
        }

        public virtual BacklogItemTypeSchemaModel Create(BacklogItemTypeSchemaModel backlogItemTypeSchema)
        {
            Entities.BacklogItemTypeSchema entity =
                HydrateBacklogItemTypeSchemaEntity(backlogItemTypeSchema);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemTypeSchemaModel(entity);
        }

        public virtual BacklogItemTypeSchemaModel Update(BacklogItemTypeSchemaModel backlogItemTypeSchema)
        {
            Entities.BacklogItemTypeSchema entity =
                HydrateBacklogItemTypeSchemaEntity(backlogItemTypeSchema);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemTypeSchemaModel(entity);
        }

        public virtual void Delete(BacklogItemTypeSchemaModel backlogItemTypeSchema)
        {
            Entities.BacklogItemTypeSchema entity =
                HydrateBacklogItemTypeSchemaEntity(backlogItemTypeSchema);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<BacklogItemTypeSchemaModel> HydrateBacklogItemTypeSchemaModels(
            List<Entities.BacklogItemTypeSchema> entities, int depth = 3)
        {
            List<BacklogItemTypeSchemaModel> models = new();

            entities.ForEach(entity =>
            {
                BacklogItemTypeSchemaModel model = HydrateBacklogItemTypeSchemaModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private BacklogItemTypeSchemaModel HydrateBacklogItemTypeSchemaModel(
            Entities.BacklogItemTypeSchema backlogItemTypeSchema, int depth = 3)
        {
            return (BacklogItemTypeSchemaModel)_Hydrator.Hydrate(
                backlogItemTypeSchema, typeof(BacklogItemTypeSchemaModel), depth
            );
        }

        private Entities.BacklogItemTypeSchema HydrateBacklogItemTypeSchemaEntity(
            BacklogItemTypeSchemaModel backlogItemTypeSchema, int depth = 3)
        {
            return (Entities.BacklogItemTypeSchema)_Hydrator.Hydrate(
                backlogItemTypeSchema, typeof(Entities.BacklogItemTypeSchema), depth
            );
        }
    }
}
