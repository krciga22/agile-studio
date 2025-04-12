using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public BacklogItemLinkTypeSchemaEntryService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual BacklogItemLinkTypeSchemaEntryModel? Get(int id)
        {
            BacklogItemLinkTypeSchemaEntry? entity = _DBContext.BacklogItemLinkTypeSchemaEntry.Find(id);
            if (entity is null)
            {
                return null;
            }

            _DBContext.Entry(entity).Reference("CreatedBy").Load();

            return HydrateBacklogItemLinkTypeSchemaEntryModel(entity);
        }

        public virtual BacklogItemLinkTypeSchemaEntryModel Create(BacklogItemLinkTypeSchemaEntryModel backlogItemLinkTypeSchemaEntry)
        {
            BacklogItemLinkTypeSchemaEntry entity = HydrateBacklogItemLinkTypeSchemaEntryEntity(backlogItemLinkTypeSchemaEntry);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemLinkTypeSchemaEntryModel(entity);
        }

        public virtual BacklogItemLinkTypeSchemaEntryModel Update(BacklogItemLinkTypeSchemaEntryModel backlogItemLinkTypeSchemaEntry)
        {
            BacklogItemLinkTypeSchemaEntry entity = HydrateBacklogItemLinkTypeSchemaEntryEntity(backlogItemLinkTypeSchemaEntry);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateBacklogItemLinkTypeSchemaEntryModel(entity);
        }

        public virtual void Delete(BacklogItemLinkTypeSchemaEntryModel backlogItemLinkTypeSchemaEntry)
        {
            BacklogItemLinkTypeSchemaEntry entity = HydrateBacklogItemLinkTypeSchemaEntryEntity(backlogItemLinkTypeSchemaEntry);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private BacklogItemLinkTypeSchemaEntryModel HydrateBacklogItemLinkTypeSchemaEntryModel(BacklogItemLinkTypeSchemaEntry backlogItemLinkTypeSchemaEntry, int depth = 3)
        {
            return (BacklogItemLinkTypeSchemaEntryModel)_Hydrator.Hydrate(
                backlogItemLinkTypeSchemaEntry, typeof(BacklogItemLinkTypeSchemaEntryModel), depth
            );
        }

        private BacklogItemLinkTypeSchemaEntry HydrateBacklogItemLinkTypeSchemaEntryEntity(BacklogItemLinkTypeSchemaEntryModel backlogItemLinkTypeSchemaEntry, int depth = 3)
        {
            return (BacklogItemLinkTypeSchemaEntry)_Hydrator.Hydrate(
                backlogItemLinkTypeSchemaEntry, typeof(BacklogItemLinkTypeSchemaEntry), depth
            );
        }
    }
}
