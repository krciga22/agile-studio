using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.Accounts.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaRepository : EntityRepository<DBContext, BacklogItemTypeSchemaModel, BacklogItemTypeSchema, int>
    {
        public BacklogItemTypeSchemaRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(BacklogItemTypeSchemaModel model)
        {
            return model.ID;
        }

        public virtual List<BacklogItemTypeSchemaModel> GetAll()
        {
            List<BacklogItemTypeSchema> entities = _DBContext.BacklogItemTypeSchema.ToList();

            return HydrateModels(entities);
        }

        protected override DbSet<BacklogItemTypeSchema> GetDbSet()
        {
            return _DBContext.BacklogItemTypeSchema;
        }
    }
}
