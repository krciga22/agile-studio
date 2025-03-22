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

        public virtual List<BacklogItemTypeSchema> GetAll()
        {
            List<Entities.BacklogItemTypeSchema> entities = _DBContext.BacklogItemTypeSchema.ToList();

            return HydrateBacklogItemTypeSchemaModels(entities);
        }

        public virtual BacklogItemTypeSchema? Get(int id)
        {
            Entities.BacklogItemTypeSchema? entity = _DBContext.BacklogItemTypeSchema.Find(id);
            if (entity is null)
            {
                return null;
            }

            return HydrateBacklogItemTypeSchemaModel(entity);
        }

        public virtual BacklogItemTypeSchema Create(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            Entities.BacklogItemTypeSchema entity =
                HydrateBacklogItemTypeSchemaEntity(backlogItemTypeSchema);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemTypeSchemaModel(entity);
        }

        public virtual BacklogItemTypeSchema Update(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            Entities.BacklogItemTypeSchema entity =
                HydrateBacklogItemTypeSchemaEntity(backlogItemTypeSchema);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemTypeSchemaModel(entity);
        }

        public virtual void Delete(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            Entities.BacklogItemTypeSchema entity =
                HydrateBacklogItemTypeSchemaEntity(backlogItemTypeSchema);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<BacklogItemTypeSchema> HydrateBacklogItemTypeSchemaModels(
            List<Entities.BacklogItemTypeSchema> entities, int depth = 3)
        {
            List<BacklogItemTypeSchema> models = new();

            entities.ForEach(entity =>
            {
                BacklogItemTypeSchema model = HydrateBacklogItemTypeSchemaModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private BacklogItemTypeSchema HydrateBacklogItemTypeSchemaModel(
            Entities.BacklogItemTypeSchema backlogItemTypeSchema, int depth = 3)
        {
            return (BacklogItemTypeSchema)_Hydrator.Hydrate(
                backlogItemTypeSchema, typeof(BacklogItemTypeSchema), depth
            );
        }

        private Entities.BacklogItemTypeSchema HydrateBacklogItemTypeSchemaEntity(
            BacklogItemTypeSchema backlogItemTypeSchema, int depth = 3)
        {
            return (Entities.BacklogItemTypeSchema)_Hydrator.Hydrate(
                backlogItemTypeSchema, typeof(Entities.BacklogItemTypeSchema), depth
            );
        }
    }
}
