using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryRepository : EntityRepository<DBContext,  BacklogItemLinkTypeSchemaEntryModel, BacklogItemLinkTypeSchemaEntry, int>
    {
        public BacklogItemLinkTypeSchemaEntryRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(BacklogItemLinkTypeSchemaEntryModel model)
        {
            return model.ID;
        }

        protected override DbSet<BacklogItemLinkTypeSchemaEntry> GetDbSet()
        {
            return _DBContext.BacklogItemLinkTypeSchemaEntry;
        }
    }
}
