using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaRepository : EntityRepository<DBContext, BacklogItemLinkTypeSchemaModel, BacklogItemLinkTypeSchema, int>
    {
        public BacklogItemLinkTypeSchemaRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(BacklogItemLinkTypeSchemaModel model)
        {
            return model.ID;
        }

        protected override DbSet<BacklogItemLinkTypeSchema> GetDbSet()
        {
            return _DbContext.BacklogItemLinkTypeSchema;
        }
    }
}
