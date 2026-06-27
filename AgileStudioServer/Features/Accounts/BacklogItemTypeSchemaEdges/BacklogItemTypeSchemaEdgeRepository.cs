using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges
{
    public class BacklogItemTypeSchemaEdgeRepository : EntityRepository<DBContext, BacklogItemTypeSchemaEdgeModel, BacklogItemTypeSchemaEdge, int>
    {
        private readonly ServiceContext _ServiceContext;

        public BacklogItemTypeSchemaEdgeRepository(
            ServiceContext serviceContext, 
            DBContext dbContext, 
            Hydrator hydrator) : 
            base(dbContext, hydrator)
        {
            _ServiceContext = serviceContext;
        }

        public override int GetIdentifier(BacklogItemTypeSchemaEdgeModel model)
        {
            return model.ID;
        }

        public virtual List<BacklogItemTypeSchemaEdgeModel> GetByFromTypeId(int? fromTypeId, int schemaId = 0)
        {
            var query = _DBContext.BacklogItemTypeSchemaEdge.Where(backlogItemTypeSchemaEdge =>
                backlogItemTypeSchemaEdge.FromTypeID == fromTypeId
            );

            if(schemaId > 0){
                query.Where(backlogItemTypeSchemaEdge => backlogItemTypeSchemaEdge.SchemaID == schemaId);
            }

            query = query.Include(b => b.FromType)
                .Include(b => b.ToType)
                .Include(b => b.Schema)
                .Include(b => b.CreatedBy);

            query = ApplySortToQuery(query);

            return HydrateModels([.. query]);
        }

        public virtual List<BacklogItemTypeSchemaEdgeModel> GetByToTypeId(int toTypeId, int schemaId = 0)
        {
            var query = _DBContext.BacklogItemTypeSchemaEdge.Where(backlogItemTypeSchemaEdge =>
                backlogItemTypeSchemaEdge.ToType.ID == toTypeId
            );

            if (schemaId > 0){
                query.Where(backlogItemTypeSchemaEdge => backlogItemTypeSchemaEdge.SchemaID == schemaId);
            }

            query = query.Include(b => b.FromType)
                .Include(b => b.ToType)
                .Include(b => b.Schema)
                .Include(b => b.CreatedBy);

            query = ApplySortToQuery(query);

            return HydrateModels([.. query]);
        }

        public virtual List<BacklogItemTypeSchemaEdgeModel> GetBySchemaId(int schemaId)
        {
            List<BacklogItemTypeSchemaEdge> entities = _DBContext.BacklogItemTypeSchemaEdge
                .Where(backlogItemTypeSchemaEdge =>
                    backlogItemTypeSchemaEdge.SchemaID == schemaId
                )
                .Include(b => b.FromType)
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

        private IOrderedQueryable<BacklogItemTypeSchemaEdge> ApplySortToQuery(
            IQueryable<BacklogItemTypeSchemaEdge> query)
        {
            IOrderedQueryable<BacklogItemTypeSchemaEdge>? result = null;

            int sortedFieldsCount = 0;
            if (!string.IsNullOrWhiteSpace(_ServiceContext.Sort))
            {
                string[] sorts = _ServiceContext.Sort.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (string sort in sorts)
                {
                    sortedFieldsCount++;

                    string[] sortParts = sort.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    string sortField = sortParts[0];
                    bool descending = sortParts.Length > 1 && sortParts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

                    Expression<Func<BacklogItemTypeSchemaEdge, string>> sortKeySelector = item =>
                        ((DateTimeOffset)item.CreatedOn).ToUnixTimeSeconds().ToString();

                    switch (sortField)
                    {
                        case "id":
                            sortKeySelector = item => item.ID.ToString();
                            break;
                        case "fromTypeID":
                            sortKeySelector = item => item.FromTypeID.ToString();
                            break;
                        case "toTypeID":
                            sortKeySelector = item => item.ToTypeID.ToString();
                            break;
                        default:
                            sortedFieldsCount--;
                            break;
                    }

                    if (result == null)
                    {
                        result = descending ?
                            query.OrderByDescending(sortKeySelector) :
                            query.OrderBy(sortKeySelector);
                    }
                    else
                    {
                        result = descending ?
                            result.ThenByDescending(sortKeySelector) :
                            result.ThenBy(sortKeySelector);
                    }
                }
            }

            if (result == null)
            {
                Expression<Func<BacklogItemTypeSchemaEdge, string>> sortKeySelector = item => item.ID.ToString();
                result = query.OrderByDescending(sortKeySelector);
            }

            return result;
        }
    }
}
