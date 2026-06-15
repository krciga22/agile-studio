using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypes
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

        public virtual List<BacklogItemTypeModel> GetByBacklogItemTypeSchemaId(
            int backlogItemTypeSchemaId)
        {
            List<BacklogItemType> entities = _DBContext.BacklogItemType.Where(backlogItemType =>
                backlogItemType.BacklogItemTypeSchema.ID == backlogItemTypeSchemaId)
                .Include(b => b.CreatedBy)
                .Include(b => b.BacklogItemTypeSchema)
                .Include(b => b.Workflow)
                .ToList();

            return HydrateModels(entities);
        }

        public virtual PaginationResults<BacklogItemTypeModel> GetByAccountID(
            int accountId, ServiceContext serviceContext)
        {
            var query =
                (from account in _DBContext.Account
                 join backlogItemType in _DBContext.BacklogItemType on account.ID equals backlogItemType.AccountID
                 where account.ID == accountId
                 select backlogItemType)
                .Distinct();

            query = ApplySearchToQuery(query, serviceContext);

            int total = query.Count();

            query = ApplySortToQuery(query, serviceContext);

            return GetPaginationResultsFromQuery(query, serviceContext, total);
        }

        protected override DbSet<BacklogItemType> GetDbSet()
        {
            return _DBContext.BacklogItemType;
        }

        private static IQueryable<BacklogItemType> ApplySearchToQuery(IQueryable<BacklogItemType> query, ServiceContext serviceContext)
        {
            if (!string.IsNullOrWhiteSpace(serviceContext.SearchQuery))
            {
                string searchLower = serviceContext.SearchQuery.ToLower();
                query = query.Where(backlogItemType => backlogItemType.Title.ToLower().Contains(searchLower));
            }

            return query;
        }

        private static IOrderedQueryable<BacklogItemType> ApplySortToQuery(IQueryable<BacklogItemType> query, ServiceContext serviceContext)
        {
            IOrderedQueryable<BacklogItemType>? result = null;

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

                    Expression<Func<BacklogItemType, string>> sortKeySelector = item =>
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
                Expression<Func<BacklogItemType, string>> sortKeySelector = item => item.ID.ToString();
                result = query.OrderByDescending(sortKeySelector);
            }

            return result;
        }

        private PaginationResults<BacklogItemTypeModel> GetPaginationResultsFromQuery(IQueryable<BacklogItemType> query, ServiceContext serviceContext, int total)
        {
            int page = serviceContext.Page;
            int pageSize = serviceContext.ItemsPerPage;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<BacklogItemType> entities = query.ToList();
            List<BacklogItemTypeModel> models = HydrateModels(entities);
            return new PaginationResults<BacklogItemTypeModel>(models, total, page, pageSize);
        }
    }
}
