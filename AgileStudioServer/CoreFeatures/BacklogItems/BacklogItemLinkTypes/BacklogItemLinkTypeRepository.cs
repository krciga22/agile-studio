using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeRepository : EntityRepository<DBContext,  BacklogItemLinkTypeModel, BacklogItemLinkType, int>
    {
        public BacklogItemLinkTypeRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(BacklogItemLinkTypeModel model)
        {
            return model.ID;
        }

        protected override DbSet<BacklogItemLinkType> GetDbSet()
        {
            return _DbContext.BacklogItemLinkType;
        }
    }
}
