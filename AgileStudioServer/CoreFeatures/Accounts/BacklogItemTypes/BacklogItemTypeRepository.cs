using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.Accounts.BacklogItemTypes
{
    public class BacklogItemTypeRepository : EntityRepository<DBContext, BacklogItemTypeModel, BacklogItemType, int>
    {
        public BacklogItemTypeRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(BacklogItemTypeModel model)
        {
            return model.ID;
        }

        public virtual List<BacklogItemTypeModel> GetByBacklogItemTypeSchemaId(int backlogItemTypeSchemaId)
        {
            List<BacklogItemType> entities = _DBContext.BacklogItemType.Where(backlogItemType =>
                backlogItemType.BacklogItemTypeSchema.ID == backlogItemTypeSchemaId)
                .Include(b => b.CreatedBy)
                .Include(b => b.BacklogItemTypeSchema)
                .Include(b => b.Workflow)
                .ToList();

            return HydrateModels(entities);
        }

        protected override DbSet<BacklogItemType> GetDbSet()
        {
            return _DBContext.BacklogItemType;
        }
    }
}
