using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryRepository : EntityRepository<DBContext,  BacklogItemLinkTypeSchemaEntryModel, BacklogItemLinkTypeSchemaEntry, int>
    {
        private readonly ServiceContext _ServiceContext;

        public BacklogItemLinkTypeSchemaEntryRepository(
            DBContext dbContext, 
            Hydrator hydrator,
            ServiceContext serviceContext
            ) : base(dbContext, hydrator)
        {
            _ServiceContext = serviceContext;
        }

        public virtual PaginationResults<BacklogItemLinkTypeSchemaEntryModel> GetBySchemaID(int schemaID)
        {
            var query =
                (from schema in _DBContext.BacklogItemLinkTypeSchema
                 join entry in _DBContext.BacklogItemLinkTypeSchemaEntry on schema.ID equals entry.BacklogItemLinkTypeSchemaID
                 where schema.ID == schemaID
                 select entry)
                .Distinct();

            query = ApplySearchToQuery(query);

            int total = query.Count();

            query = ApplySortToQuery(query);

            return GetPaginationResultsFromQuery(query, total);
        }

        public override int GetIdentifier(BacklogItemLinkTypeSchemaEntryModel model)
        {
            return model.ID;
        }

        protected override DbSet<BacklogItemLinkTypeSchemaEntry> GetDbSet()
        {
            return _DBContext.BacklogItemLinkTypeSchemaEntry;
        }

        private IQueryable<BacklogItemLinkTypeSchemaEntry> ApplySearchToQuery(
            IQueryable<BacklogItemLinkTypeSchemaEntry> query)
        {
            if (!string.IsNullOrWhiteSpace(_ServiceContext.SearchQuery))
            {
                string searchLower = _ServiceContext.SearchQuery.ToLower();
                query = query.Where(backlogItemLinkTypeSchemaEntry =>
                    backlogItemLinkTypeSchemaEntry.BacklogItemLinkType.Title.ToLower().Contains(searchLower)
                    || backlogItemLinkTypeSchemaEntry.BacklogItemLinkType.TitleOpposite.ToLower().Contains(searchLower));
            }

            return query;
        }

        private IOrderedQueryable<BacklogItemLinkTypeSchemaEntry> ApplySortToQuery(
            IQueryable<BacklogItemLinkTypeSchemaEntry> query)
        {
            IOrderedQueryable<BacklogItemLinkTypeSchemaEntry>? result = null;

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

                    Expression<Func<BacklogItemLinkTypeSchemaEntry, string>> sortKeySelector = item =>
                        ((DateTimeOffset)item.CreatedOn).ToUnixTimeSeconds().ToString();
                    switch (sortField)
                    {
                        case "title":
                            sortKeySelector = item => item.BacklogItemLinkType.Title;
                            break;
                        case "titleOpposite":
                            sortKeySelector = item => item.BacklogItemLinkType.TitleOpposite;
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
                Expression<Func<BacklogItemLinkTypeSchemaEntry, string>> sortKeySelector = item => item.ID.ToString();
                result = query.OrderByDescending(sortKeySelector);
            }

            return result;
        }

        private PaginationResults<BacklogItemLinkTypeSchemaEntryModel> GetPaginationResultsFromQuery(
            IQueryable<BacklogItemLinkTypeSchemaEntry> query, int total)
        {
            int page = _ServiceContext.Page;
            int pageSize = _ServiceContext.ItemsPerPage;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            List<BacklogItemLinkTypeSchemaEntry> entities = query.ToList();
            List<BacklogItemLinkTypeSchemaEntryModel> models = HydrateModels(entities);
            return new PaginationResults<BacklogItemLinkTypeSchemaEntryModel>(models, total, page, pageSize);
        }
    }
}
