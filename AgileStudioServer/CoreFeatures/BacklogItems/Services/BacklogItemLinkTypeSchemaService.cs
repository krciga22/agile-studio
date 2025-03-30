using AgileStudioServer.Core.Hydrator;
using Entities = AgileStudioServer.CoreFeatures.BacklogItems.Repositories.Entities;
using AgileStudioServer.CoreFeatures.BacklogItems.Services.Models;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.BacklogItems.Services
{
    public class BacklogItemLinkTypeSchemaService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public BacklogItemLinkTypeSchemaService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual BacklogItemLinkTypeSchemaModel? Get(int id)
        {
            Entities.BacklogItemLinkTypeSchema? entity = _DBContext.BacklogItemLinkTypeSchema.Find(id);
            if (entity is null)
            {
                return null;
            }

            _DBContext.Entry(entity).Reference("CreatedBy").Load();

            return HydrateBacklogItemLinkTypeSchemaModel(entity);
        }

        public virtual BacklogItemLinkTypeSchemaModel Create(BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchema)
        {
            Entities.BacklogItemLinkTypeSchema entity = HydrateBacklogItemLinkTypeSchemaEntity(backlogItemLinkTypeSchema);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemLinkTypeSchemaModel(entity);
        }

        public virtual BacklogItemLinkTypeSchemaModel Update(BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchema)
        {
            Entities.BacklogItemLinkTypeSchema entity = HydrateBacklogItemLinkTypeSchemaEntity(backlogItemLinkTypeSchema);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemLinkTypeSchemaModel(entity);
        }

        public virtual void Delete(BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchema)
        {
            Entities.BacklogItemLinkTypeSchema entity = HydrateBacklogItemLinkTypeSchemaEntity(backlogItemLinkTypeSchema);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private BacklogItemLinkTypeSchemaModel HydrateBacklogItemLinkTypeSchemaModel(Entities.BacklogItemLinkTypeSchema backlogItemLinkTypeSchema, int depth = 3)
        {
            return (BacklogItemLinkTypeSchemaModel)_Hydrator.Hydrate(
                backlogItemLinkTypeSchema, typeof(BacklogItemLinkTypeSchemaModel), depth
            );
        }

        private Entities.BacklogItemLinkTypeSchema HydrateBacklogItemLinkTypeSchemaEntity(BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchema, int depth = 3)
        {
            return (Entities.BacklogItemLinkTypeSchema)_Hydrator.Hydrate(
                backlogItemLinkTypeSchema, typeof(Entities.BacklogItemLinkTypeSchema), depth
            );
        }
    }
}
