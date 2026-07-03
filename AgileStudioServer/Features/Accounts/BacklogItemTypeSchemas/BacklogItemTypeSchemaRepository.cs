using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas
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

        public virtual PaginationResults<BacklogItemTypeSchemaModel> GetByAccountID(
            int accountId, ServiceContext serviceContext)
        {
            var query =
                (from account in _DBContext.Account
                 join backlogItemTypeSchema in _DBContext.BacklogItemTypeSchema 
                    on account.ID equals backlogItemTypeSchema.AccountID
                 where account.ID == accountId
                 select backlogItemTypeSchema)
                .Distinct();

            query = ApplySearchToQuery(query, serviceContext);

            int total = query.Count();

            query = ApplySortToQuery(query, serviceContext);

            return GetPaginationResultsFromQuery(query, serviceContext, total);
        }

        protected override DbSet<BacklogItemTypeSchema> GetDbSet()
        {
            return _DBContext.BacklogItemTypeSchema;
        }

        private static IQueryable<BacklogItemTypeSchema> ApplySearchToQuery(
            IQueryable<BacklogItemTypeSchema> query, ServiceContext serviceContext)
        {
            if (!string.IsNullOrWhiteSpace(serviceContext.SearchQuery))
            {
                string searchLower = serviceContext.SearchQuery.ToLower();
                query = query.Where(backlogItemTypeSchema => 
                    backlogItemTypeSchema.Title.ToLower().Contains(searchLower));
            }

            return query;
        }

        private static IOrderedQueryable<BacklogItemTypeSchema> ApplySortToQuery(
            IQueryable<BacklogItemTypeSchema> query, ServiceContext serviceContext)
        {
            IOrderedQueryable<BacklogItemTypeSchema>? result = null;

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

                    Expression<Func<BacklogItemTypeSchema, string>> sortKeySelector = item =>
                        ((DateTimeOffset)item.CreatedOn).ToUnixTimeSeconds().ToString();
                    switch (sortField)
                    {
                        case "title":
                            sortKeySelector = item => item.Title;
                            break;
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
                Expression<Func<BacklogItemTypeSchema, string>> sortKeySelector = item => item.ID.ToString();
                result = query.OrderByDescending(sortKeySelector);
            }

            return result;
        }

        private PaginationResults<BacklogItemTypeSchemaModel> GetPaginationResultsFromQuery(
            IQueryable<BacklogItemTypeSchema> query, ServiceContext serviceContext, int total)
        {
            int page = serviceContext.Page;
            int pageSize = serviceContext.ItemsPerPage;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<BacklogItemTypeSchema> entities = query.ToList();
            List<BacklogItemTypeSchemaModel> models = HydrateModels(entities);
            return new PaginationResults<BacklogItemTypeSchemaModel>(models, total, page, pageSize);
        }
    }
}
