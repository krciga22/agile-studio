using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes
{
    public class BacklogItemTypeSchemaNodeRepository : EntityRepository<DBContext, BacklogItemTypeSchemaNodeModel, BacklogItemTypeSchemaNode, int>
    {
        private readonly ServiceContext _ServiceContext;

        public BacklogItemTypeSchemaNodeRepository(
            DBContext dbContext, 
            Hydrator hydrator,
            ServiceContext serviceContext
            ) : base(dbContext, hydrator)
        {
            _ServiceContext = serviceContext;
        }

        public override int GetIdentifier(BacklogItemTypeSchemaNodeModel model)
        {
            return model.ID;
        }

        public virtual BacklogItemTypeSchemaNodeModel? Get(int schemaId, int backlogItemTypeId)
        {
            List<BacklogItemTypeSchemaNode> entities =
                _DBContext.BacklogItemTypeSchemaNode.Where(backlogItemTypeSchemaNode =>
                    backlogItemTypeSchemaNode.Schema.ID == schemaId && 
                    backlogItemTypeSchemaNode.BacklogItemTypeID == backlogItemTypeId
                )
                .Include(b => b.Schema)
                .Include(b => b.CreatedBy)
                .ToList();

            return entities.Count() > 0 ? HydrateModel(entities[0]) : null;
        }

        public PaginationResults<BacklogItemTypeSchemaNodeModel> GetBySchemaId(int schemaId)
        {
            var query = _DBContext.BacklogItemTypeSchemaNode.Where(node => node.SchemaID == schemaId);

            query = ApplySearchToQuery(query, _ServiceContext);

            int total = query.Count();

            query = ApplySortToQuery(query, _ServiceContext);

            return GetPaginationResultsFromQuery(query, _ServiceContext, total);
        }

        protected override DbSet<BacklogItemTypeSchemaNode> GetDbSet()
        {
            return _DBContext.BacklogItemTypeSchemaNode;
        }

        private static IQueryable<BacklogItemTypeSchemaNode> ApplySearchToQuery(
            IQueryable<BacklogItemTypeSchemaNode> query, ServiceContext serviceContext)
        {
            if (!string.IsNullOrWhiteSpace(serviceContext.SearchQuery))
            {
                string searchLower = serviceContext.SearchQuery.ToLower();

                query = query.Where(backlogItemTypeSchemaNode =>
                    backlogItemTypeSchemaNode.Schema.Title.ToLower().Contains(searchLower));

                query = query.Where(backlogItemTypeSchemaNode =>
                    backlogItemTypeSchemaNode.BacklogItemType.Title.ToLower().Contains(searchLower));
            }

            return query;
        }

        private static IOrderedQueryable<BacklogItemTypeSchemaNode> ApplySortToQuery(
            IQueryable<BacklogItemTypeSchemaNode> query, ServiceContext serviceContext)
        {
            IOrderedQueryable<BacklogItemTypeSchemaNode>? result = null;

            int sortedFieldsCount = 0;
            if (!string.IsNullOrWhiteSpace(serviceContext.Sort))
            {
                string[] sorts = serviceContext.Sort.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (string sort in sorts)
                {
                    sortedFieldsCount++;

                    string[] sortParts = sort.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    string sortField = sortParts[0];
                    bool descending = sortParts.Length > 1 && sortParts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

                    Expression<Func<BacklogItemTypeSchemaNode, string>> sortKeySelector = item =>
                        ((DateTimeOffset)item.CreatedOn).ToUnixTimeSeconds().ToString();
                    switch (sortField)
                    {
                        case "id":
                            sortKeySelector = item => item.ID.ToString();
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
                Expression<Func<BacklogItemTypeSchemaNode, string>> sortKeySelector = item => item.ID.ToString();
                result = query.OrderByDescending(sortKeySelector);
            }

            return result;
        }

        private PaginationResults<BacklogItemTypeSchemaNodeModel> GetPaginationResultsFromQuery(
            IQueryable<BacklogItemTypeSchemaNode> query, ServiceContext serviceContext, int total)
        {
            int page = serviceContext.Page;
            int pageSize = serviceContext.ItemsPerPage;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<BacklogItemTypeSchemaNode> entities = query.ToList();
            List<BacklogItemTypeSchemaNodeModel> models = HydrateModels(entities);
            return new PaginationResults<BacklogItemTypeSchemaNodeModel>(models, total, page, pageSize);
        }
    }
}
