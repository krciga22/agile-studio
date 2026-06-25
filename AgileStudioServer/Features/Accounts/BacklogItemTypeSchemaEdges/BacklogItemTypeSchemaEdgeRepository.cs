using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges
{
    public class BacklogItemTypeSchemaEdgeRepository : EntityRepository<DBContext, BacklogItemTypeSchemaEdgeModel, BacklogItemTypeSchemaEdge, int>
    {
        public BacklogItemTypeSchemaEdgeRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(BacklogItemTypeSchemaEdgeModel model)
        {
            return model.ID;
        }

        public virtual List<BacklogItemTypeSchemaEdgeModel> GetByFromTypeId(int fromTypeId, int schemaId = 0)
        public virtual List<BacklogItemTypeSchemaEdgeModel> GetByFromTypeId(int? fromTypeId, int schemaId = 0)
        {
            var query = _DBContext.BacklogItemTypeSchemaEdge.Where(backlogItemTypeSchemaEdge =>
                backlogItemTypeSchemaEdge.FromTypeID == fromTypeId
            );

            if(schemaId > 0){
                query.Where(backlogItemTypeSchemaEdge => backlogItemTypeSchemaEdge.SchemaID == schemaId);
            }

            List<BacklogItemTypeSchemaEdge> entities = query.Include(b => b.FromType)
                .Include(b => b.ToType)
                .Include(b => b.Schema)
                .Include(b => b.CreatedBy)
                .ToList();

            return HydrateModels(entities);
        }

        public virtual List<BacklogItemTypeSchemaEdgeModel> GetByToTypeId(int toTypeId, int schemaId = 0)
        {
            var query = _DBContext.BacklogItemTypeSchemaEdge.Where(backlogItemTypeSchemaEdge =>
                backlogItemTypeSchemaEdge.ToType.ID == toTypeId
            );

            if (schemaId > 0){
                query.Where(backlogItemTypeSchemaEdge => backlogItemTypeSchemaEdge.SchemaID == schemaId);
            }

            List<BacklogItemTypeSchemaEdge> entities = query.Include(b => b.FromType)
                .Include(b => b.ToType)
                .Include(b => b.Schema)
                .Include(b => b.CreatedBy)
                .ToList();

            return HydrateModels(entities);
        }

        public virtual BacklogItemTypeSchemaEdgeModel? Get(int? fromTypeId, int toTypeId, int schemaId)
        {
            var query = _DBContext.BacklogItemTypeSchemaEdge.Where(backlogItemTypeSchemaEdge =>
                backlogItemTypeSchemaEdge.FromTypeID == fromTypeId &&
                backlogItemTypeSchemaEdge.ToTypeID == toTypeId && 
                backlogItemTypeSchemaEdge.SchemaID == schemaId
            );

            List<BacklogItemTypeSchemaEdge> entities = query.Include(b => b.FromType)
                .Include(b => b.ToType)
                .Include(b => b.Schema)
                .Include(b => b.CreatedBy)
                .ToList();

            return entities.Count == 1 ? HydrateModel(entities[0]) : null;
        }

        protected override DbSet<BacklogItemTypeSchemaEdge> GetDbSet()
        {
            return _DBContext.BacklogItemTypeSchemaEdge;
        }
    }
}
