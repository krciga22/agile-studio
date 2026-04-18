using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.Accounts.ChildBacklogItemTypes
{
    public class ChildBacklogItemTypeRepository : EntityRepository<DBContext, ChildBacklogItemTypeModel, ChildBacklogItemType, int>
    {
        public ChildBacklogItemTypeRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(ChildBacklogItemTypeModel model)
        {
            return model.ID;
        }

        public virtual List<ChildBacklogItemTypeModel> GetByParentTypeId(int parentTypeId)
        {
            List<ChildBacklogItemType> entities =
                _DBContext.ChildBacklogItemType.Where(childBacklogItemType =>
                    childBacklogItemType.ParentType.ID == parentTypeId
                )
                .Include(b => b.ChildType)
                .Include(b => b.ParentType)
                .Include(b => b.Schema)
                .Include(b => b.CreatedBy)
                .ToList();

            return HydrateModels(entities, 2);
        }

        public virtual List<ChildBacklogItemTypeModel> GetByChildTypeId(int childTypeId)
        {
            List<ChildBacklogItemType> entities =
                _DBContext.ChildBacklogItemType.Where(childBacklogItemType =>
                    childBacklogItemType.ChildType.ID == childTypeId
                )
                .Include(b => b.ChildType)
                .Include(b => b.ParentType)
                .Include(b => b.Schema)
                .Include(b => b.CreatedBy)
                .ToList();

            return HydrateModels(entities);
        }

        public virtual ChildBacklogItemTypeModel? Get(int parentTypeId, int childTypeId)
        {
            List<ChildBacklogItemType> entities =
                _DBContext.ChildBacklogItemType.Where(childBacklogItemType =>
                    childBacklogItemType.ParentType.ID == parentTypeId &&
                    childBacklogItemType.ChildType.ID == childTypeId
                )
                .Include(b => b.ChildType)
                .Include(b => b.ParentType)
                .Include(b => b.Schema)
                .Include(b => b.CreatedBy)
                .ToList();

            return entities.Count() == 1 ? HydrateModel(entities[0]) : null;
        }

        protected override DbSet<ChildBacklogItemType> GetDbSet()
        {
            return _DBContext.ChildBacklogItemType;
        }
    }
}
