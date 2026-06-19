using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes
{
    public class BacklogItemTypeSchemaNodeRepository : EntityRepository<DBContext, BacklogItemTypeSchemaNodeModel, BacklogItemTypeSchemaNode, int>
    {
        public BacklogItemTypeSchemaNodeRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(BacklogItemTypeSchemaNodeModel model)
        {
            return model.ID;
        }

        public virtual BacklogItemTypeSchemaNodeModel? GetBySchemaId(int schemaId)
        {
            List<BacklogItemTypeSchemaNode> entities =
                _DBContext.BacklogItemTypeSchemaNode.Where(backlogItemTypeSchemaNode =>
                    backlogItemTypeSchemaNode.Schema.ID == schemaId
                )
                .Include(b => b.Schema)
                .Include(b => b.CreatedBy)
                .ToList();

            return entities.Count() > 0 ? HydrateModel(entities[0]) : null;
        }

        protected override DbSet<BacklogItemTypeSchemaNode> GetDbSet()
        {
            return _DBContext.BacklogItemTypeSchemaNode;
        }
    }
}
