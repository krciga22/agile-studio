using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaRepository : EntityRepository<DBContext, BacklogItemLinkTypeSchemaModel, BacklogItemLinkTypeSchema, int>
    {
        private readonly ServiceContext _ServiceContext;

        public BacklogItemLinkTypeSchemaRepository(
            DBContext dbContext, 
            Hydrator hydrator,
            ServiceContext serviceContext) : base(dbContext, hydrator)
        {
            _ServiceContext = serviceContext;
        }

        public override int GetIdentifier(BacklogItemLinkTypeSchemaModel model)
        {
            return model.ID;
        }

        public virtual PaginationResults<BacklogItemLinkTypeSchemaModel> GetByAccountID(int accountId)
        {
            var query =
                (from account in _DBContext.Account
                 join backlogItemLinkTypeSchema in _DBContext.BacklogItemLinkTypeSchema on account.ID equals backlogItemLinkTypeSchema.AccountID
                 where account.ID == accountId
                 select backlogItemLinkTypeSchema)
                .Distinct();

            query = ApplySearchToQuery(query);

            int total = query.Count();

            query = ApplySortToQuery(query);

            return GetPaginationResultsFromQuery(query, total);
        }

        protected override DbSet<BacklogItemLinkTypeSchema> GetDbSet()
        {
            return _DBContext.BacklogItemLinkTypeSchema;
        }

        private IQueryable<BacklogItemLinkTypeSchema> ApplySearchToQuery(
            IQueryable<BacklogItemLinkTypeSchema> query)
        {
            if (!string.IsNullOrWhiteSpace(_ServiceContext.SearchQuery))
            {
                string searchLower = _ServiceContext.SearchQuery.ToLower();
                query = query.Where(backlogItemLinkTypeSchema => 
                    backlogItemLinkTypeSchema.Title.ToLower().Contains(searchLower));
            }

            return query;
        }

        private IOrderedQueryable<BacklogItemLinkTypeSchema> ApplySortToQuery(
            IQueryable<BacklogItemLinkTypeSchema> query)
        {
            IOrderedQueryable<BacklogItemLinkTypeSchema>? result = null;

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

                    Expression<Func<BacklogItemLinkTypeSchema, string>> sortKeySelector = item =>
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
                Expression<Func<BacklogItemLinkTypeSchema, string>> sortKeySelector = item => item.ID.ToString();
                result = query.OrderByDescending(sortKeySelector);
            }

            return result;
        }

        private PaginationResults<BacklogItemLinkTypeSchemaModel> GetPaginationResultsFromQuery(
            IQueryable<BacklogItemLinkTypeSchema> query, int total)
        {
            int page = _ServiceContext.Page;
            int pageSize = _ServiceContext.ItemsPerPage;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<BacklogItemLinkTypeSchema> entities = query.ToList();
            List<BacklogItemLinkTypeSchemaModel> models = HydrateModels(entities);
            return new PaginationResults<BacklogItemLinkTypeSchemaModel>(models, total, page, pageSize);
        }
    }
}
