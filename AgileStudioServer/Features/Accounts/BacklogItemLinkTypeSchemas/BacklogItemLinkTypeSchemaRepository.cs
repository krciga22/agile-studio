using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas
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

        public virtual List<BacklogItemLinkTypeSchemaModel> GetAll()
        {
            List<BacklogItemLinkTypeSchema> entities = _DBContext.BacklogItemLinkTypeSchema.ToList();

            return HydrateModels(entities);
        }

        protected override DbSet<BacklogItemLinkTypeSchema> GetDbSet()
        {
            return _DBContext.BacklogItemLinkTypeSchema;
        }
    }
}
