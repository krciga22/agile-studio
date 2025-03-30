using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas
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
            BacklogItemLinkTypeSchema? entity = _DBContext.BacklogItemLinkTypeSchema.Find(id);
            if (entity is null)
            {
                return null;
            }

            _DBContext.Entry(entity).Reference("CreatedBy").Load();

            return HydrateBacklogItemLinkTypeSchemaModel(entity);
        }

        public virtual BacklogItemLinkTypeSchemaModel Create(BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchema)
        {
            BacklogItemLinkTypeSchema entity = HydrateBacklogItemLinkTypeSchemaEntity(backlogItemLinkTypeSchema);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemLinkTypeSchemaModel(entity);
        }

        public virtual BacklogItemLinkTypeSchemaModel Update(BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchema)
        {
            BacklogItemLinkTypeSchema entity = HydrateBacklogItemLinkTypeSchemaEntity(backlogItemLinkTypeSchema);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemLinkTypeSchemaModel(entity);
        }

        public virtual void Delete(BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchema)
        {
            BacklogItemLinkTypeSchema entity = HydrateBacklogItemLinkTypeSchemaEntity(backlogItemLinkTypeSchema);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private BacklogItemLinkTypeSchemaModel HydrateBacklogItemLinkTypeSchemaModel(BacklogItemLinkTypeSchema backlogItemLinkTypeSchema, int depth = 3)
        {
            return (BacklogItemLinkTypeSchemaModel)_Hydrator.Hydrate(
                backlogItemLinkTypeSchema, typeof(BacklogItemLinkTypeSchemaModel), depth
            );
        }

        private BacklogItemLinkTypeSchema HydrateBacklogItemLinkTypeSchemaEntity(BacklogItemLinkTypeSchemaModel backlogItemLinkTypeSchema, int depth = 3)
        {
            return (BacklogItemLinkTypeSchema)_Hydrator.Hydrate(
                backlogItemLinkTypeSchema, typeof(BacklogItemLinkTypeSchema), depth
            );
        }
    }
}
