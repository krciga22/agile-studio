using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEntries
{
    public class BacklogItemTypeSchemaEntryRepository : EntityRepository<DBContext, BacklogItemTypeSchemaEntryModel, BacklogItemTypeSchemaEntry, int>
    {
        public BacklogItemTypeSchemaEntryRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(BacklogItemTypeSchemaEntryModel model)
        {
            return model.ID;
        }

        public virtual List<BacklogItemTypeSchemaEntryModel> GetByParentTypeId(int parentTypeId)
        {
            List<BacklogItemTypeSchemaEntry> entities =
                _DBContext.BacklogItemTypeSchemaEntry.Where(backlogItemTypeSchemaEntry =>
                    backlogItemTypeSchemaEntry.ParentType.ID == parentTypeId
                )
                .Include(b => b.ChildType)
                .Include(b => b.ParentType)
                .Include(b => b.Schema)
                .Include(b => b.CreatedBy)
                .ToList();

            return HydrateModels(entities, 2);
        }

        public virtual List<BacklogItemTypeSchemaEntryModel> GetByChildTypeId(int childTypeId)
        {
            List<BacklogItemTypeSchemaEntry> entities =
                _DBContext.BacklogItemTypeSchemaEntry.Where(backlogItemTypeSchemaEntry =>
                    backlogItemTypeSchemaEntry.ChildType.ID == childTypeId
                )
                .Include(b => b.ChildType)
                .Include(b => b.ParentType)
                .Include(b => b.Schema)
                .Include(b => b.CreatedBy)
                .ToList();

            return HydrateModels(entities);
        }

        public virtual BacklogItemTypeSchemaEntryModel? GetByChildTypeIdAndSchemaId(int childTypeId, int schemaId)
        {
            List<BacklogItemTypeSchemaEntry> entities =
                _DBContext.BacklogItemTypeSchemaEntry.Where(backlogItemTypeSchemaEntry =>
                    backlogItemTypeSchemaEntry.ChildType.ID == childTypeId && 
                    backlogItemTypeSchemaEntry.Schema.ID == schemaId
                )
                .Include(b => b.ChildType)
                .Include(b => b.ParentType)
                .Include(b => b.Schema)
                .Include(b => b.CreatedBy)
                .ToList();

            return entities.Count() > 0 ? HydrateModel(entities[0]) : null;
        }

        public virtual BacklogItemTypeSchemaEntryModel? Get(int parentTypeId, int childTypeId)
        {
            List<BacklogItemTypeSchemaEntry> entities =
                _DBContext.BacklogItemTypeSchemaEntry.Where(backlogItemTypeSchemaEntry =>
                    backlogItemTypeSchemaEntry.ParentType.ID == parentTypeId &&
                    backlogItemTypeSchemaEntry.ChildType.ID == childTypeId
                )
                .Include(b => b.ChildType)
                .Include(b => b.ParentType)
                .Include(b => b.Schema)
                .Include(b => b.CreatedBy)
                .ToList();

            return entities.Count() == 1 ? HydrateModel(entities[0]) : null;
        }

        protected override DbSet<BacklogItemTypeSchemaEntry> GetDbSet()
        {
            return _DBContext.BacklogItemTypeSchemaEntry;
        }
    }
}
